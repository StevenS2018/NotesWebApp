namespace NotesWebApp.Models
{
    public class NotesModel
    {
        public int id { get; set; }
        public string NoteAuthor { get; set; } = string.Empty;
        public string NotesTitle { get; set; } = string.Empty;
        public string NotesDescription { get; set; } = string.Empty;


        public NotesModel()
        {

        }

    }
}
