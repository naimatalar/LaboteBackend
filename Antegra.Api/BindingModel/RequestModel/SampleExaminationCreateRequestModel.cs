using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class SampleExaminationCreateRequestModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string SampleMethod { get; set; }
        public List<Guid> SampleExaminationDevices { get; set; } = new List<Guid>();
        public List<SampleExaminationPriceCurrencyCreateModel> SampleExaminationPriceCurrencies { get; set; }= new List<SampleExaminationPriceCurrencyCreateModel>();
    }
}
