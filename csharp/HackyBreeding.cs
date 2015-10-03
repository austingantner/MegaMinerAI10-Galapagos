using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp
{
    class HackyBreeding
    {
        public static void MatingSeason(int childcount)
        {
            List<Creature> AvailableMates = new List<Creature>();
            foreach (Creature c in BaseAI.creatures)
            {
                if (c.Owner == BaseAI.playerID() && c.CurrentHealth >= 80)
                {
                    bool added = false;
                    for (int i = 0; i < AvailableMates.Count; i++)
                    {
                        if (AvailableMates[i].GenericHeuristic <= c.GenericHeuristic)
                        {
                            AvailableMates.Insert(i, c);
                            added = true;
                            break;
                        }
                    }
                    if (!added)
                    {
                        AvailableMates.Add(c);
                    }
                }
            }

            for (int i = 0; i < childcount; i++)
            {
                if (AvailableMates.Count >= 2)
                {
                    Breed(AvailableMates[0], AvailableMates[1]);
                    AvailableMates.RemoveRange(0, 2);
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
