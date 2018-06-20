using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;


namespace CumulativeDataTriangle
{
    /// <summary>
    /// Class that represents Product in the given input file
    /// </summary>
    public class Product
    {
        public int EarliestOriginYear { get; set; }
        public int NoOfDevelopmentYears { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<ProductIncrementalValue> ProductIncrementalValues { get; set; }
    }


}
