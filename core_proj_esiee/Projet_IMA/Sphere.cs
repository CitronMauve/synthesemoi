using System;
using System.Collections.Generic;

namespace Projet_IMA
{
    class Sphere
    {
        private V3 centre;
        private float rayon;
        private Couleur couleur;
        private Texture texture;

        public Sphere(float rayon, V3 centre, Couleur couleur, Texture texture)
        {
            this.rayon = rayon;
            this.centre = centre;
            this.couleur = couleur;
            this.texture = texture;
        }

        public V3 Calculer(float u, float v)
        {
            return new V3(
                (float) (Math.Cos(v) * Math.Cos(u)),
                (float) (Math.Cos(v) * Math.Sin(u)),
                (float) Math.Sin(v)
            );
        }
        

        public void DessinerSphere(int[,] ZBuffer, List<Lampe> lampes)
        {
            float step = 0.01f;
            for (float u = 0; u <= 2 * Math.PI; u += step)
            {
                for (float v = -(float) Math.PI / 2; v <= (float) Math.PI / 2; v += step)
                {
                    V3 currentPoint;
					currentPoint = Calculer(u, v);

					int x = (int) (currentPoint.x * this.rayon + this.centre.x);
                    int y = (int) (currentPoint.y * this.rayon + this.centre.y);
                    int z = (int) (currentPoint.z * this.rayon + this.centre.z);

                    if (y < ZBuffer[(int) x, (int) z]) 
					{
						Couleur couleurAffichee;
						V3 normale;
						V3 camera;

						if (texture != null)
                        {
							// TODO fix couleur with the following
							// this.couleur = this.texture.LireCouleur(u / (float) (2 * Math.PI), v / (float) Math.PI + 0.5f);
							this.couleur = this.texture.LireCouleur(u / (float) (2 * Math.PI), -v / (float) Math.PI + 0.5f);
						}

						couleurAffichee = new Couleur();

						normale = currentPoint * this.rayon;
						normale.Normalize();

						camera = new V3(BitmapEcran.GetWidth() / 2, BitmapEcran.GetWidth() * 1.5f, BitmapEcran.GetHeight());
						camera.Normalize();

						foreach (Lampe lampe in lampes) {
							couleurAffichee += lampe.allEffects(this.couleur, normale, camera);
						}

                        BitmapEcran.DrawPixel(x, z, couleurAffichee);

                        ZBuffer[x, z] = y;
                    }
                }
            }
        }
    }
}
