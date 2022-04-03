using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
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
    public class AnalysisController : LaboteControllerBase
    {
        private readonly LaboteContext _context;

        public AnalysisController(LaboteContext context)
        {
            _context = context;
        }

        private const string pageName = "analiz-olusturma";

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
        public async Task<dynamic> ConfirmSample(SampleConfirmCreateModel model)
        {
            var data=_context.SampleAccepts.Where(x => x.Id == model.SampleAcceptId).FirstOrDefault();
            data.DeliveyToLaboratoeyDate = model.ConfirmDate;
            data.ConfirmToGetLaboratoryUserId = User.Identity.UserId();
            data.SampleAcceptStatus = Core.Constants.Enums.SampleAcceptStatus.SubmitToLaboratory;
            _context.Update(data);
            _context.SaveChanges();
            return PageResponse;

        }

    }
}
