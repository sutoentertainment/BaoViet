using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Interfaces
{
    public interface ISQLItem
    {
        [PrimaryKey, AutoIncrement]
        int Id { get; set; }

        DateTime Date_Created { get; set; }
    }
}
