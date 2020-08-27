using MarsRover_8_18;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MarsRover.Tests
{
  public class ObstacleTests
  {
    [Fact]
    public void RoverDetectsObstacleBeforeMove()
    {
      Mock<IObstacleService> svc = new Mock<IObstacleService>();
      svc.Setup(o => o.IsPointAnObstacle(0, 1)).Returns(true);

      Rover r = new Rover(svc.Object);

      (bool canMove, Obstacle ob) moveResult = r.ProcessSingleCommand('F');

      Assert.False(moveResult.canMove);
    }


    [Fact]
    public void RoverReturnsObstacleCoordsWhenCannotMove()
    {
      Mock<IObstacleService> svc = new Mock<IObstacleService>();
      svc.Setup(o => o.IsPointAnObstacle(0, 1)).Returns(true);
      Obstacle badObj = new Obstacle(0, 1);

      Rover r = new Rover(svc.Object);

      (bool canMove, Obstacle ob) moveResult = r.ProcessSingleCommand('F');

      Assert.False(moveResult.canMove);
      Assert.True(moveResult.ob.Equals(badObj));
    }

    [Fact]
    public void RoverReturnsObstacleCoordWhenInListOfCommands()
    {
      Mock<IObstacleService> svc = new Mock<IObstacleService>();
      svc.Setup(o => o.IsPointAnObstacle(1, 3)).Returns(true);
      Obstacle badObj = new Obstacle(1, 3);

      char[] commands = { 'F', 'F', 'F', 'R', 'F', 'F' };

      Rover r = new Rover(svc.Object);
      r.StoreCommands(commands);

      (bool obstacleFound, Obstacle ob) moveResult = r.ProcessAllCommands();

      Assert.True(moveResult.obstacleFound);
      Assert.True(moveResult.ob.Equals(badObj));

    }
  }
}
