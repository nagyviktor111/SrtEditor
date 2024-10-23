namespace SrtEditor.Domain.TimeEditor
{
    public class TimeEditorOptions
    {
        public string FilePath { get; set; } = string.Empty;

        public double Delay { get; set; }

        public TimeScaleDifference TimeScaleDifference { get; set; } = new TimeScaleDifference();

        public CreatedFileLocation CreatedFileLocation { get; set; }
    }
}
