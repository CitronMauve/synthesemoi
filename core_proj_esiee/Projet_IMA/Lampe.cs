using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Lampe
    {
        protected Couleur Csource;

        public Lampe(Couleur Csource)
        {
            this.Csource = Csource;
        }
    }
}
