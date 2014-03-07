using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public static class RandomExtensions
    {

        public static double Exponential(this Random r, double lambda)
        {
            return Math.Log(1 - r.NextDouble())/-lambda;
        }

        public static double Uniform(this Random r, double min, double max)
        {
            return min + r.NextDouble()*(max - min);
        }
    }
}
