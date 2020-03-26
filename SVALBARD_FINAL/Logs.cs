using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Logs
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string IssuerID { get; set; }
        public string IssuerDir { get; set; }
        public string IssuerEts { get; set; }
        public string IssuerService { get; set; }
        public string ArchiveID { get; set; }
        public int Action { get; set; }
    }
}