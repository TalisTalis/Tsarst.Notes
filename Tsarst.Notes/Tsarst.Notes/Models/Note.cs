using SQLite;
using System;

namespace Tsarst.Notes.Models
{
    /// <summary>
    /// Класс представляет записку
    /// </summary>
    public class Note
    {
        // поля класса
        // создаем поля таблицы

        [PrimaryKey, AutoIncrement] public int ID { get; set; } // аттрибуты поля id 

        public string Text { get; set; } // поле, в котором хранится текст записки

        public DateTime Date { get; set; } // поле, в котором хранится дата и время создания или редактирования записки
    }
}
