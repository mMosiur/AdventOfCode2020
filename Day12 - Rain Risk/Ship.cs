using System;

namespace Day12
{
    class Ship : IShip
    {
        public Vector Position { get; private set; } = new(0, 0);

        private Vector direction = new(1, 0);

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
                    direction.RotateLeft();
                }
                else throw new NotSupportedException();
            }
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
                    direction.RotateRight();
                }
                else throw new NotSupportedException();
            }
        }

        public void TurnBack()
        {
            direction.Flip();
        }

        public void GoForward(int distance)
        {
            Position += (direction * distance);
        }

        public void GoNorth(int distance)
        {
            Position += Vector.North * distance;
        }

        public void GoSouth(int distance)
        {
            Position += Vector.South * distance;
        }

        public void GoEast(int distance)
        {
            Position += Vector.East * distance;
        }

        public void GoWest(int distance)
        {
            Position += Vector.West * distance;
        }

    }
}