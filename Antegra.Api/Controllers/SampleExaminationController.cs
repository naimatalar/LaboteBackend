using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleExaminationController : LaboteControllerBase
    {
        private readonly LaboteContext _context;
        private const string pageName = "analiz-tanimlama";
        public SampleExaminationController(LaboteContext context)
        {
            _context = context;
        }


        [HttpGet("GetAllSampleExaminationByCurrentTopic")]
        public async Task<dynamic> GetAllSampleExaminationByCurrentTopic()
        {
            var userId = User.Identity.UserId();
            var topicId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().UserTopicId;
            var data = _context.SampleExaminations.Where(x => x.UserTopicId == topicId).Select(x => new
            {
                x.Description,
                x.Id,
                x.Name,
                x.SampleMethod,
                SampleExaminationDevicesCount = x.SampleExaminationDevices.Count,
                SampleExaminationDevices = x.SampleExaminationDevices.Select(y => new
                {
                    Id = y.Device.Id,

                }).Select(x => x.Id).ToList(),
                SampleExaminationPriceCurrencies = x.SampleExaminationPriceCurrencies.Select(y => new
                {
                    CurrencyType = (int)y.CurrencyType,
                    y.Id,
                    y.Price,
                    CurrenyTypeName = y.CurrencyType.GetDisplayName(),

                }).ToList()

            }).ToList();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpGet("GetSampleExaminationById/{id}")]
        public async Task<dynamic> GetSampleExaminationById(Guid id)
        {
            var data = _context.SampleExaminations.Where(x => x.Id == id).Select(x => new
            {
                x.Description,
                x.Id,
                x.Name,
                x.SampleMethod,
                SampleExaminationDevices = x.SampleExaminationDevices.Select(y => new
                {
                    y.Device.Id,

                }).Select(x => x.Id).ToList(),
                SampleExaminationPriceCurrencies = x.SampleExaminationPriceCurrencies.Select(y => new
                {
                    CurrencyType = (int)y.CurrencyType,
                    y.Id,
                    y.Price,
                    CurrenyTypeName = y.CurrencyType.GetDisplayName(),

                }).ToList()
            }).FirstOrDefault();
            PageResponse.Data = data;
            return PageResponse;
        }


        [HttpGet("GetSampleExaminationByQuery/{q?}")]
        public async Task<dynamic> GetSampleExaminationById(string q = "")
        {
            var data = _context.SampleExaminations.Where(x => x.Name.Contains(q)).Select(x => new
            {
                x.Id,
                x.Name,
            }).Take(7).ToList();
            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpPost("CreateSampleExamination")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateSampleExamination(SampleExaminationCreateRequestModel model)
        {
            if (model.Id == null)
            {
                var userId = User.Identity.UserId();
                var topicId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().UserTopicId;
                var sampleExamination = new SampleExamination
                {
                    Name = model.Name,
                    Unit = model.Unit,
                    Description = model.Description,
                    UserTopicId = topicId,
                    SampleMethod = model.SampleMethod
                };
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.SampleExaminations.Add(sampleExamination);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }

                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var item in model.SampleExaminationDevices)
                        {
                            context.SampleExaminationDevice.Add(new SampleExaminationDevice
                            {
                                SampleExaminationId = sampleExamination.Id,
                                DeviceId = item
                            });
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var item in model.SampleExaminationPriceCurrencies)
                        {
                            context.SampleExaminationPriceCurrencies.Add(new SampleExaminationPriceCurrency
                            {
                                SampleExaminationId = sampleExamination.Id,
                                CurrencyType = (Core.Constants.Enums.SampleExaminationCurrencyType)item.CurrencyType,
                                Price = item.Price,
                            });
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }

                return PageResponse;
            }
            else
            {
                var userId = User.Identity.UserId();
                var topicId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().UserTopicId;
                var sampleExamination = new SampleExamination
                {
                    Id = (Guid)model.Id,
                    Name = model.Name,
                    Unit = model.Unit,
                    Description = model.Description,
                    UserTopicId = topicId,
                    SampleMethod = model.SampleMethod
                };
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.SampleExaminations.Update(sampleExamination);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }

                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var devices = context.SampleExaminations.Where(x => x.Id == model.Id).Select(x => new { devices = x.SampleExaminationDevices }).FirstOrDefault();
                        context.SampleExaminationDevice.RemoveRange(devices.devices);
                        foreach (var item in model.SampleExaminationDevices)
                        {
                            context.SampleExaminationDevice.Add(new SampleExaminationDevice
                            {
                                SampleExaminationId = sampleExamination.Id,
                                DeviceId = item
                            });
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var currency = context.SampleExaminations.Where(x => x.Id == model.Id).Select(x => new { currency = x.SampleExaminationPriceCurrencies }).FirstOrDefault();
                        context.SampleExaminationPriceCurrencies.RemoveRange(currency.currency);

                        foreach (var item in model.SampleExaminationPriceCurrencies)
                        {
                            context.SampleExaminationPriceCurrencies.Add(new SampleExaminationPriceCurrency
                            {
                                SampleExaminationId = sampleExamination.Id,
                                CurrencyType = (Core.Constants.Enums.SampleExaminationCurrencyType)item.CurrencyType,
                                Price = item.Price,
                            });
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }

                return PageResponse;

            }

            return PageResponse;
        }
        [HttpGet("GetSampleExaminationResultValueType/{SampleExaminationId}")]
        public async Task<dynamic> GetSampleExaminationResultValueType(Guid SampleExaminationId)
        {
            var data = _context.SampleExaminationResultValueTypes.Where(x => x.SampleExaminationId == SampleExaminationId).Select(x => new
            {
                x.Id,
                x.MeasurementUnit,
                x.MeasurementUnitLongName,
                x.MeasureUnitType,
                MeasureUnitTypeName = x.MeasureUnitType.GetDisiplayDescription(),
                x.MeasureUnitSymbol,

            });

            PageResponse.Data = data;
            return PageResponse;
        }

        [HttpGet("DeleteSampleExaminationResultValueType/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> DeleteSampleExaminationResultValueType(Guid Id)
        {
            var data = _context.SampleExaminationResultValueTypes.Where(x => x.Id == Id).FirstOrDefault();
            _context.SampleExaminationResultValueTypes.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }

        [HttpPost("CreateSampleExaminationResultValueType")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateSampleExaminationResultValueType(CreateSampleExaminationResultValueTypeModel model)
        {

           
            var ress = new SampleExaminationResultValueType();
            if (model.Id == null)
            {
                ress = new SampleExaminationResultValueType
                {
                    SampleExaminationId = model.SampleExaminationId,
                    MeasurementUnit = model.MeasurementUnit,
                    MeasurementUnitLongName = model.MeasurementUnitLongName,
                    MeasureUnitSymbol = model.MeasureUnitSymbol,
                    MeasureUnitType = (Core.Constants.Enums.MeasureUnitType)model.MeasureUnitType,
                };
                _context.SampleExaminationResultValueTypes.Add(ress);
                _context.SaveChanges();
            }
            else
            {
                var data = _context.SampleExaminationResultValueTypes.Where(x => x.Id == model.Id).FirstOrDefault();
                data.SampleExaminationId = model.SampleExaminationId;
                data.MeasurementUnit = model.MeasurementUnit;
                data.MeasurementUnitLongName = model.MeasurementUnitLongName;
                data.MeasureUnitSymbol = model.MeasureUnitSymbol;
                data.MeasureUnitType = (Core.Constants.Enums.MeasureUnitType)model.MeasureUnitType;
                ress = data;
                _context.Update(data);
                _context.SaveChanges();
            }

            PageResponse.Data = ress;
            return PageResponse;
        }

    }
}
