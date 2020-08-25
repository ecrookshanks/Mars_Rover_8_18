using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover_8_18
{
  class ObstacleService : IObstacleService
  {
    public bool IsPointAnObstacle(int x, int y)
    {
      // default implementation - point not an obstacle.
      return false;
    }
  }
}
