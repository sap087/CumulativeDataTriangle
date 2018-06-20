using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CumulativeDataTriangle
{
    /// <summary>
    /// /// <summary>
    /// Interface to caluculat the Cumulative data triangle from the
    /// input incremental data triangle for each of the product.
    /// </summary>
    /// </summary>
    public interface ICumulativeDataCalculator
    {
        /// <summary>
        /// Calculates the Cumulative data triangle for the give <param name="product"></param>
        /// </summary>
        /// <param name="product"><see cref="Product"/> whose Cumulative data triangle needs to be calculated</param>
        /// <returns> List of calculated double values for each of the development years</returns>
        IList<double> Calculate(Product product);
    }
}
