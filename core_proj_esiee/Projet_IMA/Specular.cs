using System;

namespace Projet_IMA
{
    class Specular
    {
        int specular_power;

        public Specular(Couleur Csource, int specular_power)
        {
            this.specular_power = specular_power;
        }

        public Couleur Illuminer(Couleur couleur, V3 vecteur)
        {
            throw new NotImplementedException();
        }
    }
}
