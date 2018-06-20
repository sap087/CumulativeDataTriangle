using System;
using System.Collections.Generic;
using System.IO;

namespace CumulativeDataTriangle
{
    class FileOperations : IFileOperation
    {
        /// <summary>
        /// <see cref="IsFileExists"/>
        /// </summary>
        public bool IsFileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// <see cref="IFileOperation.ReadFile"/>
        /// </summary>
        public IList<string> ReadFile(string fileName)
        {
            return File.ReadAllLines(fileName);
        }

        /// <summary>
        /// <see cref="IFileOperation.WriteToFile"/>
        /// </summary>
        public bool WriteToFile(string fileName, IEnumerable<string> contents)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            try
            {
                File.WriteAllLines(fileName, contents);
            } 
            catch(Exception)
            {
                var message = string.Format("Could not write file {0}", fileName);
                throw new AccessViolationException(message);
            }
            return true;
        }

        /// <summary>
        /// <see cref="IFileOperation.GetFileNameInSameDirAsOutput"/>
        /// </summary>
        public string GetFileNameInSameDirAsOutput(string outputFileName, string newFileName)
        {
            return Path.GetDirectoryName(outputFileName) + "\\" + newFileName + Path.GetExtension(outputFileName);
        }
    }
}
