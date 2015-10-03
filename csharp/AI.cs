using System;
using System.Runtime.InteropServices;
using csharp;
using csharp.OurCode;
///The class implementing gameplay logic.
class AI : BaseAI
{
    public override string username()
    {
        return "i-am-error";
    }
    public override string password()
    {
        return "aeshuyei";
    }

    //This function is called each time it is your turn
    //Return true to end your turn, return false to ask the server for updated information
    public override bool run()
    {
        try
        {
            if (turnNumber() < 25)
            {
                HackyBreeding.MatingSeason(1);
            }
            else if (turnNumber() < 460)
            {
                HackyBreeding.MatingSeason(1);
            }
            else
            {
                HackyBreeding.MatingSeason(5);
            }
            //Iterate through every creature
            //for (int ii = 0; ii < creatures.Length; ii++)
            foreach (Creature c in creatures)
            {//if I own the creature
                try
                {
                    if (c.Owner == playerID())
                    {
                        foreach (Creature c2 in creatures)
                        {
                            if (c.Owner != playerID())
                            {
                                if (functions.shouldAttack(c2, c))
                                {
                                    Movement.GetTo(c, c2.X, c2.Y);
                                    if (c.CanEat > 0 && ((Math.Abs(c.X - c2.X) == 1 && c.Y == c2.Y) || (c.X == c2.X && Math.Abs(c.Y - c2.Y) == 1)))
                                    {
                                        c.eat(c2.X, c2.Y);
                                    }
                                }
                            }
                        }
                        if ((double)c.CurrentHealth / (double)c.MaxHelth <= .35 || turnNumber() < 14)
                        {
                            int plantid = functions.findPlant(c.X, c.Y);
                            Movement.GetTo(c, plants[plantid].X, plants[plantid].Y, true);
                        }
                        else
                        {
                            int enemyid = functions.closestEnemy(c.X, c.Y);
                            Creature e = creatures[enemyid];
                            Movement.GetTo(c, e.X, e.Y);
                            if (c.CanEat > 0 && ((Math.Abs(c.X - e.X) == 1 && c.Y == e.Y) || (c.X == e.X && Math.Abs(c.Y - e.Y) == 1)))
                            {
                                c.eat(e.X, e.Y);
                                if (c.MovementLeft > 0)
                                {
                                    int plantid = functions.findPlant(c.X, c.Y);
                                    Movement.GetTo(c, plants[plantid].X, plants[plantid].Y);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return true;
    }

    //This function is called once, before your first turn
    public override void init() { }

    //This function is called once, after your last turn
    public override void end() { }


    public AI(IntPtr c)
        : base(c)
    { }
}




////Iterate through every creature
//      for (int ii = 0; ii < creatures.Length; ii++)
//      {//if I own the creature
//          if (creatures[ii].Owner == playerID())
//          { //check if there is a plant to that creature's left
//              int plantIn = getPlantAtLocation(creatures[ii].X + 1, creatures[ii].Y);
//              //if there is no plant to my left, or there is a plant of size 0, and there is no creature to my left
//              if ((plantIn == -1 || (plantIn != 1 && plants[plantIn].Size == 0)) && getCreatureAtLocation(creatures[ii].X + 1, creatures[ii].Y) == -1)
//              {//if x is in the range of the map, and y is in the range of the map
//                  if (0 <= creatures[ii].X + 1 && creatures[ii].X + 1 < mapWidth() && 0 <= creatures[ii].Y && creatures[ii].Y < mapHeight())
//                  {//if I have ennough health to move, and have movment left
//                      if (creatures[ii].CurrentHealth > healthPerMove() && creatures[ii].MovementLeft > 0)
//                      {//move creature to the left by incrementing its x cooridinate, and not changing its y
//                          creatures[ii].move(creatures[ii].X + 1, creatures[ii].Y);
//                      }
//                  }
//              }

//              //check if there is a plant to my left
//              plantIn = getPlantAtLocation(creatures[ii].X + 1, creatures[ii].Y);
//              //check if there is a creature to my left
//              int creatIn = getCreatureAtLocation(creatures[ii].X + 1, creatures[ii].Y);
//              //if there is a plant to my left, and its size is > 0, and this creature has not already eaten this turn       
//              if (plantIn != -1 && plants[plantIn].Size > 0 && creatures[ii].CanEat == 1)
//              {//eat the plant, using its x and y
//                  creatures[ii].eat(plants[plantIn].X, plants[plantIn].Y);
//              }
//              //else if there is a creature to my left, and it is not my creature and this creature has not eaten yet        
//              else if (creatIn != -1 && creatures[creatIn].Owner != playerID() && creatures[ii].CanEat == 1)
//              {//take a bite out of this creature
//                  creatures[ii].eat(creatures[creatIn].X, creatures[creatIn].Y);
//              }
//              //else if there is a creature to my left, and it is my creature, and neither has bred this turn        
//              else if (creatIn != -1 && creatures[creatIn].Owner == playerID() && creatures[ii].CanBreed == 1 && creatures[creatIn].CanBreed == 1)
//              {//if both creatures have enough health to breed
//                  if (creatures[ii].CurrentHealth > healthPerBreed() && creatures[creatIn].CurrentHealth > healthPerBreed())
//                  {//breed with this creature
//                      creatures[ii].breed(creatures[creatIn]);
//                  }
//              }
//          }
//      }