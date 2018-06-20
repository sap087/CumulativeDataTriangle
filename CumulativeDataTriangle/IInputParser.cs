using System.Collections.Generic;

namespace CumulativeDataTriangle
{
    public interface IInputParser 
    {
        /// <summary>
        /// IList of rows in the input file with invalid or incorrect values. This list is filled only
        /// after <see cref="ParseInput"/> method is called.
        /// </summary>
        IList<string> InvalidInputRows { get; }

        /// <summary>
        /// Parses the list of string which is a comma separated string and creates <see cref="ProductIncrementalValue"/>
        /// for each of the string.
        /// </summary>
        /// <param name="commaSeparatedTexts">list of comma separated strings</param>
        /// <returns>List of <see cref="ProductIncrementalValue"/> for each of the parsed comma separated string.</returns>
        IList<ProductIncrementalValue> ParseInput(IList<string> commaSeparatedTexts);
    }
}
