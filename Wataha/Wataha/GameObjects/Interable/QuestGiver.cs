using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameSystem;
using Wataha.GameSystem.Animation;
using WatahaSkinnedModel;

namespace Wataha.GameObjects.Interable
{
    class QuestGiver : GameObject
    {
        public bool ifColisionTerrain;
        public float colliderSize;
        public Vector3 LastMove;
        public Vector3 position;
        public float angle;

        public QuestGiver questsGiverNeedToStart = null;
        public List<Quest> questsList;
        public List<Quest> questCompleted;
        public Quest actualQuest = null;
        public GameObject reward;


        public AnimationPlayer animationPlayer;
        public AnimationSystem animationSystem;
        float animationOffset = 0;

        public QuestGiver(Model model, Matrix world) : base(world,model)
        {
            questsList = new List<Quest>();
            questCompleted = new List<Quest>();

            position = world.Translation;
            position = position + new Vector3(0, -1f, 0);
            angle = 180;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90)) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);
           
            foreach (ModelMesh mesh in model.Meshes)
            {
                mesh.BoundingSphere = BoundingSphere.CreateFromBoundingBox(collider);
            }
        }

        public override void Draw()
        {

            base.Draw();
        }

        public override void Draw(Camera camera,string technique)
        {

            base.Draw(camera,technique);
        }

        // Update is called once per frame
        public override void Update(GameTime gameTime)
        {
            if (actualQuest != null)
                if (actualQuest.questStatus.Equals(Quest.status.SUCCED))
                    CompletedQuest();
        }

       public void Init()
        {
            if (questsList.Count > 0)
                actualQuest = questsList[0];
        }

        public void CompletedQuest()
        {
            questCompleted.Add(actualQuest);
            if ((questsList.IndexOf(actualQuest) + 1) < questsList.Count)
            {
                actualQuest = questsList[questsList.IndexOf(actualQuest) + 1];
            }
            else
            {
                actualQuest = null;
            }
        }
    }
}
