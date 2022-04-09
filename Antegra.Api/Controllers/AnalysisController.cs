using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : LaboteControllerBase
    {
        private readonly LaboteContext _context;

        public AnalysisController(LaboteContext context)
        {
            _context = context;
        }

        private const string pageName = "analiz-olusturma";

        [HttpPost("GetSampleAcceptByLaboratoryId")]
        public async Task<dynamic> GetSampleAcceptByLaboratoryId(BasePaginationRequestModel model)
        {

            var currentCustomers = _context.Laboratories.Where(x => x.IsActive && x.Id == model.LaboratoryId).SelectMany(x => x.SampleAccepts).Where(x => x.SampleAcceptStatus == Enums.SampleAcceptStatus.AcceptFromCustomer).Select(x => new
            {
                x.Id,
                x.Brand,
                x.AcceptedDate,
                ViewAcceptedDate = x.AcceptedDate.ToString("dd/MM/yyyy HH:mm"),
                x.Barcode,
                LaboteUser = x.LaboteUser.FirstName + " " + x.LaboteUser.Lastname,
                x.ManufactureDate,
                ViewManufactureDate = x.ManufactureDate.Value.ToString("dd/MM/yyyy HH:mm"),
                x.Quantity,
                x.SampleName,
                x.SerialNo,
                x.UnitType,
                x.CreateDate,
                ViewCreateDate = x.CreateDate.ToString("dd/MM/yyyy HH:mm"),
                CurrentCustomer = x.CurrentCustomer.Name + " - " + x.CurrentCustomer.Title,
                BarcodeImageString = Barcode.Generate(x.Barcode)
            });

            PageResponse.Data = new { List = currentCustomers.Skip((model.PageNumber - 1) * (model.PageSize - 1)).Take(model.PageSize).ToList(), TotalCount = (currentCustomers.Count() / model.PageSize) + 1, PageNumber = model.PageNumber };
            return Ok(PageResponse);
        }

        [HttpPost("GetSampleAccept")]
        public async Task<dynamic> GetSampleAccept(BasePaginationRequestModel model)
        {

            var currentCustomers = _context.SampleAccepts.Where(x => x.IsActive && x.SampleAcceptStatus == Core.Constants.Enums.SampleAcceptStatus.AcceptFromCustomer).Select(x => new
            {
                x.Id,
                x.Brand,
                x.AcceptedDate,
                ViewAcceptedDate = x.AcceptedDate.ToString("dd/MM/yyyy HH:mm"),
                x.Barcode,
                LaboteUser = x.LaboteUser.FirstName + " " + x.LaboteUser.Lastname,
                x.ManufactureDate,
                ViewManufactureDate = x.ManufactureDate.Value.ToString("dd/MM/yyyy HH:mm"),
                x.Quantity,
                x.SampleName,
                x.SerialNo,
                x.UnitType,
                x.CreateDate,
                ViewCreateDate = x.CreateDate.ToString("dd/MM/yyyy HH:mm"),
                CurrentCustomer = x.CurrentCustomer.Name + " - " + x.CurrentCustomer.Title,
                BarcodeImageString = Barcode.Generate(x.Barcode)
            });

            PageResponse.Data = new { List = currentCustomers.Skip((model.PageNumber - 1) * (model.PageSize - 1)).Take(model.PageSize).ToList(), TotalCount = (currentCustomers.Count() / model.PageSize) + 1, PageNumber = model.PageNumber };
            return Ok(PageResponse);
        }


        [HttpPost("ConfirmSample")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> ConfirmSample(SampleConfirmCreateModel model)
        {
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var data = context.SampleAccepts.Where(x => x.Id == model.SampleAcceptId).FirstOrDefault();
                    data.DeliveyToLaboratoeyDate = model.ConfirmDate;
                    data.ConfirmToGetLaboratoryUserId = User.Identity.UserId();
                    data.SampleAcceptStatus = Core.Constants.Enums.SampleAcceptStatus.SubmitToLaboratory;
                    context.Update(data);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var sampleExaminationCreateRecord = new AnalisysCreateRecord
                    {
                        AnalysisStartDate = model.ConfirmDate,
                        LaboteUserId = User.Identity.UserId(),
                        SampleAcceptId = model.SampleAcceptId,

                    };
                    var data = context.AnalisysCreateRecords.Add(sampleExaminationCreateRecord);
                    context.SaveChanges();
                    transaction.Commit();
                    PageResponse.Data = new { AnalisysRecordId = sampleExaminationCreateRecord.Id };
                }
            }

            return PageResponse;

        }


        [HttpPost("GetAnalisysRecordByLaboratoryId")]
        public async Task<dynamic> GetAnalisysRecordByLaboratoryId(BasePaginationRequestModel model)
        {
            var data = _context.AnalisysCreateRecords.Include(x => x.SampleAccept).ThenInclude(x => x.LaboteUser).Where(x => x.SampleAccept.LaboratoryId == model.LaboratoryId).Select(x => new
            {
                ConfirmUser = x.LaboteUser.FirstName + " " + x.LaboteUser.Lastname,

            });


            PageResponse.Data = new { List = data.Skip((model.PageNumber - 1) * (model.PageSize - 1)).Take(model.PageSize).ToList(), TotalCount = (data.Count() / model.PageSize) + 1, PageNumber = model.PageNumber };
            return Ok(PageResponse);
        }

        [HttpGet("GetDeviceValueUnitByAnalisysCreateRecordId/{Id}")]

        public async Task<dynamic> GetDeviceValueUnitByAnalisysCreateRecordId(Guid Id)
        {


            SampleAccept SampleAccepted = new SampleAccept();

            var dd =( from acr in _context.AnalisysCreateRecords.Where(x => x.Id == Id)
                     from se in _context.SampleExaminationSampleAccepts.Where(x => x.SampleAcceptId == acr.SampleAcceptId)
                     select
                            new
                            {
                                se.Id,
                                SampleExaminationName= se.SampleExamination.Name,
                                SampleAccepted=se.SampleAccept,
                                Devices = se.SampleExamination.SampleExaminationDevices.Select(x => new
                                {
                                    x.Device.Id,
                                    x.Device.Name,
                                    x.Device.Model,
                                    DeviceResultValueType = x.Device.DeviceResultValueType.Select(z => new
                                    {
                                        z.Id,
                                        z.MeasurementUnit,
                                        z.MeasureUnitSymbol,
                                        MeasureUnitType=(int)z.MeasureUnitType,
                                        z.MeasurementUnitLongName,
                                        
                                    })

                                })
                            }).ToList();

            PageResponse.Data = new {List=dd,SampleAccepted};
            return PageResponse;

        }


        [HttpPost("SetDeviceValues")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> SetDeviceValues(SampleConfirmCreateModel model)
        {
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var data = context.SampleAccepts.Where(x => x.Id == model.SampleAcceptId).FirstOrDefault();
                    data.DeliveyToLaboratoeyDate = model.ConfirmDate;
                    data.ConfirmToGetLaboratoryUserId = User.Identity.UserId();
                    data.SampleAcceptStatus = Core.Constants.Enums.SampleAcceptStatus.SubmitToLaboratory;
                    context.Update(data);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var sampleExaminationCreateRecord = new AnalisysCreateRecord
                    {
                        AnalysisStartDate = model.ConfirmDate,
                        LaboteUserId = User.Identity.UserId(),
                        SampleAcceptId = model.SampleAcceptId,

                    };
                    var data = context.AnalisysCreateRecords.Add(sampleExaminationCreateRecord);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            return PageResponse;

        }


    }
}
