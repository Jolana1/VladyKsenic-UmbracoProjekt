using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using VladyKsenicUmbracoProjekt.lib.Models;

namespace VladyKsenicUmbracoProjekt.lib.Controllers
{
    [PluginController("OsobnaStranka")]
    public class ContactController : _BaseController
    {
        public ActionResult Index()
        {
            return View("ContactForm", new OsobnaStrankaContactModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleFormSubmit(OsobnaStrankaContactModel model)
        {
            if (ModelState.IsValid)
            {
                if (!new ApiKeyValidator().IsValid(model.Password, model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Musíte označiť, že nie ste robot.");
                }
            }
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            TempData["success"] = model.SendContactRequest();

            return RedirectToCurrentUmbracoPage();
        }
    }
}
