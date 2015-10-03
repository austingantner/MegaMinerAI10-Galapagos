using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp.OurCode
{
    class functions
    {
        public static int findPlant(int x, int y)
        {
            //Checking plant by plant (no adjecent plants)
            int distance = 0;
            int shortest = 52;
            int shortestPlantID = 0;
            for (int j = 0; j < BaseAI.plants.Length; j++)
            {
                distance = (Math.Abs(x - BaseAI.plants[j].X) + Math.Abs(y - BaseAI.plants[j].Y)); // i to j
                if (shortest == distance && BaseAI.plants[j].Size != 0)
                {
                    shortestPlantID = (BaseAI.plants[j].Size > BaseAI.plants[shortestPlantID].Size) ? shortestPlantID : j; // i to j
                }
                else if (distance < shortest && BaseAI.plants[j].Size != 0) // was j 
                {
                    shortest = distance;
                    shortestPlantID = j; //was j
                }
            }
            return shortestPlantID;
        }

        public static int closestEnemy(int x, int y)
        {
            int distance = 0;
            int shortest = 1000;
            int shortestID = 0;
            for (int i = 0; i < BaseAI.creatures.Length; i++)
            {
                distance = (Math.Abs(x - BaseAI.creatures[i].X) + Math.Abs(y - BaseAI.creatures[i].Y));
                if (distance == shortest && BaseAI.creatures[i].Owner != BaseAI.playerID())
                {
                    shortestID = (BaseAI.creatures[i].CurrentHealth > BaseAI.creatures[shortestID].CurrentHealth) ? shortestID : i;
                }
                else if (distance < shortest && BaseAI.creatures[i].Owner != BaseAI.playerID())
                {
                    shortest = distance;
                    shortestID = i;
                }
            }

            return shortestID;
        }

        public static bool shouldAttack(Creature atk, Creature us)
        {
            int atkFor = ((us.Carnivorism-atk.Defense)*10);
            //checking for min damage output
            atkFor = (atkFor < 10) ? 10 : atkFor; 

            //calculating range and distance to targer            
            int distance = Math.Abs(us.X - atk.X) + Math.Abs(us.Y - atk.Y);
            

            //checking if shouldAttack (if damage > health & withing range)
            return atkFor > atk.CurrentHealth && us.Speed >= distance;
        }



        /*
        public static int closestToPack(Creature a, Creature b, Creature c)
        {
            int dx = 0;
            int dy = 0;
            int dz = 0;
            int closestID = 0;
            if (closestEnemy(a.X, a.Y) == closestEnemy(b.X, b.Y) && closestEnemy(a.X, a.Y) == closestEnemy(c.X, c.Y))
            {
                closestID = closestEnemy(a.X, a.Y);
            }
            else
            {

            }
            return 0;
        }
         */

    }

}