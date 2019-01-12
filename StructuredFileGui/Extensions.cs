using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuredFileGui
{
    public static class Extensions
    {
        public static SolidBrush ToSolidBrush(this string hexCode)
        {
            var hexString = hexCode.Replace("#", string.Empty);

            if (string.IsNullOrWhiteSpace(hexString)) throw new FormatException();
            if (hexString.Length != 6 && hexString.Length != 8) throw new FormatException();

            try
            {
                var a = hexString.Length == 8 ? hexString.Substring(0, 2) : "ff";
                var r = hexString.Length == 8 ? hexString.Substring(2, 2) : hexString.Substring(0, 2);
                var g = hexString.Length == 8 ? hexString.Substring(4, 2) : hexString.Substring(2, 2);
                var b = hexString.Length == 8 ? hexString.Substring(6, 2) : hexString.Substring(4, 2);

                return new SolidBrush(Color.FromArgb(
                    byte.Parse(a, System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(r, System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(g, System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(b, System.Globalization.NumberStyles.HexNumber)));
            }
            catch
            {
                throw new FormatException();
            }
        }
    }
}
