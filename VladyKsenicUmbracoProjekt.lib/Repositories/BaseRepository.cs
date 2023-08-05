using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;

namespace VladyKsenicUmbracoProjekt.lib.Repositories
{
    public class BaseRepository
    {
        protected readonly IMemberService MemberService;

        public BaseRepository()
        {
            MemberService = Current.Services.MemberService;
        }
    }
}


