using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp
{
    static class Movement
    {
        /// <summary>
        /// Move towards a target location
        /// </summary>
        /// <param name="c">Creature to move</param>
        /// <param name="x">X or location</param>
        /// <param name="y">Y of location</param>
        /// <returns></returns>
        public static bool GetTo(Creature c, int x, int y)
        {
            return GetTo(c, x, y, false);
        }
        /// <summary>
        /// Move towards a target location
        /// </summary>
        /// <param name="c">Creature to move</param>
        /// <param name="x">X or location</param>
        /// <param name="y">Y of location</param>
        /// <param name="EatInPassing">False for no. if you are on your way to attack say no to foods</param>
        /// <returns></returns>
        public static bool GetTo(Creature c, int x, int y, bool EatInPassing)
        {
            try
            {
                int Safeguard = 35;
                int Stuck = 0;
                if (EatInPassing)
                {
                    EatInPassingFn(c);
                }
                while (c.MovementLeft > 0 && Safeguard > 0)
                {
                    Safeguard--;
                    Random rand = new Random();
                    switch (rand.Next(0, 2))
                    {
                        case 0:
                            if (c.X > x)
                            {
                                if (LegalMove(c.X - 1, c.Y))
                                {
                                    c.move(c.X - 1, c.Y);
                                }
                                else
                                {
                                    Stuck++;
                                }
                            }
                            else if (c.X < x)
                            {
                                if (LegalMove(c.X + 1, c.Y))
                                {
                                    c.move(c.X + 1, c.Y);
                                }
                                else
                                {
                                    Stuck++;
                                }
                            }
                            break;
                        case 1:
                            if (c.Y > y)
                            {
                                if (LegalMove(c.X, c.Y - 1))
                                {
                                    c.move(c.X, c.Y - 1);
                                }
                                else
                                {
                                    Stuck++;
                                }
                            }
                            else if (c.Y < y)
                            {
                                if (LegalMove(c.X, c.Y + 1))
                                {
                                    c.move(c.X, c.Y + 1);
                                }
                                else
                                {
                                    Stuck++;
                                }
                            }
                            break;
                    }
                    if (EatInPassing)
                    {
                        EatInPassingFn(c);
                    }
                    if (((Math.Abs(c.X - x) == 1 && c.Y == y) || (c.X == x && Math.Abs(c.Y - y) == 1)))
                    {
                        return true;
                    }
                    if (Stuck > 4)
                    {
                        Stuck = 0;
                        UnstuckMe(c);
                    }
                }

                if (c.MovementLeft > 0)
                {
                    UnstuckMe(c);
                }
                if (c.X == x && c.Y == y)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool LegalMove(int x, int y)
        {
            if ((-1 == BaseAI.getPlantAtLocation(x, y) || BaseAI.plants[BaseAI.getPlantAtLocation(x, y)].Size == 0)
                && -1 == BaseAI.getCreatureAtLocation(x, y) && x < BaseAI.mapWidth() && x >= 0 && y < BaseAI.mapHeight() && y >= 0)
                return true;
            return false;
        }

        private static void EatInPassingFn(Creature c)
        {
            try
            {
                if (c.CanEat > 0)
                {
                    int plantid = BaseAI.getPlantAtLocation(c.X + 1, c.Y);
                    if (-1 != plantid && BaseAI.plants[plantid].Size > 0)
                    {
                        c.eat(c.X + 1, c.Y);
                    }
                    else
                    {
                        plantid = BaseAI.getPlantAtLocation(c.X - 1, c.Y);
                        if (-1 != plantid && BaseAI.plants[plantid].Size > 0)
                        {
                            c.eat(c.X - 1, c.Y);
                        }
                        else
                        {
                            plantid = BaseAI.getPlantAtLocation(c.X, c.Y + 1);
                            if (-1 != plantid && BaseAI.plants[plantid].Size > 0)
                            {
                                c.eat(c.X, c.Y + 1);
                            }
                            else
                            {
                                plantid = BaseAI.getPlantAtLocation(c.X, c.Y - 1);
                                if (-1 != plantid && BaseAI.plants[plantid].Size > 0)
                                {
                                    c.eat(c.X, c.Y - 1);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in EatInPassingFn");
            }
            // BreedInPassing(c);
        }

        private static void BreedInPassing(Creature c)
        {
            try
            {
                if (c.CanBreed > 0 && c.CurrentHealth >= 105)
                {
                    int mateid = BaseAI.getCreatureAtLocation(c.X + 1, c.Y);
                    if (-1 != mateid && BaseAI.creatures[mateid].Owner == BaseAI.playerID() && BaseAI.creatures[mateid].CurrentHealth >= 105)
                    {
                        c.breed(BaseAI.creatures[mateid]);
                    }
                    else
                    {
                        mateid = BaseAI.getCreatureAtLocation(c.X - 1, c.Y);
                        if (-1 != mateid && BaseAI.creatures[mateid].Owner == BaseAI.playerID() && BaseAI.creatures[mateid].CurrentHealth >= 105)
                        {
                            c.breed(BaseAI.creatures[mateid]);
                        }
                        else
                        {
                            mateid = BaseAI.getCreatureAtLocation(c.X, c.Y + 1);
                            if (-1 != mateid && BaseAI.creatures[mateid].Owner == BaseAI.playerID() && BaseAI.creatures[mateid].CurrentHealth >= 105)
                            {
                                c.breed(BaseAI.creatures[mateid]);
                            }
                            else
                            {
                                mateid = BaseAI.getCreatureAtLocation(c.X, c.Y - 1);
                                if (-1 != mateid && BaseAI.creatures[mateid].Owner == BaseAI.playerID() && BaseAI.creatures[mateid].CurrentHealth >= 105)
                                {
                                    c.breed(BaseAI.creatures[mateid]);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in breedInPassingFn");
            }
        }

        //private static void AttackInPassingFn(Creature c)
        //{
        //    try
        //    {
        //        if (c.CanEat > 0)
        //        {
        //            if (-1 != BaseAI.getCreatureAtLocation(c.X + 1, c.Y))
        //            {
        //                c.eat(c.X + 1, c.Y);
        //            }
        //            else if (-1 != BaseAI.getCreatureAtLocation(c.X - 1, c.Y))
        //            {
        //                c.eat(c.X - 1, c.Y);
        //            }
        //            else if (-1 != BaseAI.getCreatureAtLocation(c.X, c.Y + 1))
        //            {
        //                c.eat(c.X, c.Y + 1);
        //            }
        //            else if (-1 != BaseAI.getCreatureAtLocation(c.X, c.Y - 1))
        //            {
        //                c.eat(c.X, c.Y - 1);
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        private static void UnstuckMe(Creature c)
        {
            try
            {
                EatInPassingFn(c);
                if (c.MovementLeft > 0)
                {
                    Random rand = new Random();
                    switch (rand.Next(0, 4))
                    {
                        case 0:
                            if (LegalMove(c.X - 1, c.Y))
                                c.move(c.X - 1, c.Y);
                            break;
                        case 1:
                            if (LegalMove(c.X + 1, c.Y))
                                c.move(c.X + 1, c.Y);
                            break;
                        case 2:
                            if (LegalMove(c.X, c.Y - 1))
                                c.move(c.X, c.Y - 1);
                            break;
                        case 3:
                            if (LegalMove(c.X, c.Y + 1))
                                c.move(c.X, c.Y + 1);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("error in unstuckme");
            }
        }
    }
}
