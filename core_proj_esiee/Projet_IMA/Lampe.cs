using System;

namespace Projet_IMA
{
    class Lampe
    {
        private float ambiante_intensity;
        private V3 direction;
        private Couleur color;
        private int specular_power;

        public Lampe() { }

        public Lampe(float ambiante_intensity, V3 direction, Couleur color, int specular_power)
        {
            this.ambiante_intensity = ambiante_intensity;
            this.direction = direction;
            this.direction.Normalize();
            this.color = color;
            this.specular_power = specular_power;
        }

        public Couleur ambianteEffect(Couleur couleurPoint)
        {
            return couleurPoint * ambiante_intensity;
        }

        public Couleur diffuseEffect(Couleur pointColor, V3 normalPoint)
        {
            return (this.color * pointColor) * Math.Max(0, -this.direction * normalPoint);
        }

        public Couleur specularEffect(V3 cameraPosition, V3 normalPoint)
        {
            V3 reflection = this.direction + 2 * (-this.direction * normalPoint * normalPoint);
            return this.color * (float) (Math.Pow(Math.Max(0, reflection * cameraPosition), this.specular_power));
        }

        public Couleur allEffects(Couleur colorPoint, V3 normalPoint, V3 cameraPosition)
        {
            return ambianteEffect(colorPoint) + diffuseEffect(colorPoint, normalPoint) + specularEffect(cameraPosition, normalPoint);
        }
    }
}
