using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wataha.GameSystem.ParticleSystem
{
    struct  ParticleVertex : IVertexType
    {
        Vector3 startPosition;
        Vector2 uv;
        Vector3 direction;
        float speed;
        float startTime;

        public Vector3 StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }
        public Vector2 UV
        {
            get { return uv; }
            set { uv = value; }
        }
        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public float StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public ParticleVertex(Vector3 StartPosition, Vector2 UV, Vector3 Direction, float Speed, float StartTime)
        {
            this.startPosition = StartPosition;
            this.uv = UV;
            this.direction = Direction;
            this.speed = Speed;
            this.startTime = StartTime;
        }
        public readonly static VertexDeclaration VertexDeclaration = 
            new VertexDeclaration
            (new VertexElement(0, VertexElementFormat.Vector3,
                // Start position    
                VertexElementUsage.Position, 0), 
                new VertexElement(12, VertexElementFormat.Vector2,     
                    // UV coordinates  
                    VertexElementUsage.TextureCoordinate, 0),   
                new VertexElement(20, VertexElementFormat.Vector3,  
                    // Movement direction 
                    VertexElementUsage.TextureCoordinate, 1),    
                new VertexElement(32, VertexElementFormat.Single,    
                    // Movement speed     
                    VertexElementUsage.TextureCoordinate, 2), 
                new VertexElement(36, VertexElementFormat.Single,   
                    // Start time 
                    VertexElementUsage.TextureCoordinate, 3)   
            );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexDeclaration; }
        }


    }
}
