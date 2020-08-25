using System;
using System.ComponentModel;
using Xunit;

namespace MarsRover.Tests
{
  public class CommandTests
  {
    [Fact]
    public void RoverShouldHaveDefaultCoords()
    {
      Rover r = new Rover();

      int xCoord = r.xPos;
      int yCoord = r.yPos;

      Direction d = r.Direction;

      Assert.Equal(0, xCoord);
      Assert.Equal(0, yCoord);
      Assert.Equal(Direction.North, d);

    }

    [Fact]
    public void RoverCanRecieveInitialCoordsAndDirection()
    {
      Rover r = new Rover(new RoverCoordinates(3, 3), Direction.South);

      int xPos = r.xPos;
      int yPos = r.yPos;
      Direction d = r.Direction;

      Assert.Equal(3, xPos);
      Assert.Equal(3, yPos);
      Assert.Equal(Direction.South, d);
    }

    [Fact]
    public void RoverCanReceiveAndStoreCommands()
    {
      Rover r = new Rover();  // default coords and direction

      char[] commands = { 'F', 'R', 'F', 'R' };

      r.StoreCommands(commands);

      Assert.NotNull(r.Commands);
      Assert.Equal(4, r.Commands.Length);
      
    }

    [Fact]
    public void BadCommandThrowsException()
    {
      Rover r = new Rover();

      Assert.Throws<BadCommandException>(() => r.ProcessSingleCommand('S'));

    }

    [Fact]
    public void RoverCanProcessSingleCommandWithoutStoring()
    {
      Rover r = new Rover(); // starting at (0,0), facing North

      char command = 'F';
      RoverCoordinates dest = new RoverCoordinates(0, 1);

      r.ProcessSingleCommand(command);  // should move to (0,1), facing North

      Assert.True(r.Coordinates.Equals(dest));

    }

    [Fact]
    public void RoverCanRotateToOriginalPosition()
    {
      Rover r = new Rover();
      char command = 'L';

      r.ProcessSingleCommand(command);
      r.ProcessSingleCommand(command);
      r.ProcessSingleCommand(command);
      r.ProcessSingleCommand(command);

      Assert.Equal(Direction.North, r.Direction);

      command = 'R';

      r.ProcessSingleCommand(command);
      r.ProcessSingleCommand(command);
      r.ProcessSingleCommand(command);
      r.ProcessSingleCommand(command);

      Assert.Equal(Direction.North, r.Direction);

    }

    [Fact]
    public void RoverCanProcessDirectionChangeCommands()
    {
      Rover r = new Rover();  // default coords and direction

      char[] commands = { 'R', 'R', 'R', 'L' };

      r.StoreCommands(commands);

      r.ProcessAllCommands();

      Assert.Equal(Direction.South, r.Direction);

    }

    [Fact]
    public void RoverCanNavigateInASquare()
    {
      Rover r = new Rover();  // default coords and direction

      char[] commands = { 'F', 'L', 'F', 'L', 'F', 'F', 'L', 'F', 'F', 'L', 'F', 'L', 'F' };

      r.StoreCommands(commands);
      r.ProcessAllCommands();

      // Should end back at the starting point
      RoverCoordinates dest = new RoverCoordinates(0, 0);

      Assert.True(dest.Equals(r.Coordinates));

    }

    [Fact]
    public void RoverCanMoveForwardFourSpaces()
    {
      Rover r = new Rover();  // default coords and direction

      char[] commands = { 'F', 'F', 'F', 'F' };

      r.StoreCommands(commands);
      r.ProcessAllCommands();

      // Should move forward 4 spots
      RoverCoordinates dest = new RoverCoordinates(0, 4);

      Assert.True(dest.Equals(r.Coordinates));

    }

  }
}
