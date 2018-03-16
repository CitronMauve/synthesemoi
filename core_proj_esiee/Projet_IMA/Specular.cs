using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Specular : Lampe
    {
        int specular_power;

        public Specular(Couleur Csource, int specular_power) : base(Csource)
        {
            this.specular_power = specular_power;
        }
    }
}
