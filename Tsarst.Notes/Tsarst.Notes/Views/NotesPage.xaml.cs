using System;
using System.Linq;

using Xamarin.Forms;
using Tsarst.Notes.Models;

namespace Tsarst.Notes.Views
{
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            collectionView.ItemsSource = await App.NotesDB.GetNotesAsync(); // при загрузке страницы вывод из БД всех заметок
            base.OnAppearing();
        }

        /// <summary>
        /// При нажатии кнопки Add открытие новой страницы NoteAddingPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteAddingPage));
        }

        /// <summary>
        /// Обработка выделения заметки. Если выделена, то необходимо редактирование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null) // если есть выделение
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault(); // получаем объект
                await Shell.Current.GoToAsync(
                    $"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}"); // запуск страницы по id заметки
            }
        }
    }
}