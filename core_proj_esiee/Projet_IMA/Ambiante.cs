using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Ambiante : Lampe
    {
        public float intensite;

        public Ambiante(float intensite)
        {
            this.intensite = intensite;
        }
    }
}
