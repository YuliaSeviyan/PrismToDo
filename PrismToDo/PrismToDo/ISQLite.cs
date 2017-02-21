using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismToDo
{
    public interface ISqLite
    {
        string GetDatabasePath(string filename);
    }
}
