using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElderSourceApp.Models
{
    public class ReportViewModel

    {
        public string ReportName { get; set; }

        public DateTime timeCreated { get; set; }
        public DateTime filterStart { get; set; }

        public DateTime filterEnd { get; set; }
    }
}