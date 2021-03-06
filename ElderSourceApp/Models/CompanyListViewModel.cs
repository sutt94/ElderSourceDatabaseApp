﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace ElderSourceApp.Models
{
    public class CompanyListViewModel
    {
        public int CompanyModelID { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String CompanyType { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public Boolean HasSymbol { get; set; }
        public Boolean EmployeesTrained { get; set; }
        public Boolean HasPolicies { get; set; }
        public Boolean HasDeclaration { get; set; }
        public Boolean InArrears { get; set; }
    }
}