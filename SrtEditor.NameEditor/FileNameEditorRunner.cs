using SrtEditor.Domain.Exceptions;
using SrtEditor.Domain.NameEditor;

namespace SrtEditor.NameEditor
{
    public class FileNameEditorRunner
    {
        public void Run(NameEditorOptions options)
        {
            IEnumerable<RenameItem> changes = GetNewNames(options);

            if (!changes.Any())
            {
                throw new ValidationException("No changes!");
            }

            if (!options.CopyVideoNames && !options.ExtendFileNames)
            {
                throw new ValidationException("You must select at least one operation!");
            }

            foreach (var item in changes)
            {
                File.Move(item.OldName, item.NewName, true);
            }
        }

        public IEnumerable<RenameItem> GetNewNames(NameEditorOptions options)
        {
            if (!Directory.Exists(options.FolderPath))
            {
                return [];
            }

            var list = Directory
                .GetFiles(options.FolderPath, "*.srt", SearchOption.TopDirectoryOnly)
                .Select(s => new RenameItem(s, s))
                .ToList();

            if (options.CopyVideoNames) 
            {
                list = CopyVideoNames(list, options.FolderPath);
            }

            if (options.ExtendFileNames)
            {
                list = ExtendFileNames(list, options.Extension ?? "");
            }

            return list;
        }

        private static List<RenameItem> CopyVideoNames(List<RenameItem> list, string folderPath)
        {
            var videos = Directory
                .GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => f.ToLower().EndsWith("avi") || f.ToLower().EndsWith("mp4") || f.ToLower().EndsWith("mkv"))
                .ToList();

            if (videos.Count < list.Count)
            {
                return list;
            }

            for (var i = 0; i < list.Count; i++)
            {
                list[i].NewName = videos[i][..^4] + ".srt";
            }

            return list;
        }

        private static List<RenameItem> ExtendFileNames(List<RenameItem> list, string extension)
        {
            foreach (var item in list)
            {
                item.NewName = item.NewName[..^4] + extension + ".srt";
            }

            return list;
        }
    }
}
