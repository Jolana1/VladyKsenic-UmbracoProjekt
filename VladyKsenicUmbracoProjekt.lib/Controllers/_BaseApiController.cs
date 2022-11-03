using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Web.WebApi;

namespace VladyKsenicUmbracoProjekt.lib.Controllers
{
    public class _BaseApiController : UmbracoApiController
    {
        public string CurrentSessionId
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }

        public string GetRequestParam(string paramName)
        {
            return HttpContext.Current.Request.Params.Get(paramName);
        }
    }
}
