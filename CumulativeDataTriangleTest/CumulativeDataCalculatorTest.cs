using System.Collections.Generic;
using CumulativeDataTriangle;
using NUnit.Framework;


namespace CumulativeDataTriangleTest
{
    [TestFixture]
    public class CumulativeDataCalculatorTest
    {
        [Test]
        public void TestGetCumulativeDataTriangle_EmptyIncrementalData_ReturnsEmptyList()
        {
            var product = CreateProduct("Comp", 1990, 4, new List<ProductIncrementalValue>());
            CumulativeDataCalculator cumulativeDataTriangle = new CumulativeDataCalculator();
            Assert.IsEmpty(cumulativeDataTriangle.Calculate(product));
        }

        [Test]
        public void TestGetCumulativeDataTriangle2()
        {
            var productIncrementalValue = CreateProductIncrementalValue("Comp", 1993, 1993, 45.2);
            var expectedCumulativeTriange = new List<double>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 45.2};
            var product = CreateProduct("Comp", 1990, 4, new List<ProductIncrementalValue>() {productIncrementalValue});
            CumulativeDataCalculator cumulativeDataTriangle = new CumulativeDataCalculator();
            CollectionAssert.AreEqual(expectedCumulativeTriange, cumulativeDataTriangle.Calculate(product));
        }


        [Test]
        public void TestGetCumulativeDataTriangle3()
        {
            var productIncrementalValue1 = CreateProductIncrementalValue("Comp", 1992, 1992, 110.0);
            var productIncrementalValue2 = CreateProductIncrementalValue("Comp", 1992, 1993, 170.0);
            var productIncrementalValue3 = CreateProductIncrementalValue("Comp", 1993, 1993, 200.0);

            var expectedCumulativeTriange = new List<double>() {0, 0, 0, 0, 0, 0, 0, 110, 280, 200};
            var product = CreateProduct("Comp", 1990, 4,
                new List<ProductIncrementalValue>()
                {
                    productIncrementalValue1,
                    productIncrementalValue2,
                    productIncrementalValue3
                });
            CumulativeDataCalculator cumulativeDataTriangle = new CumulativeDataCalculator();
            CollectionAssert.AreEqual(expectedCumulativeTriange, cumulativeDataTriangle.Calculate(product));
        }

        [Test]
        public void TestGetCumulativeDataTriangle4()
        {
            var productIncrementalValue1 = CreateProductIncrementalValue("Non - Comp", 1990, 1990, 45.2);
            var productIncrementalValue2 = CreateProductIncrementalValue("Non - Comp", 1990, 1991, 64.8);
            var productIncrementalValue3 = CreateProductIncrementalValue("Non - Comp", 1990, 1993, 37.0);

            var productIncrementalValue4 = CreateProductIncrementalValue("Non - Comp", 1991, 1991, 50.0);
            var productIncrementalValue5 = CreateProductIncrementalValue("Non - Comp", 1991, 1992, 75.0);
            var productIncrementalValue6 = CreateProductIncrementalValue("Non - Comp", 1991, 1993, 25.0);

            var productIncrementalValue7 = CreateProductIncrementalValue("Non - Comp", 1992, 1992, 55.0);
            var productIncrementalValue8 = CreateProductIncrementalValue("Non - Comp", 1992, 1993, 85.0);
            var productIncrementalValue9 = CreateProductIncrementalValue("Non - Comp", 1993, 1993, 100.0);

            var expectedCumulativeTriange = new List<double>() {45.2, 110, 110, 147, 50, 125, 150, 55, 140, 100};
            var product = CreateProduct("Non - Comp", 1990, 4,
                new List<ProductIncrementalValue>()
                {
                    productIncrementalValue1,
                    productIncrementalValue2,
                    productIncrementalValue3,
                    productIncrementalValue4,
                    productIncrementalValue5,
                    productIncrementalValue6,
                    productIncrementalValue7,
                    productIncrementalValue8,
                    productIncrementalValue9
                });
            CumulativeDataCalculator cumulativeDataTriangle = new CumulativeDataCalculator();
            CollectionAssert.AreEqual(expectedCumulativeTriange, cumulativeDataTriangle.Calculate(product));
        }

