using SrtTimeEditor.Domain;
using SrtTimeEditor.Program.Exceptions;

namespace SrtTimeEditor.Program
{
    public class SrtOptionsValidator(SrtOptions options)
    {
        private readonly SrtOptions _options = options;

        public void Validate()
        {
            bool isValid = ProperFileExists(_options.FilePaths)
                && (HasDeley(_options.Delay) || HasTimeScaleDiff(_options.TimeScaleDifference));

            if (!isValid)
            {
                throw new ValidationException("Please check your inputs!");
            }
        }

        public static bool HasTimeScaleDiff(TimeScaleDifference diff)
        {
            return diff.Movie1 != diff.Subtitle1
                || diff.Movie2 != diff.Subtitle2;
        }

        public static bool HasDeley(double delay)
        {
            return delay != 0;
        }

        private static bool ProperFileExists(string path)
        {
            return File.Exists(path)
                && new FileInfo(path).Length > 0
                && path.EndsWith(".srt");
        }
    }
}
