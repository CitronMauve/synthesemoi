using System;
using System.Collections.Generic;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static List<Object> objects;
        //public static List<Lampe> lampes;
        public static Ambiante ambiante;
        public static Diffuse diffuse;

        public static void Go()
        {
            objects = new List<Object>();
            Sphere vert = new Sphere(100, new V3(200f, 0f, 200f), new Couleur(0f, 0.7f, 0f));
            objects.Add(vert);
            Sphere rouge = new Sphere(100, new V3(350f, 0f, 200f), new Couleur(0.7f, 0f, 0f));
            objects.Add(rouge);

            //lampes = new List<Lampe>();
            ambiante = new Ambiante(1.0f);
            //lampes.Add(ambiante);

            //  Direction , Couleur
            diffuse = new Diffuse(new V3(1, 1, 1), new Couleur(1, 0, 1));
            //lampes.Add(diffuse);


            Draw();
            /*
            Texture T1 = new Texture("brick01.jpg");
           
            int larg = 600;
            int haut = 300;
            float r_x = 1.5f;   // repetition de la texture en x
            float r_y = 1.0f;   // repetition de la texture en y
            float pas = 0.001f;

            for (float u = 0 ; u < 1 ; u+=pas)  // echantillonage fnt paramétrique
            for (float v = 0 ; v < 1 ; v+=pas)
                {
                    int x = (int) (u * larg + 10); // calcul des coordonnées planes
                    int y = (int) (v * haut + 15);

                    Couleur c = T1.LireCouleur(u * r_x, v * r_y);
                    
                    BitmapEcran.DrawPixel(x,y,c );
                   
                }

            // dessin sur l'image pour comprendre l'orientation axe et origine du Bitmap
            
            Couleur Red = new Couleur(1.0f, 0.0f, 0.0f);
            for (int i = 0; i < 1000; i++)
                BitmapEcran.DrawPixel(i, i, Red);

            Couleur Green = new Couleur(0.0f, 1.0f, 0.0f);
            for (int i = 0; i < 1000; i++)
                BitmapEcran.DrawPixel(i, 1000-i, Green);

            // test des opérations sur les vecteurs

            V3 t = new V3(1, 0, 0);
            V3 r = new V3(0, 1, 0);
            V3 k = t + r;
            float p = k * t * 2;
            V3 n = t ^ r;
            V3 m = -t;
            */
        }

        public static void Draw()
        {
            // ZBuffer
            int[,] zbuffer = ZBuffer();
            foreach (Sphere sphere in objects)
            {
                sphere.DessinerSphere(zbuffer, ambiante, diffuse);
            }
            /*
            // Lampe ambiante 30%
            foreach(Sphere objet in objects)
            {
                objet.couleur.R *= 1.3f;
                objet.couleur.V *= 1.3f;
                objet.couleur.B *= 1.3f;
            }
            foreach(Sphere objet in objects)
            {
                objet.DessinerSphere(zbuffer);
            }
            */
        }

        public static int[,] ZBuffer()
        {
            int longueurEcran = BitmapEcran.GetWidth();
            int hauteurEcran = BitmapEcran.GetHeight();
            int[,] zbuffer = new int[hauteurEcran, longueurEcran];

            for (int i = 0; i < hauteurEcran; i++)
            {
                for (int j = 0; j < longueurEcran; j++)
                {
                    zbuffer[i, j] = 9;
                }
            }

            return zbuffer;
            
        }
    }
}
