using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Umbraco.Web.Models;


namespace VladyKsenicUmbracoProjekt.lib.Models

{
    public class LoginModel 
    {
        [Display(Name = "Prihlasovacie meno")]
        [Required(ErrorMessage = "Prihlasovacie meno musí byť zadané")]
        public string Username { get; set; }

        [Display(Name = "Heslo")]
        [Required(ErrorMessage = "Heslo musí byť zadané")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //public class LostPasswordModel  
        //{
        //    [Display(Name = "Váš e-mail")]
        //    [Email(ErrorMessage = "Neplatný formát e-mailu")]
        //    [Required(ErrorMessage = "E-mail musí byť zadaný")]
        //    public string Email { get; set; }
        //}

        //public class ChangePasswordModel 
        //{
        //    [Display(Name = "Nové heslo")]
        //    [Required(ErrorMessage = "Nové heslo musí byť zadané")]
        //    public string NewPassword { get; set; }
        //    [Display(Name = "Zopakované nové heslo")]
        //    [Required(ErrorMessage = "Zopakované nové heslo musí byť zadané")]
        //    public string NewPasswordRepeat { get; set; }
        }
}


