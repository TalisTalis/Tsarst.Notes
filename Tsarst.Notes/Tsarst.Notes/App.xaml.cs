using System;
using Xamarin.Forms;
using Tsarst.Notes.Data;
using System.IO;

namespace Tsarst.Notes
{
    public partial class App : Application
    {
        static NotesDB notesDB; // поле. объект, который представляет БД

        //свойство
        public static NotesDB NotesDB
        {
            get
            {
                // если есть ссылка, то ссылка не присваивается
                if (notesDB == null) // не инициализирована ли ссылка. Реализуем паттерн Singlton
                {
                    notesDB = new NotesDB(
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "TsarstNotesDatabase.db3"));
                }
                return notesDB;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell(); // установка страницы которая открывается при старте
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
