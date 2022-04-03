using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
  
    public class CreateEditCurrentCutomerRequestModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string LogoBase64String { get; set; }
        public string TaxNumber { get; set; }
        public string TaxAgency { get; set; }
        public string OfficialAgent { get; set; }
        public CurrentCustomerContactInfoRequestModel CurrentCustomerContactInfos { get; set; } = new CurrentCustomerContactInfoRequestModel();
        public CurrentCustomerBankAccountInfoRequestModel CurrentCustomerBankAccountInfos { get; set; } = new CurrentCustomerBankAccountInfoRequestModel();

    }
    public class CurrentCustomerContactInfoRequestModel
    {
     
        public string MailAddress1 { get; set; }
        public string MailAddress2 { get; set; }
        public string MailAddress3 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string neighborhood { get; set; }
        public string FullAddress { get; set; }
        public string WebSite { get; set; }
    }
    public class CurrentCustomerBankAccountInfoRequestModel
    {
       
        public Guid CurrentCustomerId { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string BankMerchant { get; set; }

    }
}
