using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VladyKsenicUmbracoProjekt.lib.Util
{
    public class ConfigurationUtil
    {
        public const string LoginFormId = "osobnaStranka.LoginFormId";
        public const string AfterLoginFormId = "osobnaStranka.AfterLoginFormId";
        public const string AfterPasswordResetFormId = "osobnaStranka.AfterPasswordResetFormId";


        public static int GetPageId(string pageKey)
        {
            return int.Parse(ConfigurationManager.AppSettings[pageKey]);
        }

        public static string GetCfgValue(string cfgKey)
        {
            return ConfigurationManager.AppSettings[cfgKey];
        }
    }
}
