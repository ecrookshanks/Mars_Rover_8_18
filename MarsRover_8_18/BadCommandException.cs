using System;

namespace MarsRover
{
  public class BadCommandException : Exception
  {
    public BadCommandException(string message) : base(message)
    {
    }
  }
}