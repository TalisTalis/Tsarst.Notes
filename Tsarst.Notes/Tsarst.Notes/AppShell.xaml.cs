using Tsarst.Notes.Views;
using Xamarin.Forms;


namespace Tsarst.Notes
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NoteAddingPage), typeof(NoteAddingPage));
        }
    }
}