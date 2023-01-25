using System.Collections.ObjectModel;

namespace Notes.Models
{
    internal class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public AllNotes() => LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            string appDataPath = FileSystem.AppDataDirectory;

            // Usar Linq pra carregar as notas *.notes.txt
            IEnumerable<Note> notes = Directory

                // selecionar os nomes dos ficheiros do diretorio
                .EnumerateFiles(appDataPath, "*.notes.txt")

                // Cada ficheiro vai ser usado para criar uma nova nota
                .Select(filename => new Note()
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                })

                // Ordenar a coleção por data
                .OrderBy(_ => _.Date);

            // adicionar cada nota à ObservableCollection
            foreach (Note note in notes)
                Notes.Add(note);
        }
    }
}
