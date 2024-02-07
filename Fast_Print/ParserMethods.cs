using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast_Print
{
    internal class ParserMethods()
    {
        public bool RemoveTrailingDashAndNumber(string partNumber)
        {
            if (partNumber.EndsWith("-"))
            {
                partNumber = partNumber.Substring(0, partNumber.Length - 1);
                return true;
            }
            return false;
        }





    }
}
