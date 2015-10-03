using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
namespace csharp.OurCode
{
    class AggroBreedingV1
    {
        public AI Parent;

        public AggroBreedingV1(AI Parent)
        {
            this.Parent = Parent;
        }
        int m_minDist, m_dist, m_creatureID;
        /*BREEDING CALCULATIONS FOR START OF GAME
         * 
        */
/*
        void startBreed()
        {
            m_minDist = 50;
            for(int i = 0; i < BaseAI.creatures.Length; i++)
            {
                if ((BaseAI.creatures[i].isBreeding == false) && (BaseAI.creatures[i].Owner == BaseAI.playerID()))
                {
                    for (int j = 0; j < BaseAI.creatures.Length; j++)
                    {
                        if ((BaseAI.creatures[j].isBreeding == false) && (BaseAI.creatures[j].Owner == BaseAI.playerID()) && (i != j))
                        {
                            m_dist = (BaseAI.creatures[i].X - BaseAI.creatures[j].X) + (BaseAI.creatures[i].Y - BaseAI.creatures[j].Y);
                            if (m_minDist > m_dist)
                            {
                                m_minDist = m_dist;
                                m_creatureID = j;
                            }
                        }
                    }
                    //at the conclusion of the inner loop calls a move function having i mate with j
                    BaseAI.creatures[i].isBreeding = true;
                    BaseAI.creatures[j].isBreeding = true;

                }
            }
        }
    }
}
*/