using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2ISP11_17_ZeyArt_DanArt.ClassHelper
{
    public static class CalcDebt
    {
        public static double Debt(DateTime dateStart, DateTime dateEnd, double price)
        {
            int dayCount = (dateEnd - dateStart).Days;
            double debt = (dayCount - 30) * 0.01 * price;
            if (debt > 0)
            {
                return debt;
            }
            else
            {
                return 0.00;
            }
        }
    }
}
