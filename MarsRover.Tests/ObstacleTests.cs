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
  }
}
