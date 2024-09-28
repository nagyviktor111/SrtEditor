using SrtTimeEditor.Domain;
using System.Text;

namespace SrtTimeEditor.Program
{
    public class SrtTimeEditorRunner
    {
        private readonly SrtOptionsValidator _validator;
        private readonly Encoding _encoding;
        private readonly SrtOptions _options;

        public SrtTimeEditorRunner(SrtOptions options)
        {
            _validator = new SrtOptionsValidator();
            _encoding = Encoding.UTF8;
            _options = options;
        }

        public bool IsValid()
        {
            return _validator.Validate(_options);
        }

        public void Run()
        {
            var lines = ReadLines();
            lines = ChangeLines(lines);
            WriteLines(lines);
        }

        private IEnumerable<string> ReadLines()
        {
            using StreamReader reader = new(_options.FilePaths, _encoding, true);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        private IEnumerable<string> ChangeLines(IEnumerable<string> lines)
        {
            if (_validator.HasDeley(_options.Delay))
            {
                var tmpLines = new List<string>();
                var difference = new TimeSpan((long)(_options.Delay * 10000000));

                foreach (string line in lines)
                {
                    if (line.Contains("-->"))
                    {
                        _ = TimeSpan.TryParse(line.Remove(12, 17), out TimeSpan starting);
                        _ = TimeSpan.TryParse(line.Remove(0, 17), out TimeSpan ending);
                        starting += difference;
                        ending += difference;

                        //var nulla = TimeSpan.Parse("00:00:00");
                        //if (nulla > starting) return "negative time";

                        var newLine = starting.ToString("hh\\:mm\\:ss\\,fff") + " --> " + ending.ToString("hh\\:mm\\:ss\\,fff");
                        tmpLines.Add(newLine);
                    }
                    else
                    {
                        tmpLines.Add(line);
                    }
                }

                lines = tmpLines;
            }

            if (_validator.HasTimeScaleDiff(_options.TimeScaleDifference))
            {
                var tmpLines = new List<string>();

                foreach (string line in lines)
                {
                    if (line.Contains("-->"))
                    {
                        float movie1 = _options.TimeScaleDifference.Movie1.Ticks;
                        float movie2 = _options.TimeScaleDifference.Movie2.Ticks;
                        float subtitle1 = _options.TimeScaleDifference.Subtitle1.Ticks;
                        float subtitle2 = _options.TimeScaleDifference.Subtitle2.Ticks;

                        _ = TimeSpan.TryParse(line.Remove(12, 17), out TimeSpan starting);
                        _ = TimeSpan.TryParse(line.Remove(0, 17), out TimeSpan ending);
                        float startTicks = starting.Ticks;
                        float endTicks = ending.Ticks;
                        startTicks = movie2 + ((movie2 - movie1) / (subtitle2 - subtitle1)) * (startTicks - subtitle2);
                        endTicks = movie2 + ((movie2 - movie1) / (subtitle2 - subtitle1)) * (endTicks - subtitle2);

                        starting = TimeSpan.FromTicks((long)startTicks);
                        ending = TimeSpan.FromTicks((long)endTicks);

                        var newLine = starting.ToString("hh\\:mm\\:ss\\,fff") + " --> " + ending.ToString("hh\\:mm\\:ss\\,fff");
                        tmpLines.Add(newLine);
                    }
                    else
                    {
                        tmpLines.Add(line);
                    }
                }

                lines = tmpLines;
            }

            return lines;
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
