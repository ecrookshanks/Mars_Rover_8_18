using MarsRover_8_18;
using System;
using System.Linq;

namespace MarsRover
{
  public class Rover
  {

    private char[] VALID_COMMANDS = { 'F', 'B', 'L', 'R' };
    private IObstacleService ObstacleService;

    public RoverCoordinates Coordinates { get; set; }

    public int xPos { get { return Coordinates.xPos; } set { Coordinates.xPos = value; } }
    public int yPos { get { return Coordinates.yPos; } set { Coordinates.yPos = value; } }

    public Direction Direction { get; set; }

    public char[] Commands { get; set; }

    public Rover()
    {
      this.ObstacleService = new ObstacleService();
      Coordinates = new RoverCoordinates(0, 0);
      Direction = Direction.North;
    }

    public Rover(RoverCoordinates roverCoordinates, Direction dir)
    {
      this.Coordinates = roverCoordinates;
      this.Direction = dir;
    }

    public Rover(IObstacleService svc) : this()
    {
      this.ObstacleService = svc;
    }

    public void StoreCommands(char[] commands)
    {
      this.Commands = commands;
    }

    public (bool foundObstacle, Obstacle obstacle) ProcessAllCommands()
    {
      bool found = false;
      Obstacle foundObstacle = null;

      foreach (char command in this.Commands)
      {
        (bool canMove, Obstacle ob) moveResult = this.ProcessSingleCommand(command);
        if (moveResult.canMove == false)
        {
          return (true, moveResult.ob);
        }
      }

      return (found, foundObstacle);
    }

    public (bool canMove, Obstacle ob) ProcessSingleCommand(char command)
    {
      if (!VALID_COMMANDS.Contains(command))
      {
        throw new BadCommandException("Invalid Command Character");
      }

      RoverCoordinates current = this.Coordinates;

      RoverCoordinates proposed = current;

      if (command.Equals('F'))
      {
        switch (this.Direction)
        {
          case Direction.North:
            proposed.yPos++;
            break;
          case Direction.West:
            proposed.xPos--;
            break;
          case Direction.South:
            proposed.yPos--;
            break;
          case Direction.East:
            proposed.xPos++;
            break;
          default:
            break;
        }
      }
      else if(command.Equals('B'))
      {
        switch(this.Direction)
        {
          case Direction.North:
            proposed.yPos--;
            break;
          case Direction.West:
            proposed.xPos++;
            break;
          case Direction.South:
            proposed.yPos++;
            break;
          case Direction.East:
            proposed.xPos--;
            break;
          default:
            break;
        }
      }
      else if (command.Equals('L'))
      {

        switch (this.Direction)
        {
          case Direction.North:
          case Direction.West:
          case Direction.South:
            this.Direction++;
            break;
          case Direction.East:
            this.Direction = Direction.North;
            break;
          default:
            break;
        }
        return (true, null);
      }
      else // command.equals('R')
      {
        switch (this.Direction)
        {
          case Direction.North:
            this.Direction = Direction.East;
            break;
          case Direction.West:
          case Direction.South:
          case Direction.East:
            this.Direction--;
            break;
          default:
            break;
        }
        return (true, null);
      }

      // check if proposed move is into an obstacle
      bool canMove = !(ObstacleService.IsPointAnObstacle(proposed.xPos, proposed.yPos));

      if (canMove)
      {
        MoveRover(proposed);
        return (true, null);
      }
      else
      {
        return (false, new Obstacle(proposed.xPos, proposed.yPos));
      }

    }

    private void MoveRover(RoverCoordinates proposed)
    {
      this.Coordinates = proposed;
    }
  }
}