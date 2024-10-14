using SrtEditor.Domain.TimeEditor;
using System.Text;

namespace SrtEditor.TimeEditor
{
    public class SrtTimeEditorRunner
    {
        private readonly TimeEditorOptions _options;
        private readonly TimeEditorOptionsValidator _validator;
        private readonly TimeSpanCalculator _calculator;
        private readonly Encoding _encoding;

        public SrtTimeEditorRunner(TimeEditorOptions options)
        {
            _options = options;
            _validator = new TimeEditorOptionsValidator(_options);
            _calculator = new TimeSpanCalculator(_options);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding("ISO-8859-2");
        }

        public void Run()
        {
            _validator.Validate();
            var lines = ReadLines();
            lines = SelectCalculation(lines);
            WriteLines(lines);
        }

        private IEnumerable<string> ReadLines()
        {
            using StreamReader reader = new(_options.FilePaths, _encoding, true);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        private IEnumerable<string> SelectCalculation(IEnumerable<string> lines)
        {
            if (TimeEditorOptionsValidator.HasDeley(_options.Delay))
            {
                lines = UpdateLines(lines, _calculator.CalculateDelay);
            }

            if (TimeEditorOptionsValidator.HasTimeScaleDiff(_options.TimeScaleDifference))
            {
                lines = UpdateLines(lines, _calculator.CalculateScale);
            }

            return lines;
        }

        private static List<string> UpdateLines(IEnumerable<string> lines, Func<string, string> calculationFunc)
        {
            var newLines = new List<string>();

            foreach (string line in lines)
            {
                var newLine = line.Contains("-->") ? calculationFunc(line) : line;
                newLines.Add(newLine);
            }

            return newLines;
        }

        private void WriteLines(IEnumerable<string> lines)
        {
            var newPath = _options.CreatedFileLocation == CreatedFileLocation.OverwriteOriginal
                ? _options.FilePaths
                : _options.FilePaths.Replace(".srt", "-modified.srt");
            using StreamWriter writer = new(newPath, false, _encoding);
            foreach (var line in lines)
            {
                writer.Write(line + Environment.NewLine);
            }
        }
    }
}
