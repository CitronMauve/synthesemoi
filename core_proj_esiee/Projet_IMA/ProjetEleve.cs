using System.Collections.Generic;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public const string BRICK = "brick01.jpg";
        public const string BUMP = "bump.jpg";
        public const string BUMP1 = "bump1.jpg";
        public const string BUMP20 = "bump20.jpg";
        public const string BUMP38 = "bump38.jpg";
        public const string FIBRE = "fibre.jpg";
        public const string GOLD = "gold.jpg";
        public const string GOLD_BUMP = "gold_Bump.jpg";
        public const string LEAD = "lead.jpg";
        public const string LEAD_BUMP = "lead_bump.jpg";
        public const string ROCK = "rock.jpg";
        public const string STONE2 = "stone2.jpg";
        public const string TEST = "test.jpg";
        public const string UVTEST = "uvtest.jpg";
        public const string WOOD = "wood.jpg";

        public const string VOIE_LACTEE = "milky.jpg";
        public const string SOLEIL = "sunmap.jpg";
        public const string MERCURY = "mercurymap.jpg";
        public const string MERCURY_BUMP = "mercurybump.jpg";
        public const string VENUS = "venusmap.jpg";
        public const string VENUS_BUMP = "venusbump.jpg";
        public const string TERRE = "earthmap1k.jpg";
        public const string TERRE_BUMP = "earthbump1k.jpg";
        public const string MARS = "mars_1k_color.jpg";
        public const string MARS_BUMP = "marsbump1k.jpg";
        public const string JUPITER = "jupiter2_4k.jpg";
        public const string SATURNE = "saturnmap.jpg";
        public const string URANUS = "uranusmap.jpg";
        public const string NEPTUNE = "neptunemap.jpg";


        public static V3 camera;
        public static List<Objet> objects;
        public static List<Lampe> lampes;

        public static void Go()
        {
            camera = new V3(BitmapEcran.GetWidth() / 2, BitmapEcran.GetWidth() * 1.5f, BitmapEcran.GetHeight());
            camera.Normalize();

            objects = new List<Objet> {
              // Sphere verte
              // new MySphere(100, new V3(356, 40, 168), new Couleur(0, 1, 0)),

              // Spere rouge bump
              // new MySphere(100, new V3(683, 12, 236), new Couleur(1, 0, 0), new Texture(LEAD_BUMP)),

              // Sphere texture
              // new MySphere(100, new V3(300, 0, 400), new Texture(LEAD)),

              // Sphere texture bump
              // new MySphere(100, new V3(200, 0, 200), new Texture(GOLD), new Texture(GOLD_BUMP)),

              // Rectangle
              new MyRectangle(new V3(250, 0, 250), new V3(500, 0, 250), new V3(250, 0, 500), new Texture(BRICK), new Texture(BRICK)),

              // Voie lactee
              new MyRectangle(new V3(0, 0, 0), new V3(BitmapEcran.GetWidth(), 0, 0), new V3(0, 0, BitmapEcran.GetHeight()), new Texture(VOIE_LACTEE), new Texture(VOIE_LACTEE)),

              // Soleil
              // new MySphere(100, new V3(0, 0, 300), new Texture(SOLEIL)),
              // Mercure
              new MySphere(18, new V3(50, 0, 300), new Texture(MERCURY), new Texture(MERCURY_BUMP)),
              // Venus
              new MySphere(30, new V3(120, 0, 300), new Texture(VENUS), new Texture(VENUS_BUMP)),
              // Terre
              new MySphere(40, new V3(215, 0, 300), new Texture(TERRE), new Texture(TERRE_BUMP)),
              // Mars
              new MySphere(28, new V3(300, 0, 300), new Texture(MARS), new Texture(MARS_BUMP)),
              // Jupiter
              new MySphere(150, new V3(500, 0, 300), new Texture(JUPITER)),
              // Saturne
              new MySphere(130, new V3(810, 0, 300), new Texture(SATURNE)),
              // Uranus
              new MySphere(80, new V3(1050, 0, 300), new Texture(URANUS)),
              // Neptune
              new MySphere(85, new V3(1250, 0, 300), new Texture(NEPTUNE)),
            };

            lampes = new List<Lampe> {
              // Lampe blanche
              new Lampe(0.4f, new V3(-1, 0, 0), new Couleur(1, 1, 1), 40),
              // Lampe bleue
              new Lampe(0f, new V3(1, 1, 0), new Couleur(0, 0, 0.5f), 40)
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
