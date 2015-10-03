using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// BREEDING V1 CALCULATED EVOLUTION
namespace csharp.OurCode
{
    class BreedingV1
    {
        public AI Parent;

        public BreedingV1(AI Parent)
        {
            this.Parent = Parent;
        }

        //BREEDING CALCULATIONS FOR START OF GAME (no total stat checking as all creatures will have same total stat values)
        /*
        * breeding best case scenario:
         * z = (x+y)/2 + 2 where z is total points of child x is total points of parent who initiated, and y is total points of parent who was breeded with
         * 
         * how to calculate who to breed with:
         *     gets the stats of two creatures and calculates the difference between each stat it will then calculate how many of the differences are odd 
         *     (best case at start is 4 as it is impossible to have 5 odd unless there is an even and an odd number)
         *     if numOdd is 4 then the two will meet to breed if not it will compare the value to the previous max num odd and if its greater it will replace that with
         *     numOdd and store the j value
         *     the function will then at completion of the inner for loop assuming that it did not encounter a pair with 4 odd it will store the i and j values and the 
         *     numOdd between the two of them 
         *     
         *     at the completion of the outer for loop the program will check to see what creatures have/are breeding then will 
         *     
         *     this process will repeat with each other creature that has not already been marked for breeding until 2x creatures are chosen where x is the desired number of
         *     children
         *     
        */

