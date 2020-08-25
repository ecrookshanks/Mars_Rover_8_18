using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover_8_18
{
  public interface IObstacleService
  {
    bool IsPointAnObstacle(int x, int y);
  }
}
