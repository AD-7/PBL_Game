using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wataha.GameObjects.Materials
{
    public class PointLightMaterial : Material
    {
        public Vector3 AmbientLightColor { get; set; }
        public Vector3 LightPosition { get; set; }
        public Vector3 LightColor { get; set; }
        public float LightAttenuation { get; set; }
        public float LightFalloff { get; set; }


        public PointLightMaterial()
        {
            AmbientLightColor = new Vector3(.15f, .15f, .15f);
            LightPosition = new Vector3(0, 1500, 1500);
            LightColor = new Vector3(1, 1, 1);
            LightAttenuation =10000;
            LightFalloff = 200;
        }

        public override void SetEffectParameters(Effect effect)
        {
         
            if(effect.Parameters["AmbientLightColor"] != null)
                effect.Parameters["AmbientLightColor"].SetValue(AmbientLightColor);
            if (effect.Parameters["LightPosition"] != null)
                effect.Parameters["LightPosition"].SetValue(LightPosition);
            if (effect.Parameters["LightColor"] != null)
                effect.Parameters["LightColor"].SetValue(LightColor);
            if (effect.Parameters["LightAttenuation"] != null)
                effect.Parameters["LightAttenuation"].SetValue(LightAttenuation);
            if (effect.Parameters["LightFalloff"] != null)
                effect.Parameters["LightFalloff"].SetValue(LightFalloff);
        }
    }
}
