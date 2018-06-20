using System.Collections.Generic;
using System.Linq;
using CumulativeDataTriangle;
using Moq;
using NUnit.Framework;

namespace CumulativeDataTriangleTest
{
    [TestFixture]
    public class OutputFormatterTest
    {
        [Test]
        public void CreateOutputContentTest1()
        {
            var productIncrementalValue1 = CreateProductIncrementalValue("Comp", 1992, 1992, 110.0);
            var productIncrementalValue2 = CreateProductIncrementalValue("Comp", 1992, 1993, 170.0);
            var productIncrementalValue3 = CreateProductIncrementalValue("Comp", 1993, 1993, 200.0);

            var productIncrementalValue4 = CreateProductIncrementalValue("Non - Comp", 1990, 1990, 45.2);
            var productIncrementalValue5 = CreateProductIncrementalValue("Non - Comp", 1990, 1991, 64.8);
            var productIncrementalValue6 = CreateProductIncrementalValue("Non - Comp", 1990, 1993, 37.0);
            var productIncrementalValue7 = CreateProductIncrementalValue("Non - Comp", 1991, 1991, 50.0);
            var productIncrementalValue8 = CreateProductIncrementalValue("Non - Comp", 1991, 1992, 75.0);
            var productIncrementalValue9 = CreateProductIncrementalValue("Non - Comp", 1991, 1993, 25.0);
            var productIncrementalValue10 = CreateProductIncrementalValue("Non - Comp", 1992, 1992, 55.0);
            var productIncrementalValue11 = CreateProductIncrementalValue("Non - Comp", 1992, 1993, 85.0);
            var productIncrementalValue12 = CreateProductIncrementalValue("Non - Comp", 1993, 1993, 100.0);

            var productIncrementValues = new List<ProductIncrementalValue>()
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
                productIncrementalValue12
            };


            var mockedCumulativeDataCalculator = new Mock<ICumulativeDataCalculator>();
            mockedCumulativeDataCalculator.Setup(x => x.Calculate(It.Is<Product>(p => p.ProductName == "comp")))
                .Returns(new List<double>() {0, 0, 0, 0, 0, 0, 0, 110, 280, 200});
            mockedCumulativeDataCalculator.Setup(x => x.Calculate(It.Is<Product>(p => p.ProductName == "non - comp")))
              .Returns(new List<double>() {45.2, 110, 110, 147, 50, 125, 150, 55, 140, 100});

            IList<string> expectedOutputList = new List<string>();
            expectedOutputList.Add("1990,4");
            expectedOutputList.Add("comp,0,0,0,0,0,0,0,110,280,200");
            expectedOutputList.Add("non - comp,45.2,110,110,147,50,125,150,55,140,100");

            var outputFormatter = new OutputFormatter(mockedCumulativeDataCalculator.Object);
            CollectionAssert.AreEqual(expectedOutputList, outputFormatter.CreateOutputContent(productIncrementValues));


        }


        [Test]
        public void CreateOutputContentTest2()
        {
            var productIncrementalValue1 = CreateProductIncrementalValue("Comp", 1992, 1992, 110.0);
            var productIncrementalValue2 = CreateProductIncrementalValue("COMP", 1992, 1993, 170.0);
            var productIncrementalValue3 = CreateProductIncrementalValue("ComP", 1993, 1993, 200.0);

            var productIncrementalValue4 = CreateProductIncrementalValue("Non - Comp", 1990, 1990, 45.2);
            var productIncrementalValue5 = CreateProductIncrementalValue("NON - Comp", 1990, 1991, 64.8);
            var productIncrementalValue6 = CreateProductIncrementalValue("Non - Comp", 1990, 1993, 37.0);
            var productIncrementalValue7 = CreateProductIncrementalValue("Non - CoMp", 1991, 1991, 50.0);
            var productIncrementalValue8 = CreateProductIncrementalValue("Non - ComP", 1991, 1992, 75.0);
            var productIncrementalValue9 = CreateProductIncrementalValue("NoN - Comp", 1991, 1993, 25.0);
            var productIncrementalValue10 = CreateProductIncrementalValue("Non - COMp", 1992, 1992, 55.0);
            var productIncrementalValue11 = CreateProductIncrementalValue("Non - Comp", 1992, 1993, 85.0);
            var productIncrementalValue12 = CreateProductIncrementalValue("Non - Comp", 1993, 1993, 100.0);

            var productIncrementValues = new List<ProductIncrementalValue>()
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
                productIncrementalValue12
            };


