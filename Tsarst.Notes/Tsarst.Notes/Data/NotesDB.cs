using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Tsarst.Notes.Models;

namespace Tsarst.Notes.Data
{
    /// <summary>
    /// Класс для работы с БД.
    /// Добавление, редактирование, удаление строк
    /// </summary>
    public class NotesDB
    {
        readonly SQLiteAsyncConnection db; // поле только для чтения. с помощью этого объекта будем создавать подключение

        // конструктор для класса
        public NotesDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString); // инициализация подключения

            //при инициализации класса. когда еще нет таблицы, ее создаем с указанием типа объектов "записка"
            db.CreateTableAsync<Note>().Wait(); // создание таблицы. Метод Wait нужен для завершения процесса создания
        }

        // методы для работы с данными в БД

        // получение всех записок
        public Task<List<Note>> GetNotesAsync() // возвращает объект типа Task закрытый списком с записками
        {
            return db.Table<Note>().OrderByDescending(i => i.Date).ToListAsync(); // приводим к списку строки из таблицы
        }

        // получение конкретной записки по id
        public Task<Note> GetNoteAsync(int id) // возвращает одиночный объект типа Task
        {
            return db.Table<Note>()                 // доступ к таблице
                            .Where(i => i.ID == id) // запрос по выборке из БД строки с конкретным id
                            .FirstOrDefaultAsync(); // взятие первой наденой строки или дефолтное
        }

        // добавление или редактирование записки
        public Task<int> SaveNoteAsync(Note note) // если операция прошла успешно, то возвращает количество обработаных строк
        {
            if (note.ID != 0) // если id не равен 0 - обновление записи по id
                return db.UpdateAsync(note);
            else
                return db.InsertAsync(note); // если id = 0, то создание записи в БД
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            return db.DeleteAsync(note); // возвращает количество обработанных строк
        }
    }
}
