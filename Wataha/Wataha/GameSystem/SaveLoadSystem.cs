using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Interable;
using Wataha.GameObjects.Movable;

namespace Wataha.GameSystem
{
    [Serializable]
    public class SaveSystem
    {
        public float Wolf1PositionX { get; set; }
        public float Wolf1PositionY { get; set; }
        public float Wolf1PositionZ { get; set; }
        public int Wolf1Strength { get; set; }
        public int Wolf1Resistance { get; set; }
        public int Wolf1Speed { get; set; }
        public int Wolf1Energy { get; set; }
        public string Wolf1Name { get; set; }
        public float Wolf2PositionX { get; set; }
        public float Wolf2PositionY { get; set; }
        public float Wolf2PositionZ { get; set; }
        public int Wolf2Strength { get; set; }
        public int Wolf2Resistance { get; set; }
        public int Wolf2Speed { get; set; }
        public int Wolf2Energy { get; set; }
        public string Wolf2Name { get; set; }
        public float Wolf3PositionX { get; set; }
        public float Wolf3PositionY { get; set; }
        public float Wolf3PositionZ { get; set; }
        public int Wolf3Strength { get; set; }
        public int Wolf3Resistance { get; set; }
        public int Wolf3Speed { get; set; }
        public int Wolf3Energy { get; set; }
        public string Wolf3Name { get; set; }

        public int Meat { get; set; }
        public int GoldFang { get; set; }
        public int WhiteFang { get; set; }


        public float LightPosX { get; set; }
        public float LightPosY { get; set; }
        public float LightPosZ { get; set; }
        public float LightPower { get; set; }
        public float AmbientPower { get; set; }
        public float LightColorX { get; set; }
        public float LightColorY { get; set; }
        public float LightColorZ { get; set; }
        public float LightColorW { get; set; }
        public double Timer { get; set; }


        public List<List<int>> questCompleted;

        public SaveSystem()
        {
            
        }

        public SaveSystem(Wolf wolf1, Wolf wolf2, Wolf wolf3)
        {
            questCompleted = new List<List<int>>();
            for (int i = 0; i < QuestSystem.questGivers.Count; i++)
            {
                questCompleted.Add(new List<int>());
            }

            Wolf1PositionX = wolf1.position.X;
            Wolf1PositionY = wolf1.position.Y;
            Wolf1PositionZ = wolf1.position.Z;
            Wolf1Strength = wolf1.strength;
            Wolf1Resistance = wolf1.resistance;
            Wolf1Speed = wolf1.speed;
            Wolf1Energy = wolf1.energy;
            Wolf1Name = wolf1.Name;
            Wolf2PositionX = wolf2.position.X;
            Wolf2PositionY = wolf2.position.Y;
            Wolf2PositionZ = wolf2.position.Z;
            Wolf2Strength = wolf2.strength;
            Wolf2Resistance = wolf2.resistance;
            Wolf2Speed = wolf2.speed;
            Wolf2Energy = wolf2.energy;
            Wolf2Name = wolf2.Name;
            Wolf3PositionX = wolf3.position.X;
            Wolf3PositionY = wolf3.position.Y;
            Wolf3PositionZ = wolf3.position.Z;
            Wolf3Strength = wolf3.strength;
            Wolf3Resistance = wolf3.resistance;
            Wolf3Speed = wolf3.speed;
            Wolf3Energy = wolf3.energy;
            Wolf3Name = wolf3.Name;
            Meat = Resources.Meat;
            GoldFang = Resources.Goldfangs;
            WhiteFang = Resources.Whitefangs;
            for (int i = 0; i < QuestSystem.questGivers.Count; i++)
            {
                int j = 0;
                foreach (Quest quest in QuestSystem.questGivers[i].questCompleted)
                {
                    questCompleted[i].Add(j);
                    j++;
                }
            }

            LightPosX = GameObjects.GameObject.lightPos.X;
            LightPosY = GameObjects.GameObject.lightPos.Y;
            LightPosZ = GameObjects.GameObject.lightPos.Z;
            AmbientPower = GameObjects.GameObject.ambientPower;
            LightPower = GameObjects.GameObject.lightPower;
            LightColorX = GameObjects.GameObject.lightColor.X;
            LightColorY = GameObjects.GameObject.lightColor.Y;
            LightColorZ = GameObjects.GameObject.lightColor.Z;
            LightColorW = GameObjects.GameObject.lightColor.W;
            Timer = GameObjects.GameObject.timer;
    }




        public void SaveGame()
        {

            string fileName = "save.txt";

            FileStream fs = new FileStream(fileName, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                if (this != null) formatter.Serialize(fs, this);

                // etc....
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally { fs.Close(); }
        }

    }


}
