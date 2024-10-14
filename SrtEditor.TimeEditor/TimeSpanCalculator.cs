using SrtEditor.Domain.TimeEditor;
using SrtEditor.TimeEditor.Exceptions;

namespace SrtEditor.TimeEditor
{
    public class TimeSpanCalculator(TimeEditorOptions options)
    {
        private readonly TimeEditorOptions _options = options;

        public string CalculateScale(string line)
        {
            float movie1 = _options.TimeScaleDifference.Movie1.Ticks;
            float movie2 = _options.TimeScaleDifference.Movie2.Ticks;
            float subtitle1 = _options.TimeScaleDifference.Subtitle1.Ticks;
            float subtitle2 = _options.TimeScaleDifference.Subtitle2.Ticks;

            GetTimes(line, out TimeSpan starting, out TimeSpan ending);
            float startTicks = starting.Ticks;
            float endTicks = ending.Ticks;
            startTicks = movie2 + ((movie2 - movie1) / (subtitle2 - subtitle1)) * (startTicks - subtitle2);
            endTicks = movie2 + ((movie2 - movie1) / (subtitle2 - subtitle1)) * (endTicks - subtitle2);

            starting = TimeSpan.FromTicks((long)startTicks);
            ending = TimeSpan.FromTicks((long)endTicks);

            return FormatLine(starting, ending);
        }

        public string CalculateDelay(string line)
        {
            var difference = new TimeSpan((long)(_options.Delay * 10000000));

            GetTimes(line, out TimeSpan starting, out TimeSpan ending);
            starting += difference;
            ending += difference;

            if (TimeSpan.Parse("00:00:00") > starting)
            {
                throw new NegativeTimeException("Starting time gets less than 0 in this line: " + Environment.NewLine + line);
            }

            return FormatLine(starting, ending);
        }

        private static void GetTimes(string line, out TimeSpan starting, out TimeSpan ending)
        {
            _ = TimeSpan.TryParse(line.Remove(12, 17), out starting);
            _ = TimeSpan.TryParse(line.Remove(0, 17), out ending);
        }

        private static string FormatLine(TimeSpan starting, TimeSpan ending)
        {
            return starting.ToString("hh\\:mm\\:ss\\,fff") + " --> " + ending.ToString("hh\\:mm\\:ss\\,fff");
        }
    }
}
