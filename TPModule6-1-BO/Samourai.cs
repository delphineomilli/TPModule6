using TpModule6Bo;
using System.Collections.Generic;

namespace TpModule6Bo
{
    public class Samourai : AbstractClassDojo
    {
        public override int Id { get; set; }
        public int Force { get; set; }
        public override string Nom { get; set; }
        public virtual Arme Arme { get; set; }
      
        public virtual List<ArtMartial> ArtMartiaux { get; set; }
    }
}
