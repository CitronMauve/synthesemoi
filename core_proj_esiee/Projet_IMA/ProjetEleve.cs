﻿using System.Collections.Generic;

namespace Projet_IMA
{
    static class ProjetEleve
    {
		public const string BRICK		= "brick01.jpg";
		public const string BUMP		= "bump.jpg";
		public const string BUMP1		= "bump1.jpg";
		public const string BUMP20		= "bump20.jpg";
		public const string BUMP38		= "bump38.jpg";
		public const string FIBRE		= "fibre.jpg";
		public const string GOLD		= "gold.jpg";
		public const string GOLD_BUMP   = "gold_Bump.jpg";
		public const string LEAD		= "lead.jpg";
		public const string LEAD_BUMP   = "lead_bump.jpg";
		public const string ROCK		= "rock.jpg";
		public const string STONE2		= "stone2.jpg";
		public const string TEST		= "test.jpg";
		public const string UVTEST		= "uvtest.jpg";
		public const string WOOD		= "wood.jpg";

        public static V3 camera;
		public static List<Objet> objects;
        public static List<Lampe> lampes;

        public static void Go()
        {
            camera = new V3(BitmapEcran.GetWidth() / 2, BitmapEcran.GetWidth() * 1.5f, BitmapEcran.GetHeight());
            camera.Normalize();

            objects = new List<Objet> {
				// Sphere verte
				new MySphere(100, new V3(356, 40, 168), new Couleur(0, 1, 0)),
				// Spere rouge
				new MySphere(100, new V3(683, 12, 236), new Couleur(1, 0, 0)),
                // Sphere stylée
                new MySphere(100, new V3(300, -40, 103), new Couleur((float) (169.0f/255.0f), (float) (10.0f/255.0f), (float) (90.0f/255.0f))),
                // new Sphere(50, new V3(300, -40, 103), new Couleur(0, 0, 0)),

                new MySphere(
                    100,
                    new V3(200, 0, 200),
                    new Texture(GOLD),
                    new Texture(GOLD_BUMP)
                ),
                new MySphere(
                    100,
                    new V3(300, 0, 400),
                    new Texture(LEAD)
                ),
                new Rectangle(new V3(1, 1, 1), new V3(1, 2, 1), new V3(2, 4, 2), new Couleur(1, 0, 0))
            };

			lampes = new List<Lampe> {
				// Lampe blanche
				new Lampe(0.4f, new V3(1, -1, 1), new Couleur(1, 1, 1), 40),
				// Lampe bleue
				// new Lampe(0f, new V3(1, 1, 1), new Couleur(0, 0, 1), 40)
			};

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
            */
        }

        public static void Draw()
        {
			int[,] zbuffer = ZBuffer();
            foreach (Objet objet in objects)
            {
                objet.Draw(camera, zbuffer, lampes);
            }
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