        [Test]
        public void TestGetCumulativeDataTriangle5()
        {
            var productIncrementalValue1 = CreateProductIncrementalValue("Non - Comp", 1995, 1995, 100);
            var productIncrementalValue2 = CreateProductIncrementalValue("Non - Comp", 1995, 1996, 50);
            var productIncrementalValue3 = CreateProductIncrementalValue("Non - Comp", 1995, 1997, 200);

            var productIncrementalValue4 = CreateProductIncrementalValue("Non - Comp", 1996, 1996, 80.0);
            var productIncrementalValue5 = CreateProductIncrementalValue("Non - Comp", 1996, 1997, 40.0);
            var productIncrementalValue6 = CreateProductIncrementalValue("Non - Comp", 1997, 1997, 120.0);

            var expectedCumulativeTriange = new List<double>() { 100, 150, 350, 80, 120, 120};
            var product = CreateProduct("Non - Comp", 1995, 3,
                new List<ProductIncrementalValue>()
                {
                    productIncrementalValue1,
                    productIncrementalValue2,
                    productIncrementalValue3,
                    productIncrementalValue4,
                    productIncrementalValue5,
                    productIncrementalValue6
                });

            CumulativeDataCalculator cumulativeDataTriangle = new CumulativeDataCalculator();
            CollectionAssert.AreEqual(expectedCumulativeTriange, cumulativeDataTriangle.Calculate(product));
        }


        [Test]
        public void TestGetCumulativeDataTriangle6()
        {
            var productIncrementalValue1 = CreateProductIncrementalValue("Non - Comp", 1990, 1990, 45.2);
            var productIncrementalValue2 = CreateProductIncrementalValue("Non - Comp", 1990, 1991, 64.8);
            var productIncrementalValue3 = CreateProductIncrementalValue("Non - Comp", 1990, 1993, 37.0);

            var productIncrementalValue4 = CreateProductIncrementalValue("Non - Comp", 1991, 1991, 50.0);
            var productIncrementalValue5 = CreateProductIncrementalValue("Non - Comp", 1991, 1992, 75.0);
            var productIncrementalValue6 = CreateProductIncrementalValue("Non - Comp", 1991, 1993, 25.0);

            var productIncrementalValue7 = CreateProductIncrementalValue("Non - Comp", 1992, 1992, 55.0);
            var productIncrementalValue8 = CreateProductIncrementalValue("Non - Comp", 1992, 1993, 85.0);
            var productIncrementalValue9 = CreateProductIncrementalValue("Non - Comp", 1993, 1993, 100.0);

            var productIncrementalValue10 = CreateProductIncrementalValue("Non - Comp", 1995, 1995, 100);
            var productIncrementalValue11 = CreateProductIncrementalValue("Non - Comp", 1995, 1996, 50);
            var productIncrementalValue12 = CreateProductIncrementalValue("Non - Comp", 1995, 1997, 200);

            var productIncrementalValue13 = CreateProductIncrementalValue("Non - Comp", 1996, 1996, 80.0);
            var productIncrementalValue14 = CreateProductIncrementalValue("Non - Comp", 1996, 1997, 40.0);
            var productIncrementalValue15 = CreateProductIncrementalValue("Non - Comp", 1997, 1997, 120.0);


            var expectedCumulativeTriange = new List<double>()
            {
                45.2, 110, 110, 147, 147, 147, 147, 147,
                50, 125, 150, 150, 150, 150, 150,
                55, 140, 140, 140, 140, 140,
                100, 100, 100, 100, 100,
                0, 0, 0, 0,
                100, 150, 350,
                80, 120,
                120
            };
            var product = CreateProduct("Non - Comp", 1990, 8,
                new List<ProductIncrementalValue>()
                {
                    productIncrementalValue1,
                    productIncrementalValue2,
                    productIncrementalValue3,
                    productIncrementalValue4,
                    productIncrementalValue5,
                    productIncrementalValue6,
                    productIncrementalValue7,
                    productIncrementalValue8,
                    productIncrementalValue9,
                    productIncrementalValue10,
                    productIncrementalValue11,
                    productIncrementalValue12,
                    productIncrementalValue13,
                    productIncrementalValue14,
                    productIncrementalValue15
                });
            CumulativeDataCalculator cumulativeDataTriangle = new CumulativeDataCalculator();
            CollectionAssert.AreEqual(expectedCumulativeTriange, cumulativeDataTriangle.Calculate(product));
        }

        private static Product CreateProduct(string productName, int earliestOriginYear, int noOfDevYear,
            IEnumerable<ProductIncrementalValue> productIncrementalValues)
        {
            var product = new Product()
            {
                ProductName = productName,
                EarliestOriginYear = earliestOriginYear,
                NoOfDevelopmentYears = noOfDevYear,
                ProductIncrementalValues = productIncrementalValues
            };
            return product;
        }

        private static ProductIncrementalValue CreateProductIncrementalValue(string productName, int originYear, int devYear, double incrementalValue)
        {
            var productIncrementalValue = new ProductIncrementalValue
            {
                ProductName = productName, 
                OriginYear = originYear,
                DevelopmentYear = devYear,
                IncrementalValue = incrementalValue
            };
            return productIncrementalValue;
        }
    }
}










    
