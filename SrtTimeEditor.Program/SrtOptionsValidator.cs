using SrtTimeEditor.Domain;

namespace SrtTimeEditor.Program
{
    public class SrtOptionsValidator
    {
        private readonly SrtOptions _options;

        public SrtOptionsValidator(SrtOptions options)
        {
            _options = options;
        }

        public bool Validate()
        {
            return ProperFileExists(_options.FilePaths)
                && (HasDeley(_options.Delay) || HasTimeScaleDiff(_options.TimeScaleDifference));
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
