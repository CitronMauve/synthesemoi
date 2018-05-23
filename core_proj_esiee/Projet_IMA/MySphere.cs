using System;
using System.Collections.Generic;

namespace Projet_IMA
{
    class MySphere : Objet
    {
        private float rayon;
        private V3 centre;

        // Constructor avec texture
        public MySphere(float rayon, V3 centre, Texture texture) : base(texture)
        {
            this.rayon = rayon;
            this.centre = centre;
        }

        // Constructor avec texture et bump
        public MySphere(float rayon, V3 centre, Texture texture, Texture bump) : base(texture, bump)
        {
            this.rayon = rayon;
            this.centre = centre;
        }

        // Constructor avec couleur
        public MySphere(float rayon, V3 centre, Couleur couleur) : base(couleur)
        {
            this.rayon = rayon;
            this.centre = centre;
        }

        // Constructor avec texture et bump
        public MySphere(float rayon, V3 centre, Couleur couleur, Texture bump) : base(couleur, bump)
        {
            this.rayon = rayon;
            this.centre = centre;
        }

        public override V3 Calculer(float u, float v)
        {
            return new V3(
                (float)(Math.Cos(v) * Math.Cos(u)),
                (float)(Math.Cos(v) * Math.Sin(u)),
                (float)Math.Sin(v)
            );
        }

        public override V3 CalculerDeriveeU(float u, float v)
        {
            return new V3(
                (float)(Math.Cos(v) * -Math.Sin(u)),
                (float)(Math.Cos(v) * Math.Cos(u)),
                0
            );
        }

        public override V3 CalculerDeriveeV(float u, float v)
        {
            return new V3(
                (float)(-Math.Sin(v) * Math.Cos(u)),
                (float)(-Math.Sin(v) * Math.Sin(u)),
                (float)Math.Cos(v)
            );
        }

        public override V3 BumpNormale(V3 normale, float u, float v)
        {
            float k = 2f;
            this.bump.Bump(u, v, out float dhdu, out float dhdv);

            V3 deriveeU = CalculerDeriveeU(u, v);
            V3 deriveeV = CalculerDeriveeV(u, v);

            return normale + k * (
                (deriveeU ^ (dhdv * normale)) +
                ((dhdu * normale) ^ deriveeV)
            );
        }

        public override void Draw(V3 camera, int[,] zbuffer, List<Lampe> lampes)
        {
            float step = 0.005f;
            float halfPi = (float)Math.PI / 2;

            for (float u = 0; u <= 2 * Math.PI; u += step)
            {
                for (float v = -halfPi; v <= halfPi; v += step)
                {
                    V3 currentPoint;
                    currentPoint = Calculer(u, v);

                    int x = (int)(currentPoint.x * this.rayon + this.centre.x);
                    int y = (int)(currentPoint.y * this.rayon + this.centre.y);
                    int z = (int)(currentPoint.z * this.rayon + this.centre.z);

                    if (x < 0 || x > BitmapEcran.GetWidth() ||
                        z < 0 || z > BitmapEcran.GetHeight())
                    {
                        continue;
                    }

                    if (y < zbuffer[z, x])
                    {
                        Couleur couleurAffichee;
                        V3 normale;
                        V3 bumpNormale;

                        normale = currentPoint * this.rayon;
                        normale.Normalize();

                        if (this.texture != null)
                        {
                            this.couleur = this.texture.LireCouleur(
                                u / (float)(2 * Math.PI), 
                                -v / (float)Math.PI + 0.5f
                            );
                        }

                        if (this.bump != null)
                        {
                            bumpNormale = BumpNormale(
                                normale,
                                u / (float)(2 * Math.PI),
                                -v / (float)Math.PI + 0.5f
                            );

                            bumpNormale.Normalize();
                            normale = bumpNormale;
                        }

                        couleurAffichee = LampesEffectsOnCouleur(lampes, this.couleur, normale, camera);

                        BitmapEcran.DrawPixel(x, z, couleurAffichee);
                        zbuffer[z, x] = y;
                    }
                }
            }
        }
    }
}
