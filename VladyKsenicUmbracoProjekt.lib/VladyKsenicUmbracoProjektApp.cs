using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladyKsenicUmbracoProjekt.lib.Util;

namespace VladyKsenicUmbracoProjekt.lib
{
    public class VladyKsenicUmbracoProjektApp : Umbraco.Web.UmbracoApplication
    {
        public override void Init()
        {
            base.Init();
            TranslateUtil.RegisterTranslations();
        }
    }
}
