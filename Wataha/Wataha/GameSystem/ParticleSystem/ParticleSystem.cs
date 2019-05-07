using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Wataha.GameSystem.ParticleSystem
{
    public class ParticleSystem
    {
        VertexBuffer verts;
        IndexBuffer ints;

        GraphicsDevice graphicsDevice;
        Effect effect;

        int nParticles;
        Vector2 particleSize;
        float lifespan = 1;
        Vector3 wind;
        Texture2D texture;
        float fadeInTime;

        ParticleVertex[] particles;
        int[] indices;

        int activeStart = 0, nActive = 0;

        DateTime start;


        public ParticleSystem(GraphicsDevice graphicsDevice, ContentManager content, Texture2D tex, int nParticles, Vector2 particleSize, float lifespan, Vector3 wind, float FadeInTime)
        {
            this.nParticles = nParticles;
            this.particleSize = particleSize;
            this.lifespan = lifespan;
            this.graphicsDevice = graphicsDevice;
            this.wind = wind;
            this.texture = tex;
            this.fadeInTime = FadeInTime;
            verts = new VertexBuffer(graphicsDevice, typeof(ParticleVertex), nParticles * 4, BufferUsage.WriteOnly);
            ints = new IndexBuffer(graphicsDevice, IndexElementSize.ThirtyTwoBits, nParticles * 6, BufferUsage.WriteOnly);

            generateParticles();

            effect = content.Load<Effect>("Effects/Particle");
            start = DateTime.Now;
        }

        void generateParticles()
        {
            particles = new ParticleVertex[nParticles * 4];
            indices = new int[nParticles * 6];
            Vector3 z = Vector3.Zero;
           
            int x = 0;

            for (int i = 0; i < nParticles * 4; i += 4)
            {
                particles[i + 0] = new ParticleVertex(z, new Vector2(1, 1), z, 0, -1);
                particles[i + 1] = new ParticleVertex(z, new Vector2(0, 1), z, 0, -1);
                particles[i + 2] = new ParticleVertex(z, new Vector2(1, 0), z, 0, -1);
                particles[i + 3] = new ParticleVertex(z, new Vector2(0, 0), z, 0, -1);
                indices[x++] = i + 5;
                indices[x++] = i + 4;
                indices[x++] = i + 3;

                indices[x++] = i + 2;
                indices[x++] = i + 1;
                indices[x++] = i + 3;

                //indices[x++] = i + 0;
                //indices[x++] = i + 3;
                //indices[x++] = i + 1;
                //indices[x++] = i + 2;
                //indices[x++] = i + 2;
              
                //indices[x++] = i + 0; 
            }
        }

        public void AddParticle(Vector3 Position, Vector3 Direction, float Speed)
        {
            if (nActive + 4 == nParticles * 4) return;

            int index = offsetIndex(activeStart, nActive);
            nActive += 4;
            float startTime = (float)(DateTime.Now - start).TotalSeconds;
            Position += new Vector3(-3.3f, -0.5f, -18);
            for (int i = 0; i < 4; i++)
            {
                particles[index + i].StartPosition = Position;
                particles[index + i].Direction = Direction;
                particles[index + i].Speed = Speed;
                particles[index + i].StartTime = startTime;
            }
        }

        int offsetIndex(int start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                start++;

                if (start == particles.Length)
                    start = 0;
            }
            return start;
        }


        public void Update()
        {
            float now = (float)(DateTime.Now - start).TotalSeconds;

            int startIndex = activeStart;
            int end = nActive;

            for (int i = 0; i < end; i++)
            {
                if (particles[activeStart].StartTime < now - lifespan)
                {
                    activeStart++;
                    nActive--;
                    if (activeStart == particles.Length)
                        activeStart = 0;
                }
            }

            verts.SetData<ParticleVertex>(particles);
            ints.SetData<int>(indices);

        }

        public void Draw(Matrix View, Matrix Projection, Vector3 Up, Vector3 Right)
        {
            graphicsDevice.SetVertexBuffer(verts);
            graphicsDevice.Indices = ints;

            effect.Parameters["ParticleTexture"].SetValue(texture);
            effect.Parameters["View"].SetValue(View);
            effect.Parameters["Projection"].SetValue(Projection);
            effect.Parameters["Time"].SetValue((float)(DateTime.Now - start).TotalSeconds);
            effect.Parameters["Lifespan"].SetValue(lifespan);
            effect.Parameters["Wind"].SetValue(wind);
            effect.Parameters["Size"].SetValue(particleSize / 2f);
            effect.Parameters["Up"].SetValue(Up);
            effect.Parameters["Side"].SetValue(Right);
            effect.Parameters["FadeInTime"].SetValue(fadeInTime);

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

            effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, nParticles * 4, 0, nParticles * 2);
            
            graphicsDevice.SetVertexBuffer(null);
            graphicsDevice.Indices = null;


            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default; 
        }



    }
}
