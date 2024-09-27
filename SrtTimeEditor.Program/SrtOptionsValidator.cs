using SrtTimeEditor.Domain;

namespace SrtTimeEditor.Program
{
    public class SrtOptionsValidator
    {
        public bool Validate(SrtOptions options)
        {
            return ProperFileExists(options.FilePaths)
                && (HasDeley(options.Delay) || HasTimeScaleDiff(options.TimeScaleDifference));
        }

        private bool HasTimeScaleDiff(TimeScaleDifference diff)
        {
            return diff.Movie1 != diff.Subtitle1
                || diff.Movie2 != diff.Subtitle2;
        }

        private bool HasDeley(double delay)
        {
            return delay != 0;
        }

        private bool ProperFileExists(string path)
        {
            return File.Exists(path)
                && new FileInfo(path).Length > 0
                && path.EndsWith(".srt");
        }
    }
}
