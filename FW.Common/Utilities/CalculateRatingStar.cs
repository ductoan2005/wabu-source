using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common.Utilities
{
    public static class CalculateRatingStar
    {
        public static double GetRating(int star1, int star2, int star3, int star4, int star5)
        {
            double rating = (double)(5 * star5 + 4 * star4 + 3 * star3 + 2 * star2 + 1 * star1) / (star1 + star2 + star3 + star4 + star5);

            rating = Math.Round(rating, 1);

            return rating;
        }
    }
}
