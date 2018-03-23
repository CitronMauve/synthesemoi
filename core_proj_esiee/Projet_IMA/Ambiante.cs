namespace Projet_IMA
{
    class Ambiante : Lampe
    {
        private float Intensite;

        public Ambiante(float intensite) {
            Intensite = intensite;
        }
        

        public Couleur Illuminer(Couleur couleur)
        {
            return couleur * Intensite;
        }
    }
}
