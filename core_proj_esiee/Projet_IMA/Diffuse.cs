using System;

namespace Projet_IMA
{
    class Diffuse : Lampe
    {
        private V3 Direction;
        private Couleur Csource;

        public Diffuse(V3 direction, Couleur Csource)
        {
            this.Direction = direction;
            this.Direction.Normalize();
            this.Csource = Csource;
        }

        public Couleur Illuminer(Couleur couleur, V3 normale)
        {
            return (Csource * couleur) * Math.Max(0, (Direction * -1) * normale);
        }
    }
}
