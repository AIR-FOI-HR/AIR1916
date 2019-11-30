using System;
using System.Collections.Generic;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public static class Filtar
    {
        public static List<Autori> filtarAutori { get; set; } = new List<Autori>();
        public static List<Izdavaci> filtarIzdavaci { get; set; } = new List<Izdavaci>();
        public static List<Kategorije> filtarKategorije { get; set; } = new List<Kategorije>();
        public static List<Slova> filtarSlova { get; set; } = new List<Slova>();
    }
}
