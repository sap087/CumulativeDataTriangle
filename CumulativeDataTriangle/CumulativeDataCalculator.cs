using System.Collections.Generic;
using System.Linq;

namespace CumulativeDataTriangle
{
    /// <summary>
    /// This class is responible for caluculating the Cumulative data triangle from the
    /// input incremental data triangle.
    /// </summary>
    public class CumulativeDataCalculator : ICumulativeDataCalculator
    {
        /// <summary>
        /// <see cref="ICumulativeDataCalculator.Calculate"/>
        /// </summary>
        public IList<double> Calculate(Product product)
        {
            IList<double> cumulativeDataTriangle = new List<double>();
            if (!product.ProductIncrementalValues.Any())
            {
                return new List<double>();
            }

            var cumulativeClaimsData = new double[product.NoOfDevelopmentYears, product.NoOfDevelopmentYears];
            for (int i = 0; i < product.NoOfDevelopmentYears ; i++)
            {
                var incrementalValues = product.ProductIncrementalValues.Where(v => v.OriginYear == product.EarliestOriginYear + i).ToList();
                for (int j = 0; j < product.NoOfDevelopmentYears - i; j++)
                {
                    double incrementalValue = incrementalValues.Where(val => val.DevelopmentYear == val.OriginYear + j).Select(x => x.IncrementalValue).FirstOrDefault();
                    cumulativeClaimsData[i, j] = incrementalValue + cumulativeClaimsData[i, (j-1) < 0 ? j : j -1];
                    cumulativeDataTriangle.Add(cumulativeClaimsData[i,j]);
                }
            }
            return cumulativeDataTriangle;
        }
    }
}
