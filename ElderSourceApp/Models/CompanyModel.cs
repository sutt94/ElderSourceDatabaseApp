
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElderSourceApp.Models
{
    public class CompanyModel
    {
        [Key]
        public int CompanyModelID { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Address { get; set; }
        public String CompanyType { get; set; }
        public String Phone { get; set; }
        public Boolean HasSymbol { get; set; }
        public Boolean EmployeesTrained { get; set; }
        public Boolean HasPolicies { get; set; }
        public Boolean HasDeclaration { get; set; }
        public Boolean InArrears { get; set; }
        public String description { get; set; }
        public String Notes { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastPaidDate { get; set; }


    }
}
