using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using VladyKsenicUmbracoProjekt.lib.Models.UmbracoCmsContent;
using VladyKsenicUmbracoProjekt.lib.Util;

namespace VladyKsenicUmbracoProjekt.lib.Repositories.UmbracoCmsContent
{
   

        public class TextRepository
        {
            public const int TextyId_Sk = 1069;
            public const int TextyId_En = 1072;

            public static Text GetFromUmbraco(UmbracoHelper umbraco)
            {
            string cultureId = CurrentLang.GetCurrentCultureId();
            IPublishedContent content = umbraco.Content(cultureId == CurrentLang.CultureId_En ? TextyId_En : TextyId_Sk);

                return content == null ? null : new Text(content);

                
                //    string cultureId = CurrentLang.GetCurrentCultureId(); 
                //    IPublishedContent publishedContent = umbraco.Content(cultureId == CurrentLang.CultureId_En ? TextyId_En : TextyId_Sk);
                //    IPublishedContent content = publishedContent;

                //    return content == null ? null : new Text(content);
            }
    }
}
