using System;

namespace CumulativeDataTriangle
{
    class Program
    {
        private static void Main(string[] args)
        {
            IFileOperation fileOperation = new FileOperations();
            IInputParser inputParser = new InputParser();
            IOutputFormatter outputFormatter = new OutputFormatter(new CumulativeDataCalculator());

            Console.WriteLine("Please enter the input file name of the Incremental Data Triangle");
            string inputFileName = Console.ReadLine();
            if (!fileOperation.IsFileExists(inputFileName))
            {
                Console.WriteLine("Could not find the file entered! Try again.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Please enter an output file name");
            string outputFileName = Console.ReadLine();

            var dataTriangleConverter = new DataTriangleConverter(fileOperation, inputParser, outputFormatter);

            if (!dataTriangleConverter.ConvertDataTriangle(inputFileName, outputFileName))
            {
                Console.WriteLine("Conversion failed...");
                Console.WriteLine("This could be because input data may contain some invalid values");
                Console.WriteLine(
                    "Please check the InvalidIncrementalValues file in the same directory as output file entered. ");
                Console.ReadKey();
            }
        }

    }
}
