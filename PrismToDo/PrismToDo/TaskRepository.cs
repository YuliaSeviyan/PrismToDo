using PrismToDo.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PrismToDo
{
    public class TaskRepository
    {
        SQLiteConnection database;

        public TaskRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISqLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<TaskModel>();
        }

        public IEnumerable<TaskModel> GetItems()
        {
            return (from i in database.Table<TaskModel>() select i).
                Where(x => x.Date >= DateTime.Today).
                OrderBy(x => x.Date).
                ToList();
        }

        public int DeleteItem(int id)
        {
            return database.Delete<TaskModel>(id);
        }

        public void SaveItem(TaskModel item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
            }
            else
            {
                database.Insert(item);
            }
        }
    }
}
