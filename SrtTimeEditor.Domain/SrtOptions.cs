namespace SrtTimeEditor.Domain
{
    public class SrtOptions
    {
        public string FilePaths { get; set; } = string.Empty;

        public double Delay { get; set; }

        public TimeScaleDifference TimeScaleDifference { get; set; } = new TimeScaleDifference();

        public CreatedFileLocation CreatedFileLocation { get; set; }
    }
}
