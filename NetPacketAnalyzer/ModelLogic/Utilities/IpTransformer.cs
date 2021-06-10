using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLogic.Utilities
{
    public static class IpTransformer
    {
        public static string MaskToString(byte[] bytes)
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
    }
}
