using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class CurrentCustomer : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string LogoImageName { get; set; }
        public string TaxNumber { get; set; }
        public string TaxAgency { get; set; }
        public string OfficialAgent { get; set; }
        public virtual ICollection<CurrentCustomerContactInfo> CurrentCustomerContactInfos { get; set; }
        public virtual ICollection<CurrentCustomerBankAccountInfo> CurrentCustomerBankAccountInfos { get; set; }
        public virtual ICollection<SampleAccept> SampleAccepts { get; set; }
    }
    public class CurrentCustomerContactInfo : BaseEntity
    {
        public CurrentCustomer CurrentCustomer { get; set; }
        public Guid CurrentCustomerId { get; set; }
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
    public class CurrentCustomerBankAccountInfo : BaseEntity
    {
        public CurrentCustomer CurrentCustomer { get; set; }
        public Guid CurrentCustomerId { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string BankMerchant { get; set; }

    }

}
