using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using VladyKsenicUmbracoProjekt.lib.Models.UmbracoCmsContent;

namespace VladyKsenicUmbracoProjekt.lib.Repositories.UmbracoCmsContent
{
    public class TextRepository 
    {
        public const int TextyId = 1069;

        public static Text GetFromUmbraco(UmbracoHelper umbraco)
        {
            IPublishedContent content = umbraco.Content(TextyId);

            return content == null ? null : new Text(content);
        }
    }
}
