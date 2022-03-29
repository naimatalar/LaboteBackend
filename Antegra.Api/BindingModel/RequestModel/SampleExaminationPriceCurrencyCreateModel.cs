using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BindingModel.RequestModel
{
    public class SampleExaminationPriceCurrencyCreateModel
    {      
        public int Price { get; set; }
        public Core.Constants.Enums.SampleExaminationCurrencyType CurrenyType { get; set; }
    }
}
