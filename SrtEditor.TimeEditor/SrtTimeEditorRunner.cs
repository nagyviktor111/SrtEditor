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
            var lines = Read();
            lines = Update(lines);
            Write(lines);
        }

        private IEnumerable<string> Read()
        {
            using StreamReader reader = new(_options.FilePath, _encoding, true);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        private IEnumerable<string> Update(IEnumerable<string> lines)
        {
            if (TimeEditorOptionsValidator.HasDeley(_options.Delay))
            {
                lines = Calculate(lines, _calculator.CalculateDelay);
            }

            if (TimeEditorOptionsValidator.HasTimeScaleDiff(_options.TimeScaleDifference))
            {
                lines = Calculate(lines, _calculator.CalculateScale);
            }

            return lines;
        }

        private static List<string> Calculate(IEnumerable<string> lines, Func<string, string> calculationFunc)
        {
            var newLines = new List<string>();

            foreach (string line in lines)
            {
                var newLine = line.Contains("-->") ? calculationFunc(line) : line;
                newLines.Add(newLine);
            }

            return newLines;
        }

        private void Write(IEnumerable<string> lines)
        {
            var newPath = _options.CreatedFileLocation == CreatedFileLocation.OverwriteOriginal
                ? _options.FilePath
                : _options.FilePath.Replace(".srt", "-modified.srt");
            using StreamWriter writer = new(newPath, false, _encoding);
            foreach (var line in lines)
            {
                writer.Write(line + Environment.NewLine);
            }
        }
    }
}
