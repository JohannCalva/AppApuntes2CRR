namespace AppApuntes2CRR.Views;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
        InitializeComponent();

        BindingContext = new Models.AllNotes();
    }

    protected override void OnAppearing()
    {
        ((Models.AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Notes));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var note = (Models.Note)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"{nameof(Notes)}?{nameof(Notes.ItemId)}={note.Filename}");

            notesCollection.SelectedItem = null;
        }
    }
}