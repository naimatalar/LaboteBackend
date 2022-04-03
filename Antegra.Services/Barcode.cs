using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Services
{
   public static class Barcode
    {
        public static string Generate(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                return "";
            }

            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            Image img = b.Encode(BarcodeLib.TYPE.UPCA, barcode, Color.Black, Color.White, 290, 120);
        
            using (MemoryStream ms = new MemoryStream())
            {
                //The Image is drawn based on length of Barcode text.
                img.Save(ms, ImageFormat.Png);

                //The Image is finally converted to Base64 string.
                return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

        
    }
}
