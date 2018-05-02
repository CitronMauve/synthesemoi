using System;
using System.Collections.Generic;

namespace Projet_IMA
{
    class Sphere : Objet
    {
        private float rayon;
        private V3 centre;

        public Sphere(float rayon, V3 centre, Texture texture) : base(texture)
        {
            this.rayon = rayon;
            this.centre = centre;
        }

        public Sphere(float rayon, V3 centre, Texture texture, Texture bump)
            : base(texture, bump)
        {
            this.rayon = rayon;
            this.centre = centre;
        }

        public Sphere(float rayon, V3 centre, Couleur couleur) : base(couleur)
        {
            this.rayon = rayon;
            this.centre = centre;
        }

        public override V3 Calculer(float u, float v)
        {
            return new V3(
                (float) (Math.Cos(v) * Math.Cos(u)),
                (float) (Math.Cos(v) * Math.Sin(u)),
                (float) Math.Sin(v)
            );
        }

        public V3 CalculerDeriveeU(float u, float v)
        {
            return new V3(
                (float) (Math.Cos(v) * -Math.Sin(u)),
                (float) (Math.Cos(v) * Math.Cos(u)),
                0
            );
        }

        public V3 CalculerDeriveeV(float u, float v)
        {
            return new V3(
                (float)(-Math.Sin(v) * Math.Cos(u)),
                (float)(-Math.Sin(v) * Math.Sin(u)),
                (float) Math.Cos(v)
            );
        }

        public V3 BumpNormale(V3 normale, float u, float v)
        {
            float k = 2f;
            this.bump.Bump(u, v, out float dhdu, out float dhdv);

            V3 bumpNormale = normale + k * (
                (CalculerDeriveeU(u, v) ^ (dhdv * normale)) +
                ((dhdu * normale) ^ CalculerDeriveeV(u, v))
            );
            return bumpNormale;
        }

        public override void Draw(V3 camera, int[,] zbuffer, List<Lampe> lampes)
        {
            float step = 0.01f;
            float halfPi = (float) Math.PI / 2;
            for (float u = 0; u <= 2 * Math.PI; u += step)
            {
                for (float v = - halfPi; v <= halfPi; v += step)
                {
                    V3 currentPoint;
                    currentPoint = Calculer(u, v);

                    int x = (int) (currentPoint.x * this.rayon + this.centre.x);
                    int y = (int) (currentPoint.y * this.rayon + this.centre.y);
                    int z = (int) (currentPoint.z * this.rayon + this.centre.z);

                    if (y < zbuffer[z, x])
                    {
                        Couleur couleurAffichee;
                        V3 normale;
                        V3 bumpNormale;

                        normale = currentPoint * this.rayon;
                        normale.Normalize();

                        if (this.texture != null)
                        {
                            // TODO fix couleur with the following
                            // this.couleur = this.texture.LireCouleur(u / (float) (2 * Math.PI), v / (float) Math.PI + 0.5f);
                            this.couleur = this.texture.LireCouleur(
                                u / (float) (2 * Math.PI),
                                -v / (float) Math.PI + 0.5f);

                            if (this.bump != null) {

                            bumpNormale = BumpNormale(
                                normale,
                                u / (float)(2 * Math.PI),
                                -v / (float)Math.PI + 0.5f);

                            bumpNormale.Normalize();
                            normale = bumpNormale;
                            }
                        }

                        couleurAffichee = new Couleur();

                        foreach (Lampe lampe in lampes)
                        {
                            couleurAffichee += lampe.allEffects(
                                this.couleur, normale, camera);
                        }

                        BitmapEcran.DrawPixel(x, z, couleurAffichee);

                        zbuffer[z, x] = y;
                    }
                }
            }
        }
    }
}
