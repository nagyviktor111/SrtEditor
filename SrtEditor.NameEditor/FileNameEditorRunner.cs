using SrtEditor.Domain.NameEditor;

namespace SrtEditor.NameEditor
{
    public class FileNameEditorRunner
    {
        private NameEditorOptions _options;

        public FileNameEditorRunner(NameEditorOptions options)
        {
            _options = options;
        }

        public void Run()
        {

        }

        public IEnumerable<PreviewItem> UpdatePreview(NameEditorOptions options)
        {
            _options = options;
            return
            [
                new() { NewName = "new1", OldName = "old1" },
                new() { NewName = "new2", OldName = "old2" }
            ];
        }
    }
}
