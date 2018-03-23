using System;
using System.Collections.Generic;

namespace Projet_IMA
{
    class Sphere
    {
        public float rayon;
        public V3 centre;
        public Couleur couleur;

        public Sphere(float rayon, V3 centre, Couleur couleur)
        {
            this.rayon = rayon;
            this.centre = centre;
            this.couleur = couleur;
        }

        public V3 Calculer(float u, float v)
        {
            return new V3(
                (float) (Math.Cos(v) * Math.Cos(u)),
                (float) (Math.Cos(v) * Math.Sin(u)),
                (float) Math.Sin(v)
                );
        }

        public void DessinerSphere(int[,] ZBuffer, Ambiante ambiante, Diffuse diffuse)
        {
            float step = 0.01f;
            for (float u = 0; u <= 2 * Math.PI; u += step)
            {
                for (float v = -(float) Math.PI / 2; v <= (float) Math.PI / 2; v += step)
                {
                    V3 P = Calculer(u, v);
                    if ((int) (P.y * this.rayon + this.centre.y) < ZBuffer[(int) (P.x * rayon + centre.x), (int) (P.z * rayon + centre.z)])
                    {
                        Couleur couleurAffichee;

                        Couleur couleurAmbiante = ambiante.Illuminer(this.couleur);

                        V3 normale = P - this.centre;
                        normale.Normalize();
                        Couleur couleurDiffuse = diffuse.Illuminer(this.couleur, normale);

                        

                        couleurAffichee = couleurAmbiante + couleurDiffuse;


                        BitmapEcran.DrawPixel(
                            (int) (P.x * this.rayon + this.centre.x),
                            (int) (P.z * this.rayon + this.centre.z),
                            couleurAffichee
                            );

                        ZBuffer[(int)(P.x * rayon + centre.x), (int)(P.z * rayon + centre.z)] = (int) (P.y * this.rayon + this.centre.y);
                    }
                }
            }
        }
    }
}
