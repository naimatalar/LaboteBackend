using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
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
        private const string pageName = "tetkik-tanimlama";
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
                SampleExaminationDevicesCount = x.SampleExaminationDevices.Count(),
               
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
                    y.Device.Name,
                    y.Device.Brand,
                    y.Device.Model
                }),
                SampleExaminationPriceCurrencies = x.SampleExaminationPriceCurrencies.Select(y => new
                {
                    CurrenyType = (int)y.CurrenyType,
                    y.Id,
                    y.Price,
                    CurrenyTypeName = y.CurrenyType.GetDisplayName(),

                })
            }).ToList();
            return PageResponse;
        }


        [HttpPost("CreateSampleExamination")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateSampleExamination(SampleExaminationCreateRequestModel model)
        {
            var userId = User.Identity.UserId();
            var topicId = _context.Users.Where(x => x.Id == userId).FirstOrDefault().UserTopicId;
            var sampleExamination = new SampleExamination
            {
                Name = model.Name,
                Unit = model.Unit,
                Description = model.Description,
                UserTopicId = topicId,
                SampleMethod=model.SampleMethod
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
                            CurrenyType = item.CurrenyType,
                            Price = item.Price,
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            return PageResponse;
        }


    }
}
