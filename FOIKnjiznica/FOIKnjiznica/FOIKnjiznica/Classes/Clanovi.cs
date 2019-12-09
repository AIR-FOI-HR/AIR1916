using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public static class Clanovi
    {
        public static int id { get; set; }
        public static string hrEduPersonUniqueID { get; set; }
        public static string ime { get; set; }
        public static string prezime { get; set; }
        public static string mobitelID { get; set; }
        public static List<Publikacije> listaFavorita { get; set; }

    }
}
