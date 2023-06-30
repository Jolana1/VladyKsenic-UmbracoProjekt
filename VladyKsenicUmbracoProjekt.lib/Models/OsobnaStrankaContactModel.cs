using dufeksoft.lib.Mail;
using dufeksoft.lib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VladyKsenicUmbracoProjekt.lib.Util;

namespace VladyKsenicUmbracoProjekt.lib.Models
{
    public class OsobnaStrankaContactModel
    {
        [RequiredCurrentLang("Models/OsobnaStrankaContactModel", "Meno a priezvisko musia byť zadané")]
        [DisplayCurrentLang("Models/OsobnaStrankaContactModel", "Meno a priezvisko")]
        public string Name { get; set; }
        [EmailCurrentLang("Models/OsobnaStrankaContactModel", "Neplatný email")]
        [RequiredCurrentLang("Models/OsobnaStrankaContactModel","E-mail musí byť zadaný")]
        [DisplayCurrentLang("Models/OsobnaStrankaContactModel", "E-mail")]
        public string Email { get; set; }
        [RequiredCurrentLang("Models/OsobnaStrankaContactModel","Text správy musí byť zadaný")]
        [DisplayCurrentLang("Models/OsobnaStrankaContactModel", "Text správy")]
        public string Text { get; set; }

        [Display(Name = "Heslo")]
        public string Password { get; set; }
        [Display(Name = "Potvrdenie hesla")]
        public string ConfirmPassword { get; set; }

        public bool SendContactRequest()
        {
            List<TextTemplateParam> paramList = new List<TextTemplateParam> { };
            paramList.Add(new TextTemplateParam("NAME", this.Name));
            paramList.Add(new TextTemplateParam("EMAIL", this.Email));
            paramList.Add(new TextTemplateParam("TEXT", this.Text));

            // Odoslanie uzivatelovi
            OsobnaStrankaMailer.SendMailTemplate(
                "Vaša správa",
                TextTemplate.GetTemplateText("ContactSendSuccess_Sk", paramList),
                this.Email, null);

            return true;
        }
    }
}
