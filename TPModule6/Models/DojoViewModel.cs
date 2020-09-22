using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPModule6.Models
{
    public class DojoViewModel
    {
        public Samourai samourai { get; set; }

        public List<SelectListItem> Armes { get; set; } = new List<SelectListItem>();

        public int? IdArmes { get; set; } 

      
    }
}