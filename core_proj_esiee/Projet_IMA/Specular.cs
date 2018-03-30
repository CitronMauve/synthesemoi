using System;

namespace Projet_IMA
{
    class Specular
    {
        private V3 Direction;
        private int Specular_power;
        private Couleur Csource;

        public Specular(V3 direction, Couleur Csource, int specular_power)
        {
            this.Direction = direction;
            this.Direction.Normalize();
            this.Csource = Csource;
            this.Specular_power = specular_power;
        }

        public Couleur Illuminer(V3 camera, V3 normale)
        {
            V3 reflection = Direction + 2 * (-Direction * normale * normale);
            return Csource * (float) (Math.Pow(Math.Max(0, reflection * camera), this.Specular_power));
        }
    }
}
