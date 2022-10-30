using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VladyKsenicUmbracoProjekt.lib.Models
{
    public class VzdelanieModel
    {
        public string ZakladnaSkola;
        public string StrednaSkola;
        public string VysokaSkola;
        public Kurzy zoznamKurzov;

        public VzdelanieModel()


        {
            zoznamKurzov = new Kurzy();




        }

    }

}
   
