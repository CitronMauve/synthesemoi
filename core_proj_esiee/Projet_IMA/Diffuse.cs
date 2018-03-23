namespace Projet_IMA
{
    class Diffuse : Lampe
    {
        private V3 Direction;
        private Couleur Csource;

        public Diffuse(V3 direction, Couleur Csource)
        {
            this.Direction = direction;
            this.Csource = Csource;
        }

        public Couleur Illuminer(Couleur couleur, V3 normale)
        {
            return (Csource * couleur) * ((Direction * -1) * normale);
        }
    }
}
