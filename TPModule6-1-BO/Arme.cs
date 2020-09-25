using System.ComponentModel;

namespace TpModule6Bo
{
    public class Arme : AbstractClassDojo
    {
        public override int Id { get; set; }
        public override string Nom { get; set; }
        [DisplayName("Dégats")]
        public int Degats { get; set; }
    }
}