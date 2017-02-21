using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using PrismToDo.UWP;

[assembly: Dependency(typeof(SQLite_UWP))]
namespace PrismToDo.UWP
{
    public class SQLite_UWP : ISqLite
    {
        public SQLite_UWP() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return path;
        }
    }
}
