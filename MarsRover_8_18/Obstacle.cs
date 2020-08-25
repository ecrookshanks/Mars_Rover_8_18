using System.Runtime.CompilerServices;

namespace MarsRover
{
  public class Obstacle
  {
    public int xPos { get; set; }
    public int yPos { get; set; }

    public Obstacle(int x, int y) 
    {
      xPos = x; 
      yPos = y;
    }

    public override bool Equals(object obj)
    {
      if (obj is Obstacle ob)
      {
        return (ob.xPos == this.xPos && ob.yPos == this.yPos);
      }
      return false ;
    }

  }
}