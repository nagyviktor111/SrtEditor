namespace SrtEditor.Domain.Renamer
{
    public class RenamerOptions
    {
        public string? FolderPath { get; set; }

        public bool CopyVideoNames { get; set; }

        public bool ExtendFileNames { get; set; }

        public string? Extension { get; set; }
    }
}
