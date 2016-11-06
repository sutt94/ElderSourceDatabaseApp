using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElderSourceApp.Models
{
    public class ReportModel
    {

        public int reportId { get; set; }

        public string reportName { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime lastDatePaid { get; set; }

    }
}