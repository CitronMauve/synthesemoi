using System;
using System.Collections.Generic;

namespace Projet_IMA
{
    class MyRectangle : Objet
    {
        private V3 a;
        private V3 b;
        private V3 c;
        private V3 vecteurAB;
        private V3 vecteurAC;
        private V3 normale;

        // Constructor avec texture
        public MyRectangle(V3 a, V3 b, V3 c, Texture texture) : base(texture)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            CalculerVecteurs();
            CalculerNormale();
        }

        // Constructor avec couleur et bump
        public MyRectangle(V3 a, V3 b, V3 c, Texture texture, Texture bump) : base(texture, bump)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            CalculerVecteurs();
            CalculerNormale();
        }

        // Constructor avec couleur
        public MyRectangle(V3 a, V3 b, V3 c, Couleur couleur) : base(couleur)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            CalculerVecteurs();
            CalculerNormale();
        }

        // Constructor avec couleur et bump
        public MyRectangle(V3 a, V3 b, V3 c, Couleur couleur, Texture bump) : base(couleur, bump)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            CalculerVecteurs();
            CalculerNormale();
        }

        private void CalculerVecteurs()
        {
            this.vecteurAB = this.b - this.a;
            this.vecteurAC = this.c - this.a;
        }

        private void CalculerNormale()
        {
            this.normale = this.vecteurAB ^ this.vecteurAC;
            this.normale.Normalize();
        }

        public override V3 Calculer(float u, float v)
        {
            return this.a + (u * this.vecteurAB + v * this.vecteurAC);
        }

        public override V3 CalculerDeriveeU(float u, float v)
        {
            return this.vecteurAB;
        }

        public override V3 CalculerDeriveeV(float u, float v)
        {
            return this.vecteurAC;
        }

        public override V3 BumpNormale(V3 normale, float u, float v)
        {
            float k = 700f;
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
            float step = 0.0002f;
            for (float u = 0; u < 1; u += step)
            {
                for (float v = 0; v < 1; v += step)
                {
                    V3 currentPoint;
                    currentPoint = Calculer(u, v);

                    int x = (int)(currentPoint.x);
                    int y = (int)(currentPoint.y);
                    int z = (int)(currentPoint.z);

                    if (x < 0 || x > BitmapEcran.GetWidth() ||
                        z < 0 || z > BitmapEcran.GetHeight())
                    {
                        continue;
                    }

                    if (y < zbuffer[z, x])
                    {
                        Couleur couleurAffichee;
                        V3 bumpNormale;

                        if (this.texture != null)
                        {
                            this.couleur = this.texture.LireCouleur(u, -v);
                        }

                        if (this.bump != null)
                        {
                            this.normale.Normalize();
                            bumpNormale = BumpNormale(this.normale, u, -v);
                            bumpNormale.Normalize();
                            this.normale = bumpNormale;
                        }

                        couleurAffichee = LampesEffectsOnCouleur(lampes, this.couleur, this.normale, camera);

                        BitmapEcran.DrawPixel(x, z, couleurAffichee);

                        zbuffer[z, x] = y;
                    }
                }
            }
        }
    }
}
