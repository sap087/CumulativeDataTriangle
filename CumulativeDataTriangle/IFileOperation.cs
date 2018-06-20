using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CumulativeDataTriangle
{
    public interface IFileOperation
    {
        /// <summary>
        /// Checks if the file exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool IsFileExists(string fileName);

        /// <summary>
        /// Reads each line of the file with the given name. 
        /// </summary>
        /// <param name="fileName">name of the file to be read</param>
        /// <returns>each line of the file as a list. if the file does not exist throws <see cref="FileNotFoundException"/></returns>
        IList<string> ReadFile(string fileName);

        /// <summary>
        ///Creates the file with filename <param name="fileName"></param> and Writes the <param name="contents"> to the file</param>
        /// </summary>
        /// <param name="fileName">name of the filename where the <see cref="contents"/> needs to be written</param>
        /// <param name="contents">List of string to be written into the <see cref="fileName"/></param>
        /// <returns>bool indicating there was an error writing the file</returns>
        bool WriteToFile(string fileName, IEnumerable<string> contents);

        /// <summary>
        /// Gets the newFileName path with same ext and path as <param name="outputFileName"> </param>
        /// </summary>
        /// <param name="outputFileName"></param>
        /// <param name="newFileName"></param>
        /// <returns></returns>
        string GetFileNameInSameDirAsOutput(string outputFileName, string newFileName);
    }
}
