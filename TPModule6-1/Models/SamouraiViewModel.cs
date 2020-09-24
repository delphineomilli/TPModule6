using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TpModule6Bo;

namespace TPModule6_1.Models
{
    public class SamouraiViewModel
    {
        public Samourai Samourai { get; set; }
        public List<Arme> Armes { get; set; } = new List<Arme>();
        public int? IdSelectedArme { get; set; }
        public List<ArtMartial> ArtMartiaux { get; set; } = new List<ArtMartial>();
        public List<int> IdSelectedArtMartials { get; set; } = new List<int>();
    }
}