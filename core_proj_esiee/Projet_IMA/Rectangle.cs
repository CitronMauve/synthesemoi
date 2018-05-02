using System;
using System.Collections.Generic;

namespace Projet_IMA
{
  class Rectangle : Objet
  {
        private V3 a;
        private V3 b;
        private V3 c;

        public Rectangle(V3 a, V3 b, V3 c, Texture texture) : base(texture)
        {
          this.a = a;
          this.b = b;
          this.c = c;
        }

        public Rectangle(V3 a, V3 b, V3 c, Couleur couleur) : base(couleur)
        {
          this.a = a;
          this.b = b;
          this.c = c;
        }

        public V3 Calculer(float u, float v)
        {
          V3 vecteurAB = this.b - this.a;
          V3 vecteurAC = this.c - this.a;
          return new V3(this.a + u * vecteurAB + v * vecteurAC);
        }

        public override void Draw(V3 camera, int[,] zbuffer, List<Lampe> lampes)
        {
            float step = 0.01f;
            for (float u = 0; u <= 2 * Math.PI; u += step)
            {
              for (float v = -(float) Math.PI / 2; v <= (float) Math.PI / 2; v += step)
              {
              }
            }
        }
        
    }
}
