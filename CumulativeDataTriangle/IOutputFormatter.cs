using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CumulativeDataTriangle
{
    public interface IOutputFormatter
    {
        /// <summary>
        /// Creates the output as a list of comma separated strings
        /// </summary>
        /// <param name="productIncrementalValues"> List of <see cref="ProductIncrementalValue"/> based on which
        /// the comma separated string needs to be created.</param>
        /// <returns>list of comma separated strings</returns>
        IList<string> CreateOutputContent(IList<ProductIncrementalValue> productIncrementalValues);
    }
}
