import com.sun.jna.Pointer;

/// \brief A basic AI interface.

///This class implements most the code an AI would need to interface with the lower-level game code.
///AIs should extend this class to get a lot of builer-plate code out of the way
///The provided AI class does just that.
public abstract class BaseAI
{
  static Mappable[] mappables;
  static Creature[] creatures;
  static Plant[] plants;
  static Player[] players;
  Pointer connection;
  static int iteration;
  boolean initialized;

  public BaseAI(Pointer c)
  {
    connection = c;
  }

  ///
  ///Make this your username, which should be provided.
  public abstract String username();
  ///
  ///Make this your password, which should be provided.
  public abstract String password();
  ///
  ///This is run on turn 1 before run
  public abstract void init();
  ///
  ///This is run every turn . Return true to end the turn, return false
  ///to request a status update from the server and then immediately rerun this function with the
  ///latest game status.
  public abstract boolean run();

  ///
  ///This is run on after your last turn.
  public abstract void end();


  public boolean startTurn()
  {
    iteration++;
    int count = 0;
    count = Client.INSTANCE.getMappableCount(connection);
    mappables = new Mappable[count];
    for(int i = 0; i < count; i++)
    {
      mappables[i] = new Mappable(Client.INSTANCE.getMappable(connection, i));
    }
    count = Client.INSTANCE.getCreatureCount(connection);
    creatures = new Creature[count];
    for(int i = 0; i < count; i++)
    {
      creatures[i] = new Creature(Client.INSTANCE.getCreature(connection, i));
    }
    count = Client.INSTANCE.getPlantCount(connection);
    plants = new Plant[count];
    for(int i = 0; i < count; i++)
    {
      plants[i] = new Plant(Client.INSTANCE.getPlant(connection, i));
    }
    count = Client.INSTANCE.getPlayerCount(connection);
    players = new Player[count];
    for(int i = 0; i < count; i++)
    {
      players[i] = new Player(Client.INSTANCE.getPlayer(connection, i));
    }

    if(!initialized)
    {
      initialized = true;
      init();
    }
    return run();
  }


  ///How many turns it has been since the beginning of the round
  int turnNumber()
  {
    return Client.INSTANCE.getTurnNumber(connection);
  }
  ///Player Number; either 0 or 1
  int playerID()
  {
    return Client.INSTANCE.getPlayerID(connection);
  }
  ///What number game this is for the server
  int gameNumber()
  {
    return Client.INSTANCE.getGameNumber(connection);
  }
  ///The width of the map
  int mapWidth()
  {
    return Client.INSTANCE.getMapWidth(connection);
  }
  ///The height of the map
  int mapHeight()
  {
    return Client.INSTANCE.getMapHeight(connection);
  }
  ///The amount of energy required from each creature in order to breed.
  int healthPerBreed()
  {
    return Client.INSTANCE.getHealthPerBreed(connection);
  }
  ///The amount of energy required to try to move.
  int healthPerMove()
  {
    return Client.INSTANCE.getHealthPerMove(connection);
  }
  ///The amount of energy lost from the passage of time.
  int healthPerTurn()
  {
    return Client.INSTANCE.getHealthPerTurn(connection);
  }
  ///The base amount of health that each creature starts with
  int baseHealth()
  {
    return Client.INSTANCE.getBaseHealth(connection);
  }
  ///If there is a creature at location (x,y), returns the index of that creature.
  ///If there is no creature there, returns -1.
  int getCreatureAtLocation(int x, int y)
  {
    for(int ii=0;ii<creatures.length;ii++)
    {
      if (creatures[ii].getX()==x && creatures[ii].getY()==y)
      {
        return ii;
      }
    }
    return -1;
  }
  ///If there is a plant at location (x,y), returns the index of that plant.
  ///If there is no plant there, returns -1.
  int getPlantAtLocation(int x, int y)
  {
    for(int ii=0;ii<plants.length;ii++)
    {
      if (plants[ii].getX()==x && plants[ii].getY()==y)
      {
        return ii;
      }
    }
    return -1;
  }
}
