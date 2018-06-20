using System;
using System.Collections.Generic;
using System.Linq;

namespace CumulativeDataTriangle
{
    /// <summary>
    /// This class is responsible for parsing the input comma separated values
    /// </summary>
    public class InputParser : IInputParser
    {
        private const int MinimumYear = 1900;
        private const int HeadingIndex = 0;

        /// <summary>
        /// Class to map the heading in the file to corresponding values in the row
        /// </summary>
        private class ColumnIndexes
        {
            public int ProductNameIndex { get; set; }
            public int OriginYearIndex { get; set; }
            public int DevelopmentYearIndex { get; set; }
            public int IncrementalValueIndex { get; set; }
        }

        /// <summary>
        /// ctor
        /// </summary>
        public InputParser()
        {
            InvalidInputRows = new List<string>();
        }

        /// <summary>
        /// This list contains the line which has invalid input in the comma separated text
        /// </summary>
        public IList<string> InvalidInputRows { get; private set; }

        /// <summary>
        /// <see cref="IInputParser.ParseInput"/>
        /// </summary>
        public IList<ProductIncrementalValue> ParseInput(IList<string> commaSeparatedTexts)
        {
            var headings = SplitRow(commaSeparatedTexts.ElementAt(HeadingIndex)).Select(x => x.ToLowerInvariant()).ToList();
            var columnIndexes = CreateColumnIndexes(headings);

            if (columnIndexes.ProductNameIndex == -1 || columnIndexes.OriginYearIndex == -1 ||
                columnIndexes.DevelopmentYearIndex == -1 || columnIndexes.IncrementalValueIndex == -1)
            {
                return new List<ProductIncrementalValue>();
            }

            IList<ProductIncrementalValue> parsedIncrementalValues = new List<ProductIncrementalValue>();
            foreach (var commaSeparatedText in commaSeparatedTexts.Skip(1))
            {
                var incrementalValue = CreateProductIncrementalValue(commaSeparatedText, headings.Count, columnIndexes);
                if (incrementalValue != null)
                {
                    parsedIncrementalValues.Add(incrementalValue);
                }
            }
            return parsedIncrementalValues;
        }
        
        private ProductIncrementalValue CreateProductIncrementalValue(string row, int noOfRowValues, ColumnIndexes columnIndexes)
        {
            var rowValues = SplitRow(row);
            if (rowValues.Count() != noOfRowValues)
            {
                InvalidInputRows.Add(row);
                return null;
            }
            string productName = rowValues[columnIndexes.ProductNameIndex];

            int originYear;
            if (!int.TryParse(rowValues[columnIndexes.OriginYearIndex], out originYear))
            {
                InvalidInputRows.Add(row);
                return null;
            }

            int developmentYear;
            if (!int.TryParse(rowValues[columnIndexes.DevelopmentYearIndex], out developmentYear))
            {
                InvalidInputRows.Add(row);
                return null;
            }

            double incrementalValue;
            if (!double.TryParse(rowValues[columnIndexes.IncrementalValueIndex], out incrementalValue))
            {
                InvalidInputRows.Add(row);
                return null;
            }

            if (!ValidateParameters(productName, originYear, developmentYear))
            {
                InvalidInputRows.Add(row);
                return null;
            }

            var productIncrementalValue = new ProductIncrementalValue
            {
                ProductName = productName,
                OriginYear = originYear,
                DevelopmentYear = developmentYear,
                IncrementalValue = incrementalValue
            };
            return productIncrementalValue;
        }

        private static ColumnIndexes CreateColumnIndexes(IList<string> headings)
        {
            return new ColumnIndexes
            {
                ProductNameIndex = headings.IndexOf("product"),
                OriginYearIndex = headings.IndexOf("origin year"),
                DevelopmentYearIndex = headings.IndexOf("development year"),
                IncrementalValueIndex = headings.IndexOf("incremental value")
            };
        }

        private static IList<string> SplitRow(string row)
        {
            return row.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
        }

        private static bool ValidateParameters(string name, int originYear, int developmentYear)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            if (originYear <= MinimumYear || originYear > DateTime.Now.Year)
            {
                return false;
            }
            return developmentYear <= DateTime.Now.Year && developmentYear >= originYear;
        }
    }
}
