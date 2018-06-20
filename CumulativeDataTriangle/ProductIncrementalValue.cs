
namespace CumulativeDataTriangle
{
    /// <summary>
    /// Represents each of the incremental data value specified in the inpout file.
    /// </summary>
    public class ProductIncrementalValue
    {
        public string ProductName { get; set; }
        public int OriginYear { get; set; }
        public int DevelopmentYear { get; set; }
        public double IncrementalValue { get; set; }
        
    }
}
