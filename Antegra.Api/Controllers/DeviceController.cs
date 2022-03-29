using Labote.Api.BindingModel;
using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Labote.Services;
using Labote.Services.Interfaces;
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
    public class DeviceController : LaboteControllerBase
    {
        private readonly LaboteContext _context;


        private const string pageName = "cihaz-tanimlama";

        public DeviceController(LaboteContext context, IUserRoleService userRoleService)
        {
            _context = context;

        }

        [HttpGet("GetCurrentTopicDevice")]
        public async Task<dynamic> GetCurrentTopicDevice()
        {
            var userId = User.Identity.UserId();
            var topicId = _context.Users.Where(x => x.Id == userId).FirstOrDefault()?.UserTopicId;
            var devices = _context.Devices.Where(x => x.UserTopicId == topicId).Select(x => new
            {
                x.Name,
                x.Code,
                x.Description,
                x.Id
            }).ToList();
            PageResponse.Data = devices;
            return PageResponse;
        }
        [HttpGet("GetAllCurrentLaboratoryDevice/{LaboratoryId}")]
        public async Task<dynamic> GetAllCurrentDevice(Guid LaboratoryId)
        {

            var deviceUsers = _context.LaboratoryDevices.Where(x => x.LaboratoryId == LaboratoryId).Select(x => new
            {
                x.Device.Name,
                x.Device.Id,
            });

            PageResponse.Data = deviceUsers;
            return PageResponse;
        }
        [HttpGet("GetById/{Id}")]
        public async Task<dynamic> GetById(Guid Id)
        {
            var devices = _context.Devices.Where(x => x.Id == Id).Select(x => new
            {
                x.Name,
                x.Code,
                x.Description,
                x.Model,
                x.Brand,
                x.Id
            }).FirstOrDefault();

            PageResponse.Data = devices;
            return PageResponse;
        }
        [HttpPost("CreateDevice")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateDevice(DeviceCreateRequestModel model)
        {
            var userId = User.Identity.UserId();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();

            var labb = new Device();
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    if (model.Id == null)
                    {
                        labb = new Device
                        {
                            Code = model.Code,
                            Description = model.Description,
                            Name = model.Name,
                            Brand = model.Brand,
                            Model = model.Model,
                            UserTopicId = user.UserTopicId,
                        };
                        context.Devices.Add(labb);
                        context.SaveChanges();
                    }
                    else
                    {
                        var data = context.Devices.Where(x => x.Id == model.Id).FirstOrDefault();
                        data.Code = model.Code;
                        data.Description = model.Description;
                        data.Name = model.Name;
                        data.UserTopicId = user.UserTopicId;
                        data.Brand = model.Brand;
                        data.Model = model.Model;
                        context.Update(data);
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                PageResponse.Data = new
                {
                    Refres = false
                };
            }


            return PageResponse;
        }

        [HttpGet("GetAllDevicesByTopic")]
        public async Task<dynamic> GetAllDevicesByTopic()
        {
            try
            {
                var userId = User.Identity.UserId();

                var laboteUser = _context.Users.Include(x => x.UserTopic).ThenInclude(x => x.Devices).FirstOrDefault(x => x.Id == userId).UserTopic.Devices
                               .Select(x => new
                               {
                                   x.Id,
                                   x.Name,
                                   x.Brand,
                                   x.Model,
                                   x.Code,
                                   x.Description
                               }).ToList();

                PageResponse.Data = laboteUser;
            }
            catch (Exception e)
            {

                throw;
            }

            return PageResponse;
        }

        [HttpGet("GetAllDevicesByLaboratory/{id}")]
        public async Task<dynamic> GetAllDevicesByLaboratory(Guid Id)
        {
            try
            {

                var devices = _context.LaboratoryDevices.Where(x => x.LaboratoryId == Id)
                               .Select(y => new
                               {

                                   Id = y.Device.Id,
                                   Name = y.Device.Name,
                                   Brand = y.Device.Brand,
                                   Model = y.Device.Model,
                                   Code = y.Device.Code,
                                   Description = y.Device.Description
                               }).ToList();

                PageResponse.Data = devices;
            }
            catch (Exception e)
            {

                throw;
            }

            return PageResponse;
        }
        [HttpPost("GetDeviceByLaboratoryDeviceName")]
        public async Task<dynamic> GetDeviceByLaboratoryDeviceName(GetDeviceByLaboratoryDeviceNameModel model)
        {
            var userId = User.Identity.UserId();
            var currentUser = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (currentUser == null)
            {
                PageResponse.IsError = true;
                PageResponse.Message = "Kullanıcı bulunamadı";
                return PageResponse;
            };
            try
            {
                var laboteUser = _context.Devices.Where(x => x.UserTopicId == currentUser.UserTopicId).Where(x => x.Name.ToLower().Contains(model.Query.ToLower()) || x.Brand.ToLower().Contains(model.Query.ToLower()))
                               .Select(x => new
                               {
                                   x.Id,
                                   x.Brand,
                                   x.Model,
                                   x.Description,
                                   x.Code,
                                   x.Name
                               }).ToList();

                PageResponse.Data = laboteUser;
            }
            catch (Exception e)
            {

                throw;
            }

            return PageResponse;
        }

        [HttpPost("SetOrRemoveDeviceOnLaboratory")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> SetOrRemoveDeviceOnLaboratory(SetOrDeviceOnLaboratoryRequestModel model)
        {
            if (model.Action == 1)
            {

                if (_context.LaboratoryDevices.Any(x => x.DeviceId == model.DeviceId && x.LaboratoryId == model.LaboratoryId))
                {
                    PageResponse.IsError = true;
                    PageResponse.Message = "Bu kullanıcı bu laboratuvara zaten atanmış";
                    return PageResponse;
                }
                _context.LaboratoryDevices.Add(new LaboratoryDevice
                {
                    DeviceId = model.DeviceId,
                    LaboratoryId = model.LaboratoryId,
                });
                _context.SaveChanges();
            }
            else
            {
                var data = _context.LaboratoryDevices.Where(x => x.DeviceId == model.DeviceId && x.LaboratoryId == model.LaboratoryId);
                _context.LaboratoryDevices.RemoveRange(data);
                _context.SaveChanges();
            }

            return true;
        }

        [HttpGet("GetDeviceResultValueType/{DeviceId}")]
        public async Task<dynamic> GetDeviceResultValueType(Guid DeviceId)
        {
           var data= _context.DeviceResultValueTypes.Where(x => x.DeviceId == DeviceId).Select(x => new
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

        [HttpGet("GetDeviceResultValueSampleUnitReferences/{DeviceId}")]
        public async Task<dynamic> GetDeviceResultValueSampleUnitReferences(Guid DeviceId)
        {
            var data = _context.DeviceResultValueSampleUnitReferences.Where(x => x.DeviceId == DeviceId).Select(x => new
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


        [HttpPost("CreateDeviceResultValueType")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateDeviceResultValueType(CreateDeviceResultValueTypeModel model)
        {
            var ress = new DeviceResultValueType();
            if (model.Id == null)
            {
                ress = new DeviceResultValueType
                {
                    DeviceId = model.DeviceId,
                    MeasurementUnit = model.MeasurementUnit,
                    MeasurementUnitLongName = model.MeasurementUnitLongName,
                    MeasureUnitSymbol = model.MeasureUnitSymbol,
                    MeasureUnitType = (Core.Constants.Enums.MeasureUnitType)model.MeasureUnitType,
                };
                _context.DeviceResultValueTypes.Add(ress);
                _context.SaveChanges();
            }
            else
            {
                var data = _context.DeviceResultValueTypes.Where(x => x.Id == model.Id).FirstOrDefault();
                data.DeviceId = model.DeviceId;
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
       
    
        [HttpPost("CreateDeviceResultValueTypeSampleReference")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateDeviceResultValueTypeSampleReference(CreateDeviceResultValueTypeSampleReferenceRequestModel model)
        {
            var ress = new DeviceResultValueSampleUnitReference();
            if (model.Id == null)
            {
                ress = new DeviceResultValueSampleUnitReference
                {
                    DeviceId = model.DeviceId,
                    MeasurementUnit = model.MeasurementUnit,
                    MeasurementUnitLongName = model.MeasurementUnitLongName,
                    MeasureUnitSymbol = model.MeasureUnitSymbol,
                    MeasureUnitType = (Core.Constants.Enums.MeasureUnitType)model.MeasureUnitType,
                };
                _context.DeviceResultValueSampleUnitReferences.Add(ress);
                _context.SaveChanges();
            }
            else
            {
                var data = _context.DeviceResultValueSampleUnitReferences.Where(x => x.Id == model.Id).FirstOrDefault();
                data.DeviceId = model.DeviceId;
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

     
        [HttpGet("DeleteDeviceResultValueTypeSampleReference/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> DeleteDeviceResultValueTypeSampleReference(Guid Id)
        {

           var  data=_context.DeviceResultValueSampleUnitReferences.Where(x => x.Id == Id).FirstOrDefault();
            _context.DeviceResultValueSampleUnitReferences.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }
      
        
        [HttpGet("DeleteDeviceResultValueType/{Id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> DeleteDeviceResultValueType(Guid Id)
        {

           var  data=_context.DeviceResultValueTypes.Where(x => x.Id == Id).FirstOrDefault();
            _context.DeviceResultValueTypes.Remove(data);
            _context.SaveChanges();
            return PageResponse;
        }
    }
}
