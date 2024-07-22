using System;
using System.Collections.Generic;
using System.Text;

namespace PAKDial.Common
{
    public static class CommonNoGen
    {
        //Generate Four Digit Random No
        public static int GenerateFourDigitRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}
