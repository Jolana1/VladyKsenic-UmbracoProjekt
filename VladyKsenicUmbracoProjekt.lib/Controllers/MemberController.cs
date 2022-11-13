using dufeksoft.lib.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;
using VladyKsenicUmbracoProjekt.lib.Models;
using VladyKsenicUmbracoProjekt.lib.Util;


namespace VladyKsenicUmbracoProjekt.lib.Controllers
{
    [PluginController("OsobnaStranka")]
    public class MemberController : _BaseController
    {
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitLogin(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Members.Login(model.Username, model.Password))
                {
                    //FormsAuthentication.SetAuthCookie(model.Username, false);
                    UrlHelper myHelper = new UrlHelper(HttpContext.Request.RequestContext);
                    if (myHelper.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return this./*RedirectToUmbracoPageResult */RedirectToOsobnaStrankaUmbracoPage(ConfigurationUtil.AfterLoginFormId);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Neplatné meno alebo heslo.");
                }
            }
            return CurrentUmbracoPage();
        }

        public ActionResult Logout()
        {
            TempData.Clear();
            Session.Clear();
            Members.Logout();
            return this./*RedirectToUmbracoPageResult*/RedirectToOsobnaStrankaUmbracoPage(ConfigurationUtil.LoginFormId);
        }
        public ActionResult MemberInfo() => View();

        //public ActionResult LostPassword()
        //{
        //    return View(new LostPasswordModel());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SubmitLostPassword(LostPasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        DeveloperBossMemberRepository repository = new DeveloperBossMemberRepository();
        //        DeveloperBossMember member = repository.GetMemberByEmail(model.Email);
        //        if (member == null)
        //        {
        //            ModelState.AddModelError("", "Užívateľ nenájdený.");
        //        }
        //        else
        //        {
        //            DateTime dt = DateTime.Now;
        //            string pswd = dt.Ticks.ToString();

        //            member.IsLockedOut = false;
        //            member.IsApproved = true;
        //            member.Password = pswd;
        //            member.PasswordRepeat = pswd;
        //            MembershipCreateStatus status = repository.Save(this.Members, member, true);
        //            if (status == MembershipCreateStatus.Success)
        //            {
        //                status = repository.SavePassword(member);
        //            }
        //            if (status != MembershipCreateStatus.Success)
        //            {
        //                ModelState.AddModelError("", string.Format("Vyskytla sa chyba. {0} Skúste akciu zopakovať alebo nás kontaktujte.", repository.GetErrorMessage(status)));
        //            }
        //            else
        //            {
        //                List<TextTemplateParam> paramList = new List<TextTemplateParam>();
        //                paramList.Add(new TextTemplateParam("LOGIN", member.Email));
        //                paramList.Add(new TextTemplateParam("PASSWORD", member.Password));

        //                // Odoslanie uzivatelovi
        //                DeveloperBossMailer.SendMailTemplateWithoutBcc(
        //                    "Obnovenie prístupu",
        //                    TextTemplate.GetTemplateText("LostPassword_Sk", paramList),
        //                    member.Email);

        //                return this.        protected RedirectToUmbracoPageResult RedirectToOsobnaStrankaUmbracoPage(ConfigurationUtil.AfterPasswordResetFormId);
        //            }
        //        }
        //    }
        //    return CurrentUmbracoPage();
        //}

        //[Authorize(Roles = DeveloperBossMemberRepository.DeveloperBossMemberCourseRole)]
        //public ActionResult ChangePassword()
        //{
        //    return View(new ChangePasswordModel());
        //}
        //[HttpPost]
        //[Authorize(Roles = DeveloperBossMemberRepository.DeveloperBossMemberCourseRole)]
        //[ValidateAntiForgeryToken]
        //public ActionResult SavePassword(ChangePasswordModel model)
        //{
        //    if (model.NewPassword != model.NewPasswordRepeat)
        //    {
        //        ModelState.AddModelError("NewPasswordRepeat", "Zopakované nové heslo a Nové heslo musia byť rovnaké.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        DeveloperBossMemberRepository repository = new DeveloperBossMemberRepository();
        //        DeveloperBossMember currentMember = repository.GetCurrentMember();
        //        currentMember.Password = model.NewPassword;
        //        MembershipCreateStatus status = repository.SavePassword(currentMember);
        //        if (status != MembershipCreateStatus.Success)
        //        {
        //            ModelState.AddModelError("", string.Format("Vyskytla sa chyba. {0} Skúste akciu zopakovať alebo nás kontaktujte.", repository.GetErrorMessage(status)));
        //        }
        //        else
        //        {
        //            return this.        protected RedirectToUmbracoPageResult RedirectToOsobnaStrankaUmbracoPage(ConfigurationUtil.AfterLoginFormId);
        //        }
        //    }

        //    return CurrentUmbracoPage();
        //}
    }
}


