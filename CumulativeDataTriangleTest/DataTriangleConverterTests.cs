using System.Collections.Generic;
using CumulativeDataTriangle;
using Moq;
using NUnit.Framework;

namespace CumulativeDataTriangleTest
{
    [TestFixture]
    class DataTriangleConverterTests
    {
        private const string OutputFileName = "outputFile";
        private const string InputFileName = "inputFile";
        private Mock<IFileOperation> _mockedFileOperation;
        private Mock<IInputParser> _mockedInputParser;
        private Mock<IOutputFormatter> _mockedOutputFormatter;

        [SetUp]
        public void Setup()
        {
            _mockedFileOperation = new Mock<IFileOperation>();
            _mockedInputParser = new Mock<IInputParser>();
            _mockedOutputFormatter = new Mock<IOutputFormatter>();
        }


        [Test]
        public void ConvertDataTriangle_CorrectIncrementValues_CallsWrite()
        {
            var parsedIncrementalValues = new List<ProductIncrementalValue>();
            var expectedOutputContent = new List<string>()
            {
                "SomeString1",
                "SomeString2"
            };
            _mockedInputParser.Setup(x => x.ParseInput(It.IsAny<IList<string>>())).Returns(parsedIncrementalValues);
            _mockedInputParser.SetupGet(x => x.InvalidInputRows).Returns(new List<string>());
            _mockedOutputFormatter.Setup(x => x.CreateOutputContent(parsedIncrementalValues))
                .Returns(expectedOutputContent);

            var converter = new DataTriangleConverter(_mockedFileOperation.Object,
                _mockedInputParser.Object, _mockedOutputFormatter.Object);
            converter.ConvertDataTriangle(InputFileName, OutputFileName);

            _mockedFileOperation.Verify(fileOp => fileOp.WriteToFile(OutputFileName, expectedOutputContent), Times.Once());

        }


        [Test]
        public void ConvertDataTriangle_WithInvalidRows_WritesFileWithInvalidRows()
        {
            var parsedIncrementalValues = new List<ProductIncrementalValue>();
            var expectedOutputContent = new List<string>()
            {
                "SomeString1",
                "SomeString2"
            };

            var invalidRow = new List<string>() {"SomeString"};
               
            _mockedInputParser.Setup(x => x.ParseInput(It.IsAny<IList<string>>())).Returns(parsedIncrementalValues);
            _mockedInputParser.SetupGet(x => x.InvalidInputRows).Returns(invalidRow);
            _mockedOutputFormatter.Setup(x => x.CreateOutputContent(parsedIncrementalValues))
                .Returns(expectedOutputContent);
            const string invalidFile = "InvalidFile";
            _mockedFileOperation.Setup(fileOp => fileOp.GetFileNameInSameDirAsOutput(OutputFileName, "InvalidIncrementalValues")).Returns(invalidFile);

            var converter = new DataTriangleConverter(_mockedFileOperation.Object,
                _mockedInputParser.Object, _mockedOutputFormatter.Object);
            converter.ConvertDataTriangle(InputFileName, OutputFileName);

            _mockedFileOperation.Verify(fileOp => fileOp.WriteToFile(invalidFile, invalidRow), Times.Once());
            _mockedFileOperation.Verify(fileOp => fileOp.WriteToFile(OutputFileName, expectedOutputContent), Times.Once());

        }
    }
}
