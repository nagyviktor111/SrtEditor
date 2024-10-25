﻿namespace SrtEditor.Domain.NameEditor
{
    public class NameEditorOptions
    {
        public required string FolderPath { get; set; }

        public bool CopyVideoNames { get; set; }

        public bool ExtendFileNames { get; set; }

        public string? Extension { get; set; }
    }
}
