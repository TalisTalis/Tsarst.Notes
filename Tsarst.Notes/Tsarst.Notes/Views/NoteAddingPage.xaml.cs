using System;
using Tsarst.Notes.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tsarst.Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))] // аттрибут по которому приложение понимет что значение присваевается свойству
    public partial class NoteAddingPage : ContentPage
    {
        // свойство
        public string ItemId
        {
            set
            {
                LoadNote(value); // загрузка заметки если получен id заметки
            }
        }
               

        public NoteAddingPage()
        {
            InitializeComponent();

            BindingContext = new Note(); // при открытии срабатывает конструктор и создается новы объект
        }
        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value); // преобразование id из строки в число

                Note note = await App.NotesDB.GetNoteAsync(id); // получение заметки из БД

                BindingContext = note; // установить полученную заметку в контекст
            }
            catch { }
        }


        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext; // сохранение изменений в объект

            note.Date = DateTime.Now; // сохранение даты на текуущем устройстве

            if (!string.IsNullOrWhiteSpace(note.Text)) // если не пробелы и не пустой текст, то сохранить в БД
            {
                await App.NotesDB.SaveNoteAsync(note);
            }

            await Shell.Current.GoToAsync(".."); // вернутся на два шага назад (на главную страницу) к списку
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext; // получение объекта

            await App.NotesDB.DeleteNoteAsync(note); //передача объекта в метод на удаление

            await Shell.Current.GoToAsync(".."); // возврат к списку заметок
        }
    }
}