using SrtTimeEditor.Domain;
using System.Text;

namespace SrtTimeEditor.Program
{
    public class SrtTimeEditorRunner
    {
        private readonly SrtOptions _options;
        private readonly SrtOptionsValidator _validator;
        private readonly TimeSpanCalculator _calculator;
        private readonly Encoding _encoding;

        public SrtTimeEditorRunner(SrtOptions options)
        {
            _options = options;
            _validator = new SrtOptionsValidator(_options);
            _calculator = new TimeSpanCalculator(_options);
            _encoding = Encoding.UTF8;
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
            if (SrtOptionsValidator.HasDeley(_options.Delay))
            {
                lines = UpdateLines(lines, _calculator.CalculateDelay);
            }

            if (SrtOptionsValidator.HasTimeScaleDiff(_options.TimeScaleDifference))
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
