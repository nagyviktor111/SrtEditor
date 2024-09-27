using SrtTimeEditor.Domain;

namespace SrtTimeEditor.Program
{
    public class SrtTimeEditorRunner
    {
        private readonly SrtOptionsValidator _validator;

        public SrtTimeEditorRunner()
        {
            _validator = new SrtOptionsValidator();
        }

        public bool IsValid(SrtOptions options)
        {
            return _validator.Validate(options);
        }

        public void Run(SrtOptions options)
        {
            Console.WriteLine("HALOO");
        }
    }
}
