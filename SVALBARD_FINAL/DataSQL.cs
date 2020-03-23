using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class DataSQL
    {
        public int ID { get; set; }
        public DateTime Versement { get; set; }
        public string Etablissement { get; set; }
        public string Direction { get; set; }
        public string Service { get; set; }
        public string Dossiers { get; set; }
        public string Extremes { get; set; }
        public string Elimination { get; set; }
        public string Communication { get; set; }
        public string Cote { get; set; }
        public string Localisation { get; set; }
        /* public float CL { get; set; }
        public float Chrono { get; set; }
        public string Calc { get; set; }*/
    }
}