        public static void breedcreature(int children)
        {
            //VARIABLES:
            int m_numCreatures = 0, m_difEnergy = 0, m_difCarn = 0, m_difHerb = 0, m_difSpeed = 0, m_difDefense = 0, m_numOdd = 0, m_maxnumOdd = 0;
            //int [,] m_effArray = new int[BaseAI.creatures.Length,3]; <----- CHRISTIANS BLACK WITCH MAGIC
            // \/ sensible way of making a 2D array
            int[][] ary = new int[BaseAI.creatures.Length][];
            for(int i = 0; i < BaseAI.creatures.Length; i++)
            {
                ary[i] = new int[3]; 
            }
            int k = 0;
            
            //calculates the stat difference between each creature
            for (int i = 0; i < BaseAI.creatures.Length; i++)
            {
                if ((BaseAI.creatures[i].Owner == BaseAI.playerID()) && BaseAI.creatures[i].CurrentHealth >= 90)
                {
                    m_numCreatures++;
                    for (int j = 0; j < BaseAI.creatures.Length; j++)
                    {
                        if ((BaseAI.creatures[j].Owner == BaseAI.playerID()) && (i != j) && BaseAI.creatures[j].CurrentHealth >= 90)
                        {
                            m_difEnergy =Math.Abs(BaseAI.creatures[i].Energy - BaseAI.creatures[i].Energy);
                            m_difCarn = Math.Abs(BaseAI.creatures[i].Carnivorism - BaseAI.creatures[i].Carnivorism);
                            m_difHerb = Math.Abs(BaseAI.creatures[i].Herbivorism - BaseAI.creatures[i].Herbivorism);
                            m_difSpeed = Math.Abs(BaseAI.creatures[i].Speed - BaseAI.creatures[i].Speed);
                            m_difDefense = Math.Abs(BaseAI.creatures[i].Defense - BaseAI.creatures[i].Defense);

                            if (1 == (m_difEnergy % 2))
                                m_numOdd++;
                            if (1 == (m_difCarn % 2))
                                m_numOdd++;
                            if (1 == (m_difHerb % 2))
                                m_numOdd++;
                            if (1 == (m_difSpeed % 2))
                                m_numOdd++;
                            if (1 == (m_difDefense % 2))
                                m_numOdd++;
                            if (5 == m_numOdd)
                            {
                                //call the function to make i and j breed
                                ary[k][1] = i;
                                ary[k][2] = j;
                                ary[k][3] = m_numOdd;
                                break;
                            }
                            if (m_maxnumOdd < m_numOdd)
                            {
                                m_maxnumOdd = m_numOdd;
                            }
                            ary[k][1] = i;
                            ary[k][2] = j;
                            ary[k][3] = m_numOdd;

                            /*
                            m_dist = (BaseAI.creatures[i].X - BaseAI.creatures[j].X) + (BaseAI.creatures[i].Y - BaseAI.creatures[j].Y);
                            if (m_minDist > m_dist)
                            {
                                m_minDist = m_dist;
                                m_creatureID = j;
                            }
                            */
                        }

                    }
                }
            }
            //optimizes breeding and breaks out upon desired number of children designated to breed
            int pairs = 0;
            for (int i = 0; i < m_numCreatures; i++)
            {
                if (pairs == children)
                    if (true != BaseAI.creatures[ary[i][1]].isBreeding && true != BaseAI.creatures[ary[i][2]].isBreeding)
                    {
                        if (5 == ary[i][3])
                        {
                            //call MAKE [i][2] and [i][1] BREED
                            pairs++;
                            BaseAI.creatures[ary[i][1]].isBreeding = true;
                            BaseAI.creatures[ary[i][2]].isBreeding = true;
                            Breed(BaseAI.creatures[ary[i][1]], BaseAI.creatures[ary[i][2]]);

                        }
                    }
            }
            if (pairs != children)
            {
                for (int i = 0; i < m_numCreatures; i++)
                {
                    if (pairs == children)
                        if (true != BaseAI.creatures[ary[i][1]].isBreeding && true != BaseAI.creatures[ary[i][2]].isBreeding)
                        {
                            if (4 == ary[i][3])
                            {
                                //call MAKE [i][2] and [i][1] BREED
                                pairs++;
                                BaseAI.creatures[ary[i][1]].isBreeding = true;
                                BaseAI.creatures[ary[i][2]].isBreeding = true;
                                Breed(BaseAI.creatures[ary[i][1]], BaseAI.creatures[ary[i][2]]);

                            }
                        }
                }
            }
            if (pairs != children)
            {
                for (int i = 0; i < m_numCreatures; i++)
                {
                    if (pairs == children)
                        break;
                    if (true != BaseAI.creatures[ary[i][1]].isBreeding && true != BaseAI.creatures[ary[i][2]].isBreeding )
                    {
                        if (3 == ary[i][3])
                        {
                            //call MAKE [i][2] and [i][1] BREED
                            pairs++;
                            BaseAI.creatures[ary[i][1]].isBreeding = true;
                            BaseAI.creatures[ary[i][2]].isBreeding = true;
                            Breed(BaseAI.creatures[ary[i][1]], BaseAI.creatures[ary[i][2]]);
                        }
                    }
                }
            }
            if (pairs != children)
            {
                for (int i = 0; i < m_numCreatures; i++)
                {
                    if (pairs == children)
                        break;
                    if (true != BaseAI.creatures[ary[i][1]].isBreeding && true != BaseAI.creatures[ary[i][2]].isBreeding )
                    {
                        if (2 == ary[i][3])
                        {
                            //call MAKE [i][2] and [i][1] BREED
                            pairs++;
                            BaseAI.creatures[ary[i][1]].isBreeding = true;
                            BaseAI.creatures[ary[i][2]].isBreeding = true;
                            Breed(BaseAI.creatures[ary[i][1]], BaseAI.creatures[ary[i][2]]);
                        }
                    }
                }
            }
            if (pairs != children)
            {
                for (int i = 0; i < m_numCreatures; i++)
                {
                    if (pairs == children)
                        break;
                    if (true != BaseAI.creatures[ary[i][1]].isBreeding && true != BaseAI.creatures[ary[i][2]].isBreeding )
                    {
                        if (1 == ary[i][3])
                        {
                            //call MAKE [i][2] and [i][1] BREED

                            pairs++;
                            BaseAI.creatures[ary[i][1]].isBreeding = true;
                            BaseAI.creatures[ary[i][2]].isBreeding = true;
                            Breed(BaseAI.creatures[ary[i][1]], BaseAI.creatures[ary[i][2]]);
                        }
                    }
                }
            }
            if (pairs != children)
            {
                for (int i = 0; i < m_numCreatures; i++)
                {
                    if (pairs == children)
                        break;
                    if (true != BaseAI.creatures[ary[i][1]].isBreeding && true != BaseAI.creatures[ary[i][2]].isBreeding )
                    {
                        if (0 == ary[i][3])
                        {
                            //call MAKE [i][2] and [i][1] BREED
                            pairs++;
                            BaseAI.creatures[ary[i][1]].isBreeding = true;
                            BaseAI.creatures[ary[i][2]].isBreeding = true;
                            Breed(BaseAI.creatures[ary[i][1]], BaseAI.creatures[ary[i][2]]);
                        }
                    }
                }
            }
        }

        public static void Breed(Creature c1, Creature c2)
        {
            Movement.GetTo(c1, c2.X, c2.Y, true);
            Movement.GetTo(c2, c1.X, c1.Y, true);
            if (c1.CanBreed > 0 && ((Math.Abs(c1.X - c2.X) == 1 && c1.Y == c2.Y) || (c1.X == c2.X && Math.Abs(c1.Y - c2.Y) == 1)))
            {
                c1.breed(c2);
            }
        }
    }
}

