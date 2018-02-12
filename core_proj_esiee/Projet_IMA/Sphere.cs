using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public V3 Calculate(float u, float v)
        {
            return new V3(
                (float)(Math.Cos(v) * Math.Cos(u)),
                (float)(Math.Cos(v) * Math.Sin(u)),
                (float)Math.Sin(v)
                );
        }

        public void DrawSphere()
        {
            for (float u = 0; u <= 2 * Math.PI; u += 0.01f)
            {
                for (float v = -(float) Math.PI / 2; v <= (float) Math.PI / 2; v += 0.01f)
                {
                    V3 P = Calculate(u, v);
                    BitmapEcran.DrawPixel(
                        (int) (P.x * this.centre.x + this.rayon),
                        (int) (P.y * this.centre.y + this.rayon),
                        this.couleur
                        );
                }
            }
        }
    }
}
