using System;

namespace Day12
{
    public class WaypointShip : IShip
    {
        public Vector Position { get; private set; }

        private Vector waypoint = Vector.East * 10 + Vector.North;

        public void GoEast(int value)
        {
            waypoint += Vector.East * value;
        }

        public void GoForward(int value)
        {
            Position += waypoint * value;
        }

        public void GoNorth(int value)
        {
            waypoint += Vector.North * value;
        }

        public void GoSouth(int value)
        {
            waypoint += Vector.South * value;
        }

        public void GoWest(int value)
        {
            waypoint += Vector.West * value;
        }

        public void TurnLeft(int angle)
        {
            if (angle > 180)
            {
                TurnRight(angle - 180);
            }
            else if (angle == 180)
            {
                TurnBack();
            }
            else
            {
                if (angle == 90)
                {
                    waypoint.RotateLeft();
                }
                else throw new NotSupportedException();
            }
        }

        public void TurnBack()
        {
            waypoint.Flip();
        }

        public void TurnRight(int angle)
        {
            if (angle > 180)
            {
                TurnLeft(angle - 180);
            }
            else if (angle == 180)
            {
                TurnBack();
            }
            else
            {
                if (angle == 90)
                {
                    waypoint.RotateRight();
                }
                else throw new NotSupportedException();
            }
        }
    }
}