using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wataha.GameObjects.Materials
{
    public class PPPointLight : Material
    {
       public Vector3 Color;
        public Vector3 Position;
        public float Attenuation;

        public PPPointLight(Vector3 lightPosition, Vector3 lightColor, float lightAttenuation)
        {
           Color = lightColor;
          Position = lightPosition;
            Attenuation = lightAttenuation;
        }

        public override void SetEffectParameters(Effect effect)
        {
            if (effect.Parameters["LightColor"] != null)
                effect.Parameters["LightColor"].SetValue(Color);

            if (effect.Parameters["LightPosition"] != null)
                effect.Parameters["LightPosition"].SetValue(Position);

            if (effect.Parameters["LightAttenuation"] != null)
                effect.Parameters["LightAttenuation"].SetValue(Attenuation);


        }
    }
}