            var mockedCumulativeDataCalculator = new Mock<ICumulativeDataCalculator>();
            mockedCumulativeDataCalculator.Setup(x => x.Calculate(It.Is<Product>(p => p.ProductName == "comp")))
                .Returns(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 110, 280, 200 });
            mockedCumulativeDataCalculator.Setup(x => x.Calculate(It.Is<Product>(p => p.ProductName == "non - comp")))
              .Returns(new List<double>() { 45.2, 110, 110, 147, 50, 125, 150, 55, 140, 100 });

            IList<string> expectedOutputList = new List<string>();
            expectedOutputList.Add("1990,4");
            expectedOutputList.Add("comp,0,0,0,0,0,0,0,110,280,200");
            expectedOutputList.Add("non - comp,45.2,110,110,147,50,125,150,55,140,100");

            var outputFormatter = new OutputFormatter(mockedCumulativeDataCalculator.Object);
            CollectionAssert.AreEqual(expectedOutputList, outputFormatter.CreateOutputContent(productIncrementValues));


        }

        [Test]
        public void CreateOutputContentTest3()
        {
            var productIncrementalValue1 = CreateProductIncrementalValue("Comp", 1992, 1992, 110.0);
            var productIncrementalValue2 = CreateProductIncrementalValue("COMP", 1992, 1993, 170.0);
            var productIncrementalValue3 = CreateProductIncrementalValue("ComP", 1993, 1993, 200.0);

            var productIncrementalValue4 = CreateProductIncrementalValue("Non - Comp", 1990, 1990, 45.2);
            var productIncrementalValue5 = CreateProductIncrementalValue("NON - Comp", 1990, 1991, 64.8);
            var productIncrementalValue6 = CreateProductIncrementalValue("Non - Comp", 1990, 1993, 37.0);
            var productIncrementalValue7 = CreateProductIncrementalValue("Non - CoMp", 1991, 1991, 50.0);
            var productIncrementalValue8 = CreateProductIncrementalValue("Non - ComP", 1991, 1992, 75.0);
            var productIncrementalValue9 = CreateProductIncrementalValue("NoN - Comp", 1991, 1993, 25.0);
            var productIncrementalValue10 = CreateProductIncrementalValue("Non - COMp", 1992, 1992, 55.0);
            var productIncrementalValue11 = CreateProductIncrementalValue("Non - Comp", 1992, 1993, 85.0);
            var productIncrementalValue12 = CreateProductIncrementalValue("Non - Comp", 1993, 1993, 100.0);

            var productIncrementalValue13 = CreateProductIncrementalValue("Non - Comp1", 1995, 1995, 100);
            var productIncrementalValue14 = CreateProductIncrementalValue("Non - Comp1", 1995, 1996, 50);
            var productIncrementalValue15 = CreateProductIncrementalValue("Non - Comp1", 1995, 1997, 200);

            var productIncrementalValue16 = CreateProductIncrementalValue("Non - Comp1", 1996, 1996, 80.0);
            var productIncrementalValue17 = CreateProductIncrementalValue("Non - Comp1", 1996, 1997, 40.0);
            var productIncrementalValue18 = CreateProductIncrementalValue("Non - Comp1", 1997, 1997, 120.0);


            var productIncrementValues = new List<ProductIncrementalValue>()
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
                productIncrementalValue15,
                productIncrementalValue16,
                productIncrementalValue17,
                productIncrementalValue18
            };


            var mockedCumulativeDataCalculator = new Mock<ICumulativeDataCalculator>();
            mockedCumulativeDataCalculator.Setup(x => x.Calculate(It.Is<Product>(p => p.ProductName == "comp")))
                .Returns(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 110, 280, 200 });
            mockedCumulativeDataCalculator.Setup(x => x.Calculate(It.Is<Product>(p => p.ProductName == "non - comp")))
              .Returns(new List<double>() { 45.2, 110, 110, 147, 50, 125, 150, 55, 140, 100 });
            mockedCumulativeDataCalculator.Setup(x => x.Calculate(It.Is<Product>(p => p.ProductName == "non - comp1")))
             .Returns(new List<double>() { 100, 150, 350, 80, 120, 120 });

            IList<string> expectedOutputList = new List<string>();
            expectedOutputList.Add("1990,8");
            expectedOutputList.Add("comp,0,0,0,0,0,0,0,110,280,200");
            expectedOutputList.Add("non - comp,45.2,110,110,147,50,125,150,55,140,100");
            expectedOutputList.Add("non - comp1,100,150,350,80,120,120");

            var outputFormatter = new OutputFormatter(mockedCumulativeDataCalculator.Object);
            CollectionAssert.AreEqual(expectedOutputList, outputFormatter.CreateOutputContent(productIncrementValues));


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
