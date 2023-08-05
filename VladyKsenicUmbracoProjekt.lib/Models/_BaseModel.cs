using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VladyKsenicUmbracoProjekt.lib.Models
{
    public class _BaseModel
    {
    }

    public class _PagingModel
    {
        public const int DefaultItemsPerPage = 20;
        public const int AllItemsPerPage = 100000000;

        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        public _PagingModel()
        {
            ItemsPerPage = DefaultItemsPerPage;
        }
    }
}
