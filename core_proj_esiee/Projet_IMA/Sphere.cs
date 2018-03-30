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

        // public void DessinerSphere(int[,] ZBuffer, Ambiante ambiante, Diffuse diffuse, Specular speculaire)
        public void DessinerSphere(int[,] ZBuffer, Lampe lampe)
        {
            float step = 0.01f;
            for (float u = 0; u <= 2 * Math.PI; u += step)
            {
                for (float v = -(float) Math.PI / 2; v <= (float) Math.PI / 2; v += step)
                {
                    V3 currentPoint = Calculer(u, v);
                    if ((int) (currentPoint.y * this.rayon + this.centre.y) < ZBuffer[(int) (currentPoint.x * rayon + centre.x), (int) (currentPoint.z * rayon + centre.z)])
                    {
                        Couleur couleurAffichee;

                        Couleur couleurAmbiante;
                        Couleur couleurDiffuse;
                        Couleur couleurSpeculaire;

                        // ambiante
                        couleurAmbiante = lampe.ambianteEffect(this.couleur);

                        // diffuse
                        V3 normale = currentPoint * this.rayon;
                        normale.Normalize();
                        couleurDiffuse = lampe.diffuseEffect(this.couleur, normale);

                        // speculaire
                        V3 camera = new V3(BitmapEcran.GetWidth() / 2, BitmapEcran.GetWidth() * 1.5f, BitmapEcran.GetHeight());
                        camera.Normalize();
                        couleurSpeculaire = lampe.specularEffect(camera, normale);

                        couleurAffichee = couleurAmbiante + 
                            couleurDiffuse +
                            couleurSpeculaire; 

                        BitmapEcran.DrawPixel(
                                (int) (currentPoint.x * this.rayon + this.centre.x),
                                (int) (currentPoint.z * this.rayon + this.centre.z),
                                couleurAffichee
                            );

                        ZBuffer[(int)(currentPoint.x * rayon + centre.x), (int)(currentPoint.z * rayon + centre.z)] = (int) (currentPoint.y * this.rayon + this.centre.y);
                    }
                }
            }
        }
    }
}
