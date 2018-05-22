using System.Collections.Generic;

namespace Projet_IMA
{
    abstract class Objet
    {
        protected Couleur couleur;
        protected Texture texture;
        protected Texture bump;

        public Objet(Couleur couleur)
        {
            this.couleur = couleur;
        }

        public Objet(Couleur couleur, Texture bump)
        {
            this.couleur = couleur;
            this.bump = bump;
        }

        public Objet(Texture texture)
        {
            this.texture = texture;
        }

        public Objet(Texture texture, Texture bump)
        {
            this.texture = texture;
            this.bump = bump;
        }

        public V3 BumpNormale(V3 normale, float u, float v)
        {
            float k = 2f;
            this.bump.Bump(u, v, out float dhdu, out float dhdv);

            return normale + k * ((CalculerDeriveeU(u, v) ^ (dhdv * normale)) + ((dhdu * normale) ^ CalculerDeriveeV(u, v)));
        }

        public Couleur LampesEffectsOnCouleur(List<Lampe> lampes, Couleur couleur, V3 normale, V3 camera)
        {
            Couleur couleurAffichee;
            couleurAffichee = new Couleur();
            foreach (Lampe lampe in lampes)
            {
                couleurAffichee += lampe.allEffects(couleur, normale, camera);
            }
            return couleurAffichee;
        }

        abstract public V3 Calculer(float u, float v);
        abstract public V3 CalculerDeriveeU(float u, float v);
        abstract public V3 CalculerDeriveeV(float u, float v);
        abstract public void Draw(V3 camera, int[,] zbuffer, List<Lampe> lampes);
    }
}
