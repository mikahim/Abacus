using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;

namespace Abacus.Services
{
    class StringCalculator    {       

        public static string CalculateString(string calculationString)
        {
            string result = new Expression(calculationString).ToString();
            return result;
        }
    }
}
