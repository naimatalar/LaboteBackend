using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
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
    public class CurrentCustomerController : LaboteControllerBase
    {
        private readonly LaboteContext _context;
        private const string pageName = "cari-hesaplar";

        public CurrentCustomerController(LaboteContext context)
        {
            _context = context;
        }

        [HttpPost("GetAllCurrentCustomer")]
        public async Task<dynamic> GetAllCurrentCustomer(BasePaginationRequestModel model)
        {
            var currentCustomers = _context.CurrentCustomers.Where(x => x.IsActive).Select(x => new
            {
                x.Id,
                x.Name,
                x.Title,
                x.TaxNumber,
                x.TaxAgency,
                x.CreateDate,
                ViewCreateDate = x.CreateDate.ToString("dd/MM/yyyy HH:mm"),
            });

            PageResponse.Data = new { List = currentCustomers.Skip((model.PageNumber - 1) * (model.PageSize - 1)).Take(model.PageSize).ToList(), TotalCount = (currentCustomers.Count() / model.PageSize) + 1, PageNumber = model.PageNumber };
            return Ok(PageResponse);
        }
        [HttpGet("GetAllCurrentCustomerByName/{q?}")]
        public async Task<dynamic> GetAllCurrentCustomerByName(string q="")
      {
            var currentCustomers = _context.CurrentCustomers.Where(x => x.IsActive && 
            x.Name.Contains(q)|| x.Title.Contains(q) ).Select(x => new
            {
                x.Id,
                x.Name,
                x.Title,
                x.TaxNumber,
                x.TaxAgency,
                x.CreateDate,
                ViewCreateDate = x.CreateDate.ToString("dd/MM/yyyy HH:mm"),
            }).Take(7).ToList();

            PageResponse.Data = currentCustomers;
            //PageResponse.Data = new { List = currentCustomers.Skip((model.PageNumber - 1) * (model.PageSize - 1)).Take(model.PageSize).ToList(), TotalCount = (currentCustomers.Count() / model.PageSize) + 1, PageNumber = model.PageNumber };
            return PageResponse;
        }

        [HttpGet("GetAllCurrentCustomerById/{Id}")]
        public async Task<dynamic> GetAllCurrentCustomerById(Guid Id)
        {
            var currentCustomers = _context.CurrentCustomers.Where(x => x.Id == Id).Select(x => new
            {
                x.Name,
                x.Title,
                x.TaxNumber,
                x.TaxAgency,
                x.CreateDate,
                x.LogoImageName,
                x.Id,
                CurrentCustomerContactInfos = x.CurrentCustomerContactInfos.Select(y => new
                {
                    y.FullAddress,
                    y.City,
                    y.Country,
                    y.MailAddress1,
                    y.MailAddress2,
                    y.MailAddress3,
                    y.neighborhood,
                    y.Phone1,
                    y.Phone2,
                    y.Phone3,
                    y.WebSite
                }).FirstOrDefault(),
                CurrentCustomerBankAccountInfos = x.CurrentCustomerBankAccountInfos.Select(x => new
                {
                    x.BankMerchant,
                    x.BankName,
                    x.Iban
                }).FirstOrDefault()


            }).FirstOrDefault();

            PageResponse.Data = currentCustomers;
            return PageResponse;
        }


        [HttpPost("CreateCurrentCustomer")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateCurrentCustomer(CreateEditCurrentCutomerRequestModel model)
        {
            var customer = new CurrentCustomer();
            if (model.Id == null)
            {
                customer = new CurrentCustomer
                {
                    CreatorUserId = User.Identity.UserId(),
                    LogoImageName = model.LogoBase64String,
                    Name = model.Name,
                    OfficialAgent = model.OfficialAgent,
                    Title = model.Title,
                    TaxNumber = model.TaxNumber,
                    TaxAgency = model.TaxAgency,
                };
                using (var context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.CurrentCustomers.Add(customer);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                }
            }
            else
            {
                using (var context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var editCustomer = context.CurrentCustomers.Include(x => x.CurrentCustomerContactInfos).Include(x => x.CurrentCustomerBankAccountInfos).Where(x => x.Id == model.Id).FirstOrDefault();

                        context.CurrentCustomerBankAccountInfos.RemoveRange(editCustomer.CurrentCustomerBankAccountInfos);
                        context.CurrentCustomerContactInfos.RemoveRange(editCustomer.CurrentCustomerContactInfos);

                        editCustomer.CreatorUserId = User.Identity.UserId();
                        editCustomer.LogoImageName = model.LogoBase64String;
                        editCustomer.Name = model.Name;
                        editCustomer.OfficialAgent = model.OfficialAgent;
                        editCustomer.Title = model.Title;
                        editCustomer.TaxNumber = model.TaxNumber;
                        editCustomer.TaxAgency = model.TaxAgency;

                        context.Update(editCustomer);
                        context.SaveChanges();
                        customer = editCustomer;
                        transaction.Commit();
                    }
                }
            }
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (model.CurrentCustomerBankAccountInfos != null)
                    {
                        context.CurrentCustomerBankAccountInfos.Add(new CurrentCustomerBankAccountInfo
                        {
                            BankMerchant = model.CurrentCustomerBankAccountInfos.BankMerchant,
                            BankName = model.CurrentCustomerBankAccountInfos.BankName,
                            CurrentCustomerId = customer.Id,
                            Iban = model.CurrentCustomerBankAccountInfos.Iban,
                        });

                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
            }
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (model.CurrentCustomerContactInfos != null)
                    {
                        context.CurrentCustomerContactInfos.Add(new CurrentCustomerContactInfo
                        {
                            City = model.CurrentCustomerContactInfos.City,
                            Country = model.CurrentCustomerContactInfos.Country,
                            FullAddress = model.CurrentCustomerContactInfos.FullAddress,
                            MailAddress1 = model.CurrentCustomerContactInfos.MailAddress1,
                            MailAddress2 = model.CurrentCustomerContactInfos.MailAddress2,
                            MailAddress3 = model.CurrentCustomerContactInfos.MailAddress3,
                            neighborhood = model.CurrentCustomerContactInfos.neighborhood,
                            Phone1 = model.CurrentCustomerContactInfos.Phone1,
                            Phone2 = model.CurrentCustomerContactInfos.Phone2,
                            Phone3 = model.CurrentCustomerContactInfos.Phone3,
                            WebSite = model.CurrentCustomerContactInfos.WebSite,
                            CurrentCustomerId=customer.Id,
                        });

                        context.SaveChanges();
                        transaction.Commit();
                    }

                }
            }

            return PageResponse;

        }
    }
}
