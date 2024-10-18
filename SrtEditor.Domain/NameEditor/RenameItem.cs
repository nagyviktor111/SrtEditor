namespace SrtEditor.Domain.NameEditor
{
    public class RenameItem(string oldName, string newName)
    {
        public string OldName { get; set; } = oldName;

        public string NewName { get; set; } = newName;
    }
}
