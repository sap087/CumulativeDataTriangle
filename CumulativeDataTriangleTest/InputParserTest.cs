using System.Collections.Generic;
using System.Linq;
using CumulativeDataTriangle;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace CumulativeDataTriangleTest
{
    [TestFixture]
    public class InputParserTest
    {
        [Test]
        public void TestInputParser_IncorrectHeadingOnly_HasEmptyParsedList()
        {
            const string incorrectHeading = "xyz,abc,def,ghi";
            var inputParser = new InputParser();
            Assert.IsEmpty( inputParser.ParseInput(new []{ incorrectHeading}));
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }

        [Test]
        public void TestInputParser_IncorrectHeading_HasEmptyParsedList()
        {
            IList<string> inputTexts = new[]
            {
                "xyz,abc,def,ghi",
                "Comp,1990,1991,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }

        [Test]
        public void TestInputParser_NoHeading_HasEmptyParsedList()
        {
            IList<string> inputTexts = new[]
            {
                "Comp,1990,1991,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }

        [Test]
        public void TestInputParser_TwoRows_ReturnsOneProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1990,1991,310"
            };
            var inputParser = new InputParser();
            var parsedValues = inputParser.ParseInput(inputTexts);
            Assert.That(parsedValues.Count() == 1);
            Assert.That(parsedValues.FirstOrDefault().ProductName == "Comp");
            Assert.That(parsedValues.FirstOrDefault().OriginYear == 1990);
            Assert.That(parsedValues.FirstOrDefault().DevelopmentYear == 1991);
            Assert.That(parsedValues.FirstOrDefault().IncrementalValue == 310);
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }

        [Test]
        public void TestInputParser_HeadingOrderDifferent_ReturnsOneProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Incremental Value,Origin Year,Development Year",
                "Comp,310,1990,1991"
            };
            var inputParser = new InputParser();
            var parsedValues = inputParser.ParseInput(inputTexts);
            Assert.That(parsedValues.Count() == 1);
            Assert.That(parsedValues.FirstOrDefault().ProductName == "Comp");
            Assert.That(parsedValues.FirstOrDefault().OriginYear == 1990);
            Assert.That(parsedValues.FirstOrDefault().DevelopmentYear == 1991);
            Assert.That(parsedValues.FirstOrDefault().IncrementalValue == 310);
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }


        [Test]
        public void TestInputParser_HeadingOrderDifferent_ReturnsOneProductIncrementValue2()
        {
            IList<string> inputTexts = new[]
            {
                "Incremental Value,Origin Year, Product, Development Year",
                "310,1990, Comp, 1991"
            };
            var inputParser = new InputParser();
            var parsedValues = inputParser.ParseInput(inputTexts);
            Assert.That(parsedValues.Count() == 1);
            Assert.That(parsedValues.FirstOrDefault().ProductName == "Comp");
            Assert.That(parsedValues.FirstOrDefault().OriginYear == 1990);
            Assert.That(parsedValues.FirstOrDefault().DevelopmentYear == 1991);
            Assert.That(parsedValues.FirstOrDefault().IncrementalValue == 310);
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }

        [Test]
        public void TestInputParser_OriginSameAsDevYear_ReturnsOneProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1991,1991,310"
            };
            var inputParser = new InputParser();
            var parsedValues = inputParser.ParseInput(inputTexts);
            Assert.That(parsedValues.Count() == 1);
            Assert.That(parsedValues.FirstOrDefault().ProductName == "Comp");
            Assert.That(parsedValues.FirstOrDefault().OriginYear == 1991);
            Assert.That(parsedValues.FirstOrDefault().DevelopmentYear == 1991);
            Assert.That(parsedValues.FirstOrDefault().IncrementalValue == 310);
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }


        [Test]
        public void TestInputParser_OneValidAndInvalidInput_ReturnsOneProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1991,1991,310",
                "Comp,1191,310"
            };
            var inputParser = new InputParser();
            var parsedValues = inputParser.ParseInput(inputTexts);
            Assert.That(parsedValues.Count() == 1);
            Assert.That(parsedValues.FirstOrDefault().ProductName == "Comp");
            Assert.That(parsedValues.FirstOrDefault().OriginYear == 1991);
            Assert.That(parsedValues.FirstOrDefault().DevelopmentYear == 1991);
            Assert.That(parsedValues.FirstOrDefault().IncrementalValue == 310);
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[2]));
        }


        [Test]
        public void TestInputParser_OriginYearGreaterThanDevYear_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1991,1990,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }

        [Test]
        public void TestInputParser_OriginGreaterThanCurrent_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,2020,1990,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }


        [Test]
        public void TestInputParser_OriginLowerThanMin_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1800,1990,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }

        [Test]
        public void TestInputParser_DevYearGreaterThanCurrent_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1990,2020,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }


        [Test]
        public void TestInputParser_ProductIsEmptyString_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                ",1990,2020,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }

        [Test]
        public void TestInputParser_MissingColumn_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "1990,2020,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }

        [Test]
        public void TestInputParser_MissingColumnValue_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1990,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }

        [Test]
        public void TestInputParser_OriginYearNotInt_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,xyz,1990,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }


        [Test]
        public void TestInputParser_DevYearNotInt_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1990,xyz,310"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }

        [Test]
        public void TestInputParser_IncrementValNotDouble_ReturnsNoProductIncrementValue()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1990,1991,xyz"
            };
            var inputParser = new InputParser();
            Assert.IsEmpty(inputParser.ParseInput(inputTexts));
            Assert.That(inputParser.InvalidInputRows.Count() == 1);
            Assert.That(inputParser.InvalidInputRows.Contains(inputTexts[1]));
        }


        [Test]
        public void TestInputParser_MultipleValidInput_ReturnsCorrectProductIncrementValues()
        {
            IList<string> inputTexts = new[]
            {
                "Product,Origin Year,Development Year,Incremental Value",
                "Comp,1992,1992,110.0",
                "Comp,1992,1993,170.0",
                "Comp,1993,1993,200.0",
                "Non-Comp, 1990, 1990, 45.2",
                "Non-Comp, 1990, 1991, 64.8",
                "Non-Comp, 1990, 1993, 37.0",
                "Non-Comp, 1991, 1991, 50.0",
                "Non-Comp, 1991, 1992, 75.0",
                "Non-Comp, 1991, 1993, 25.0",
                "Non-Comp, 1992, 1992, 55.0",
                "Non-Comp, 1992, 1993, 85.0",
                "Non-Comp, 1993, 1993, 100.0"
            };
            var inputParser = new InputParser();
            var parsedValues = inputParser.ParseInput(inputTexts);
            Assert.That(parsedValues.Count() == 12);
            Assert.IsEmpty(inputParser.InvalidInputRows);
        }
    }
}
