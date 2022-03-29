using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Constants
{
    public class Enums
    {
        public const string SecretKey = "9fbac4553b7548e6ad7401f201056083";
        public enum HbCategoryAttribute
        {
            Attributes,
            VariantAttributes,
            BaseAttributes
        }
        public enum MarketPlaceKind
        {
            [Display(Name = "TY", Description = "Trendyol.com")]
            Trendyol = 1,
            [Display(Name = "HB", Description = "Hepsiburada.com")]
            Hepsiburada = 2,
            [Display(Name = "N11", Description = "n11.com")]
            N11 = 3,
            [Display(Name = "GG", Description = "gittigidiyor.com")]
            GittiGidiyor = 4,
            [Display(Name = "CS", Description = "ciceksepeti.com")]
            cicekSepeti = 5,
            [Display(Name = "AZ", Description = "amazon.com")]
            Amazon = 6,
            [Display(Name = "EPA", Description = "pttavm.com")]
            EPttAvm = 7
        }
        public enum SampleExaminationCurrencyType
        {
            [Display( Description = "TRY")]
            TRY = 1,
            [Display(Description = "USD")]
            USD = 2,
            [Display(Description = "EURO")]
            EURO = 3,
            [Display( Description = "GBP")]
            GBP = 4,
            [Display( Description = "RUB")]
            RUB = 5,
            [Display(Name = "CNY", Description = "CNY")]
            CNY = 6,
          
        }

        public enum JobScheduleTimeType
        {
            [Display(Description = "Dakika")]
            Minute = 1,
            [Display(Description = "Saat")]
            Hour = 2,
            [Display(Description = "Gün")]
            Day = 3,
        } 
        public enum MeasureUnitType
        {
            [Display(Description = "Yüzde")]
            Percent = 1,
            [Display(Description = "Ondalık")]
            Decimal = 2,
            [Display(Description = "Sayısal")]
            Integer = 3,
            [Display(Description = "Karakter")]
            Char = 4,

        }
        public enum MarketPlaceEndpointType
        {
            [Display(Description = "Şipariş Çekme")]
            GetOrder = 1,
            [Display(Description = "Ürün Oluşturma")]
            ProductCreate = 2,
            [Display(Description = "Ürün Güncelleme")]
            ProductEdit = 3,
            [Display(Description = "Miktar Değişikliği")]
            StockChange = 4,
            [Display(Description = "Fiyat Değişikliği")]
            PriceChange = 5,

        }
        public enum ShipmentPackageStatus
        {
            [Display(Description = "Teslim Edildi")]
            Recieved,

        }
        public enum ChannelType
        {
            [Display(Description = "Alıcı")]
            Reciever,
            [Display(Description = "Gönderici")]
            Sender,

        }

        public const string Admin = "Admin";
        public const string User = "Kullanici";


        public enum DeliveryType
        {
            [Display(Description = "Normal")]
            normal,
            [Display(Description = "Standart Gönderim")]
            StandartDelivery,

        }

        public enum ProgressStatus
        {
            [Display(Description = "Başladı")]
            Began,
            [Display(Description = "Hata Oluştu")]
            Error,
            [Display(Description = "Tamamlandı")]
            Success,
        }
        public enum ProgressType
        {
            [Display(Description = "Sipariş Bilgileri Getir")]
            GetOrder,
            [Display(Description = "Sipariş Veri Tabanına Kayıt")]
            UpdateDb,
            [Display(Description = "Sipariş Oluştur")]
            CreateOrder,


        }

        public enum AddressType
        {
            [Display(Description = "Fatura Adresi")]
            InvoiceAddress,
            [Display(Name = "Kargo Adresi")]
            ShippingAddress
        }
        public enum ErpTransferStatus
        {
            [Display(Description = "Başladı")]
            Begin,
            [Display(Name = "Devam Ediyor")]
            Continue,
            [Display(Name = "Tamamlandı")]
            Success,
            [Display(Name = "İptal")]
            Cancel,


        }
    }


    public static class EnumDisplay
    {

        public static string GetDisiplayDescription(this Enum enm)
        {
          
            var das = enm;
            var enumType = enm.GetType().GetMember(enm.ToString());
          return  enumType.FirstOrDefault().GetCustomAttribute<DisplayAttribute>().Description;


            
        }

    }

}
