using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Interable
{
    public class SheepQuest : Quest
    {

        public List<Animal> sheeps = new List<Animal>();
        public int sheepInCroft = 0;
        public int eatSheep = 0;
        int totalSheep = 5;
        Static.Environment croft;

        public SheepQuest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward, GameObject game, Static.Environment croft, ContentManager content, Wolf wolf) : base(questId, questTitle, questDescription, needStrenght, needResistance, needSpeed, meatReward, whiteFangReward, goldFangReward, game)
        {

            this.croft = croft;
            for(int i = 0; i < totalSheep; i++)
                    GenerateSheeps(wolf, content);

        }

        public override void Update()
        {
            CheckSheepsGetCroft();
            //CheckKilledSheeps(wolf);
        }

        public override bool IfCompleted(Wolf wolf)
        {
            if (Vector3.Distance(questDestination.model.Meshes[0].BoundingSphere.Center, wolf.model.Meshes[0].BoundingSphere.Center) < 10.0f && (eatSheep + sheepInCroft) == totalSheep )
                  return true;
            else
                  return false;
        }


        void CheckKilledSheeps(Wolf wolf)
        {
            List<Animal> tmp = new List<Animal>();
            foreach (Animal a in sheeps)
            {
                if (Vector3.Distance(a.position, wolf.position) <= 6 && InputSystem.newKeybordState.IsKeyDown(Keys.E) && InputSystem.oldKeybordState.IsKeyUp(Keys.E))
                {
                    AudioSystem.playGrowl(0);
                    a.active = false;
                    tmp.Add(a);
                    eatSheep++;
                    Resources.Meat += 30;
                }
            }
            foreach (Animal a in tmp)
            {
                sheeps.Remove(a);
            }
        }

        void CheckSheepsGetCroft()
        {
            List<Animal> tmp = new List<Animal>();
            foreach (Animal a in sheeps)
            {
                if (Vector3.Distance(a.sphere.Center, croft.model.Meshes[0].BoundingSphere.Center) <= 19 && InputSystem.newKeybordState.IsKeyDown(Keys.E) && InputSystem.oldKeybordState.IsKeyUp(Keys.E))
                {
                    a.active = false;
                    tmp.Add(a);
                    sheepInCroft++;
                }
            }
            foreach (Animal a in tmp)
            {
                sheeps.Remove(a);
            }
        }

        public void GenerateSheeps(Wolf wolf, ContentManager Content)
        {
            List<Vector3> spawns = new List<Vector3>()
            {
            new Vector3(20.0f, 2.2f, -3300f),
            new Vector3(0.0f, 2.2f, -310f),
            new Vector3(25.0f, 2.2f, -320f),
            new Vector3(15.0f, 2.2f, -330f),
            new Vector3(10.0f, 2.2f, -325f),
            new Vector3(40.0f, 2.2f, -350f)
            };
            Random rand = new Random();


            Matrix world = new Matrix();
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world *= Matrix.CreateTranslation(spawns[rand.Next(0, 6)]);

            Dictionary<String, String> animations = new Dictionary<string, string>();
            animations.Add("Move", "SheepM");
            Animal sheep = new Animal(wolf, Content.Load<Model>("SheepM/SheepIdle_000001"), animations, Content, world, 16, 8, "sheep");
            sheep.ajustHeight(-1.05f);
            sheep.animations["Move"].frameSpeed = 0.01f;
            sheep.animationSystem.animation.generateTags();
            sheep.animationSystem.animation.SetEffect(Content.Load<Effect>("Effects/Light"), true);
            sheeps.Add(sheep);
        }


    }
}
