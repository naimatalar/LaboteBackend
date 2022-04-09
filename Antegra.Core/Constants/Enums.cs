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
        public enum SampleAcceptStatus
        {
            [Display(Description = "Müşteriden Kabul Edildi")]
            AcceptFromCustomer = 1,
            [Display(Description = "Laboratuvara Teslim Edildi")]
            SubmitToLaboratory = 2,
            [Display(Description = "Laboratuvardan Numune Kabule Teslim Edildi")]
            SubmitToSampleAccept = 3,
            [Display(Description = "Numune Müşteriye Gönderilmek İçin Bekliyor")]
            WaitingForSendToCustomer = 4,
            [Display(Description = "Numune Kargoya Verildi")]
            Shipped = 4,
            [Display(Description = "Numune Müşteriye Ulaştı")]
            DeliveredToCustomer = 5,
            [Display(Description = "Numune Kayıp/Eksik/Hatalı")]
            ErrorSample = 6,
            [Display(Description = "Numune Arşive Kaldırıldı")]
            SendToArchive = 7,
            [Display(Description = "Numune İmha Edildi")]
            Destroyed = 8,
        }
        public enum AnalisysStatus
        {
            [Display(Description = "Başladı")]
            WillBeRefundedToCustomer = 1,
            [Display(Description = "Bitirildi")]
            ToBeArchived = 2,
       
        }

        public enum SampleReturnType
        {
            [Display(Description = "Mişteriye İade Edilecek")]
            WillBeRefundedToCustomer = 1,
            [Display(Description = "Arşivlenecek")]
            ToBeArchived = 2,
            [Display(Description = "İmha Edilecek")]
            ToBeDestroyed = 3,
        }
        public enum SampleAcceptPackaging
        {
            [Display(Description = "Orjinal Ambalaj")]
            OriginalPackage = 1,

            [Display(Description = "Numune Kabı")]
            SampleBowl = 2,
            [Display(Description = "Koli")]
            Parcel = 3,
            [Display(Description = "Diğer")]
            Other = 4,
        }
        public enum SampleAcceptBringingType
        {
            [Display(Description = "Kargo")]
            Cargo = 1,
            [Display(Description = "Elden Teslim")]
            HandDelivery = 1,

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
            [Display(Description = "TRY")]
            TRY = 1,
            [Display(Description = "USD")]
            USD = 2,
            [Display(Description = "EURO")]
            EURO = 3,
            [Display(Description = "GBP")]
            GBP = 4,
            [Display(Description = "RUB")]
            RUB = 5,
            [Display(Description = "CNY")]
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
            try
            {
                return enumType.FirstOrDefault().GetCustomAttribute<DisplayAttribute>()?.Description;
            }
            catch (Exception)
            {

                return "";
            }




        }

    }

}
