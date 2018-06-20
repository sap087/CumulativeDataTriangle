using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CumulativeDataTriangle
{
    /// <summary>
    /// Converts from incremental data triangle to cumulative data triangle.
    /// </summary>
    public class DataTriangleConverter
    {
        private const string Invalidincrementalvalues = "InvalidIncrementalValues";
        private readonly IOutputFormatter _myOutputFormatter;
        private readonly IFileOperation _myfileOperation;
        private readonly IInputParser _myInputParser;

        public DataTriangleConverter(IFileOperation fileOperation, IInputParser inputParser, IOutputFormatter outputFormatter)
        {
            _myOutputFormatter = outputFormatter;
            _myfileOperation = fileOperation;
            _myInputParser = inputParser;
        }
        
        /// <summary>
        /// Reads the input file which is a comma separated text file containing incremental data triangle and
        /// converts it to cumulative data triangle and writes this to a file.
        /// </summary>
        /// <param name="inputFileName">Input file containing the incremental data triangle</param>
        /// <param name="outputFileName">Output file containing the cumulative data triangle</param>
        /// <returns>boolean indicating if the conversion was successfully done. </returns>
        public bool ConvertDataTriangle(string inputFileName, string outputFileName)
        {
            IList<string> input = GetInput(inputFileName);
            
            IList<ProductIncrementalValue> parsedIncrementalValues = _myInputParser.ParseInput(input);
            IList<string> outputContent = _myOutputFormatter.CreateOutputContent(parsedIncrementalValues);

            try
            {
                WriteOutputFile(outputFileName, outputContent);
            }
            catch (AccessViolationException)
            {
                return false;
            }
          
            if (_myInputParser.InvalidInputRows.Any())
            {
                WriteInvalidInputValues(outputFileName);
                return false;
            }
            return true;
        }

        private void WriteInvalidInputValues(string outputFileName)
        {
            string invalidRowsFileName = _myfileOperation.GetFileNameInSameDirAsOutput(outputFileName,
                Invalidincrementalvalues);
            WriteOutputFile(invalidRowsFileName, _myInputParser.InvalidInputRows);
        }

        private bool WriteOutputFile(string outputFileName, IEnumerable<string> outputContent)
        {
            return _myfileOperation.WriteToFile(outputFileName, outputContent);
        }


        private IList<string> GetInput(string fileName)
        {
            return _myfileOperation.ReadFile(fileName);
        }
    }
}
