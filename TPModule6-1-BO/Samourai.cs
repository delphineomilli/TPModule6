using TpModule6Bo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TpModule6Bo
{
    public class Samourai : AbstractClassDojo
    {
        public override int Id { get; set; }
        public int Force { get; set; }
        public override string Nom { get; set; }
        public virtual Arme Arme { get; set; }

        [DisplayName("Arts martiaux")]
        public virtual List<ArtMartial> ArtMartiaux { get; set; } = new List<ArtMartial>();

        public int Potentiel
        {
            get
            {
                if (Arme == null)
                {
                    return this.Force * (this.ArtMartiaux.Count + 1);
                }
                else
                {
                    return (this.Force + this.Arme.Degats * (this.ArtMartiaux.Count + 1)); //(Force + dégâts de l’arme) * (nombre d’arts martiaux+1)
                }
            }
 
        }    
    }
}
