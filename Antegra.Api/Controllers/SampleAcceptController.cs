using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Constants;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleAcceptController : LaboteControllerBase
    {
        private readonly LaboteContext _context;
        private const string pageName = "numune-kabul";

        public SampleAcceptController(LaboteContext context)
        {
            _context = context;
        }

        [HttpPost("GetAllSampleAccept")]
        public async Task<dynamic> GetAllSampleAccept(BasePaginationRequestModel model)
        {
            
            var currentCustomers = _context.SampleAccepts.Where(x => x.IsActive).Select(x => new
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
                SampleAcceptBringingType = x.SampleAcceptBringingType.GetDisiplayDescription(),
                SampleAcceptPackaging = x.SampleAcceptPackaging.GetDisiplayDescription(),
                SampleAcceptStatus = x.SampleAcceptStatus.GetDisiplayDescription(),
                x.SampleName,
                SampleReturnType = x.SampleReturnType.GetDisiplayDescription(),
                x.SerialNo,
                x.UnitType,
                x.CreateDate,
                ViewCreateDate = x.CreateDate.ToString("dd/MM/yyyy HH:mm"),
                CurrentCustomer = x.CurrentCustomer.Name + " " + x.CurrentCustomer.Title,
                BarcodeImageString=Barcode.Generate(x.Barcode)
            });

            PageResponse.Data = new { List = currentCustomers.Skip((model.PageNumber - 1) * (model.PageSize - 1)).Take(model.PageSize).ToList(), TotalCount = (currentCustomers.Count() / model.PageSize) + 1, PageNumber = model.PageNumber };
            return Ok(PageResponse);
        }

        [HttpGet("GetAllSampleAcceptById/{Id}")]
        public async Task<dynamic> GetAllSampleAcceptById(Guid Id)
        {
            var currentCustomers = _context.SampleAccepts.Where(x => x.IsActive && x.Id == Id).Select(x => new
            {
                x.Id,
                x.Brand,
                x.AcceptedDate,
                x.Barcode,
                x.ManufactureDate,
                x.Quantity,
                SampleAcceptBringingType = (int)x.SampleAcceptBringingType,
                SampleAcceptPackaging = (int)x.SampleAcceptPackaging,
                SampleAcceptStatus = (int)x.SampleAcceptStatus,
                x.SampleName,
                SampleReturnType = (int)x.SampleReturnType,
                x.SerialNo,
                x.UnitType,
                x.CreateDate,
                CurrentCustomerName = x.CurrentCustomer.Name + " - " + x.CurrentCustomer.Title + " - " + x.CurrentCustomer.TaxNumber,
                x.CurrentCustomerId,
                SampleExaminationSampleAcceptsSelectList=x.SampleExaminationSampleAccepts.Select(d=>d.SampleExamination).Select(z=>new
                {
                    Label=z.Name,
                    Value=z.Id
                })
            }).FirstOrDefault();

            PageResponse.Data = currentCustomers;
            return PageResponse;
        }


        [HttpPost("CreateSampleAccepted")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateSampleAccepted(SampleAcceptCreateModel model)
        {
            var barcode =  DateTime.Now.ToString("yyddMMhhmmss");

            var userId = User.Identity.UserId();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var masterData = new Core.Entities.SampleAccept();
            if (model.Id == null)
            {
                using (var context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        masterData = new Core.Entities.SampleAccept
                        {
                            AcceptedDate = model.AcceptedDate,
                            Barcode = barcode,
                            Brand = model.Brand,
                            CurrentCustomerId = model.CurrentCustomerId,
                            ExpirationDate = model.ExpirationDate,
                            LaboteUserId = userId,
                            ManufactureDate = model.ManufactureDate,
                            Quantity = model.Quantity,
                            SampleAcceptBringingType = (Enums.SampleAcceptBringingType)model.SampleAcceptBringingType,
                            SampleAcceptPackaging = (Enums.SampleAcceptPackaging)model.SampleAcceptPackaging,
                            SampleAcceptStatus = Enums.SampleAcceptStatus.AcceptFromCustomer,
                            SampleName = model.SampleName,
                            SampleReturnType = (Enums.SampleReturnType)model.SampleReturnType,
                            SerialNo = model.SerialNo,
                            UnitType = model.UnitType,
                            Description = model.Description,
                            

                        };
                        context.SampleAccepts.Add(masterData);
                        try
                        {
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                        }

                    }
                }
            }
            else
            {
                using (var context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var sampleAccepted = context.SampleAccepts.Where(x => x.Id == model.Id).FirstOrDefault();
                        sampleAccepted.AcceptedDate = model.AcceptedDate;
                        sampleAccepted.Barcode = model.Barcode;
                        sampleAccepted.Brand = model.Brand;
                        sampleAccepted.CurrentCustomerId = model.CurrentCustomerId;
                        sampleAccepted.ExpirationDate = model.ExpirationDate;
                        sampleAccepted.LaboteUserId = userId;
                        sampleAccepted.ManufactureDate = model.ManufactureDate;
                        sampleAccepted.Quantity = model.Quantity;
                        sampleAccepted.SampleAcceptBringingType = (Enums.SampleAcceptBringingType)model.SampleAcceptBringingType;
                        sampleAccepted.SampleAcceptPackaging = (Enums.SampleAcceptPackaging)model.SampleAcceptPackaging;
                        sampleAccepted.SampleAcceptStatus = (Enums.SampleAcceptStatus)model.SampleAcceptStatus;
                        sampleAccepted.SampleName = model.SampleName;
                        sampleAccepted.SampleReturnType = (Enums.SampleReturnType)model.SampleReturnType;
                        sampleAccepted.SerialNo = model.SerialNo;
                        sampleAccepted.UnitType = model.UnitType;
                        sampleAccepted.Description = model.Description;
                        context.Update(sampleAccepted);
                        context.SaveChanges();
                        transaction.Commit();
                        masterData = sampleAccepted;
                    }
                }
            }

            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var listDD = context.SampleExaminationSampleAccepts.Where(x => x.SampleAcceptId == masterData.Id).ToList();
                    context.SampleExaminationSampleAccepts.RemoveRange(listDD);
                    foreach (var item in model.SampleExaminationIds)
                    {
                        context.SampleExaminationSampleAccepts.Add(new Core.Entities.SampleExaminationSampleAccept
                        {
                            SampleExaminationId = item,
                            SampleAcceptId=masterData.Id,
                        }) ;
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            return PageResponse;
        }

    }
}
