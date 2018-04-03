using System.Collections.Generic;

namespace Projet_IMA
{
    abstract class Objet
    {
        protected Couleur couleur;
        protected Texture texture;

        public Objet(Couleur couleur)
        {
            this.couleur = couleur;
        }
        public Objet(Texture texture)
        {
            this.texture = texture;
        }

        abstract public void Draw(V3 camera, int[,] zbuffer, List<Lampe> lampes);
    }
}
