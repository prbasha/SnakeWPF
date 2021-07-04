using System;

namespace SnakeWPF.Model
{
    /// <summary>
    /// The Cherry class represents a cherry (for the snake to eat).
    /// </summary>
    public class Cherry : GameBoardItem
    {
        #region Fields

        private Random _randomNumber;

        // Constants.
        

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cherry(double gameBoardWidthPixels, double gameBoardHeightPixels, double snakeXPosition, double snakeYPosition)
            : base(gameBoardWidthPixels, gameBoardHeightPixels)
        {
            _randomNumber = new Random((int)DateTime.Now.Ticks);
            _xPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
            _yPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
            _width = Constants.CherryWidth;
            _height = Constants.CherryHeight;
        }

        #endregion
        
        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// The MoveCherry method is called to move the cherry, based on the current position of the snake.
        /// </summary>
        /// <param name="theSnake"></param>
        public void MoveCherry(Snake theSnake)
        {
            bool cherryMoved = false;
            double xDiff;
            double yDiff;

            while (!cherryMoved)
            {
                // Attempt to relocate the cherry - somewhere on the game board.
                XPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
                YPosition = _randomNumber.Next(Constants.MinimumPosition, Constants.MaximumPosition);
                // Ensure the location of the cherry is a reasonable distance from the snake's head.
                xDiff = Math.Abs(_xPosition - theSnake.TheSnakeHead.XPosition);
                yDiff = Math.Abs(_yPosition - theSnake.TheSnakeHead.YPosition);
                if (xDiff > Constants.PlacementBuffer * _width || yDiff > Constants.PlacementBuffer * _height)
                {
                    // Ensure the location of the cherry is a reasonable distance from the snake's body parts.
                    foreach (SnakeBodyPart bodyPart in theSnake.TheSnakeBody)
                    {
                        xDiff = Math.Abs(_xPosition - bodyPart.XPosition);
                        yDiff = Math.Abs(_yPosition - bodyPart.YPosition);
                        if (xDiff > Constants.PlacementBuffer * _width || yDiff > Constants.PlacementBuffer * _height)
                        {
                            cherryMoved = true;
                        }
                        else
                        {
                            cherryMoved = false;
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
