using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace R1StatsModel.Extensions
{
    public static class DataTableExtension
    {
        public static T GetValue<T>(this DataTable table, int row, string column) 
            => (T) table.Rows[row][column];
    }
}
