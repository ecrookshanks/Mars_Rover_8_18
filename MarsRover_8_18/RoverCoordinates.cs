namespace MarsRover
{
  public class RoverCoordinates
  {
    public int xPos { get; set; }
    
    public int yPos { get; set; }

    public RoverCoordinates(int v1, int v2)
    {
      this.xPos = v1;
      this.yPos = v2;
    }

    public override bool Equals(object obj)
    {
      if (obj is RoverCoordinates rc)
      {
        return this.xPos == rc.xPos && this.yPos == rc.yPos;
      }

      return false;

    }

    public override int GetHashCode()
    {
      return xPos * 31 + yPos;  // prime number
    }
  }
}