using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObjects.Utilities
{
    public static class IpTransformer
    {
        public static string BytesToString(byte[] bytes)
        {
            string result = "";
            for (int i = 0; i < bytes.Length && i < 4; i++)
            {
                result += Convert.ToInt32(bytes[i]).ToString();
                if (i < 3)
                {
                    result += ".";
                }    
            }
            return result;

        }

        public static byte[] StringToBytes(string ip)
        {
            byte[] result = new byte[4] { 0, 0, 0, 0};
            try
            {
                var stringBytes = ip.Split(".");
                if (stringBytes.Length == 4)
                {
                    var i = 0;
                    foreach (var subByte in stringBytes)
                    {
                        result[i] = byte.Parse(subByte);
                        i++;
                    }
                }
            }
            catch { }
            return result;
        }
    }
}
