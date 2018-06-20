using System;
using System.Collections.Generic;
using System.Linq;

namespace CumulativeDataTriangle
{
    public class OutputFormatter : IOutputFormatter
    {
        private readonly ICumulativeDataCalculator myCumulativeDataCalculator;

        public OutputFormatter(ICumulativeDataCalculator cumulativeDataCalculator)
        {
            myCumulativeDataCalculator = cumulativeDataCalculator;
        }

        /// <summary>
        /// <see cref="IOutputFormatter.CreateOutputContent"/>
        /// </summary>        
        public IList<string> CreateOutputContent(IList<ProductIncrementalValue> productIncrementalValues)
        {
            //Calculate earliest origin year and No of development years
            IEnumerable<int> originYears = productIncrementalValues.Select(x => x.OriginYear).Distinct().OrderBy(x => x);
            var earliestOriginYear = originYears.First();
            int noOfDevelopmentYears = originYears.Last() - earliestOriginYear + 1;
           
            //Get first line of output
            IList<string> outputList = new List<string>();
            outputList.Add(GetFirstRow(earliestOriginYear, noOfDevelopmentYears));

            //Get output string foreach product
            var products = GetProducts(productIncrementalValues, earliestOriginYear, noOfDevelopmentYears);
            foreach (Product product in products)
            {
                outputList.Add(GetRowForEachProduct(product));
            }
            return outputList;
        }

        private string GetFirstRow(int earliestOriginYear, int noOfDevelopmentYears)
        {
            return string.Format("{0},{1}", earliestOriginYear, noOfDevelopmentYears);
        }

        private IEnumerable<Product> GetProducts(IList<ProductIncrementalValue> productIncrementalValues, int earliestOriginYear, int noOfDevelopmentYears)
        {
            IEnumerable<string> productNames = productIncrementalValues.Select(x => x.ProductName.ToLowerInvariant()).Distinct();

            //Create a list of products
            return productNames.Select(productName => new Product()
            {
                ProductName = productName,
                EarliestOriginYear = earliestOriginYear,
                NoOfDevelopmentYears = noOfDevelopmentYears,
                ProductIncrementalValues = productIncrementalValues.Where(x => String.Compare(x.ProductName, productName, StringComparison.OrdinalIgnoreCase) == 0)
            });
        } 

        private string GetRowForEachProduct(Product product)
        {
            IList<double> cumulativeData = myCumulativeDataCalculator.Calculate(product);
            return string.Format("{0},{1}", product.ProductName, string.Join(",", cumulativeData));
        }
    }
}
