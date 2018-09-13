using System;
using System.Linq;

namespace SnakeWPF.Model
{
    /// <summary>
    /// The SnakeBodyPart class represents one part of the snake's body.
    /// </summary>
    public class SnakeBodyPart : SnakePart
    {
        #region Fields
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public SnakeBodyPart(double gameBoardWidthPixels, double gameBoardHeightPixels, Snake theSnake)
        {
            // Set the gameboard width/height.
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;

            // Set the body width/height.
            _width = Constants.BodyWidth;
            _height = Constants.BodyHeight;

            // Get the current last SnakePart.
            SnakePart currentLastSnakePart;
            try
            {
                currentLastSnakePart = theSnake.TheSnakeBody.Last();
            }
            catch
            {
                // The snake body is empty - use the snake head as the last body part.
                currentLastSnakePart = theSnake.TheSnakeHead;
            }

            // Attempt to find a valid location at the end of the snake.
            if (currentLastSnakePart.DirectionOfTravel == Direction.Up)
            {
                _xPosition = currentLastSnakePart.XPosition;
                _yPosition = currentLastSnakePart.YPosition + _height;
                _directionOfTravel = Direction.Up;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Right)
            {
                _xPosition = currentLastSnakePart.XPosition - _width;
                _yPosition = currentLastSnakePart.YPosition;
                _directionOfTravel = Direction.Right;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Down)
            {
                _xPosition = currentLastSnakePart.XPosition;
                _yPosition = currentLastSnakePart.YPosition - _height;
                _directionOfTravel = Direction.Down;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else if (currentLastSnakePart.DirectionOfTravel == Direction.Left)
            {
                _xPosition = currentLastSnakePart.XPosition + _width;
                _yPosition = currentLastSnakePart.YPosition;
                _directionOfTravel = Direction.Left;

                if (CheckLocation(theSnake))
                {
                    // Location is valid.
                    return;
                }
            }
            else
            {
                throw new Exception("SnakeBodyPart(double gameBoardWidthPixels, double gameBoardHeightPixels, Snake theSnake): Unable to find valid location to grow snake.");
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="snakeHead"></param>
        public SnakeBodyPart(SnakeHead snakeHead)
        {
            _gameBoardWidthPixels = snakeHead.GameBoardWidthPixels;
            _gameBoardHeightPixels = snakeHead.GameBoardHeightPixels;
            _width = snakeHead.Width;
            _height = snakeHead.Height;
            _directionOfTravel = snakeHead.DirectionOfTravel;

            if (snakeHead.DirectionOfTravel == Direction.Right)
            {
                _xPosition = snakeHead.XPosition - _width;
                _yPosition = snakeHead.YPosition;
            }
            else if (snakeHead.DirectionOfTravel == Direction.Left)
            {
                _xPosition = snakeHead.XPosition + _width;
                _yPosition = snakeHead.YPosition;
            }
            else if (snakeHead.DirectionOfTravel == Direction.Down)
            {
                _xPosition = snakeHead.XPosition;
                _yPosition = snakeHead.YPosition - _height;
            }
            else if (snakeHead.DirectionOfTravel == Direction.Up)
            {
                _xPosition = snakeHead.XPosition;
                _yPosition = snakeHead.YPosition + _height;
            }

            UpdatePosition();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="snakePart"></param>
        public SnakeBodyPart(SnakeBodyPart snakeBodyPart)
        {
            _gameBoardWidthPixels = snakeBodyPart.GameBoardWidthPixels;
            _gameBoardHeightPixels = snakeBodyPart.GameBoardHeightPixels;
            _xPosition = snakeBodyPart.XPosition;
            _yPosition = snakeBodyPart.YPosition;
            _width = snakeBodyPart.WidthPixels;
            _height = snakeBodyPart.HeightPixels;
            _directionOfTravel = snakeBodyPart.DirectionOfTravel;
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// The CheckLocation method is called to ensure that the location of the snake body part is valid.
        /// The snake body part must not be in the same location as any of the existing snake body parts.
        /// The snake body part must be within bounds of the game board.
        /// </summary>
        /// <param name="theSnake"></param>
        /// <returns></returns>
        private bool CheckLocation(Snake theSnake)
        {
            // Check that the location is not the same as the snake's head.
            if (_xPosition == theSnake.TheSnakeHead.XPosition && _yPosition == theSnake.TheSnakeHead.YPosition)
            {
                // Location is not the same as the snake's head - do nothing.
            }
            else
            {
                return false;
            }

            // Check that the location is not the same as the snake's body.
            foreach (SnakeBodyPart bodyPart in theSnake.TheSnakeBody)
            {
                if (_xPosition == bodyPart.XPosition && _yPosition == bodyPart.YPosition)
                {
                    // Location is not the same as the snake's body part - do nothing.
                }
                else
                {
                    return false;
                }
            }

            // Check that the location is not out of bounds.
            if (_xPosition - (_width / 2.0) < 0)
            {
                return false;
            }
            else if (_xPosition + (_width / 2.0) > Constants.GameBoardWidthScale)
            {
                return false;
            }
            else if (_yPosition - (_height / 2.0) < 0)
            {
                return false;
            }
            else if (_yPosition + (_height / 2.0) > Constants.GameBoardHeightScale)
            {
                return false;
            }

            return true;
        }

        

        #endregion
    }
}
