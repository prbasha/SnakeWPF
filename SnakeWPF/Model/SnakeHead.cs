using System;
using System.Collections.ObjectModel;

namespace SnakeWPF.Model
{
    /// <summary>
    /// The SnakeHead class represents the snake's head.
    /// </summary>
    public class SnakeHead : SnakePart
    {
        #region Fields
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public SnakeHead(double gameBoardWidthPixels, double gameBoardHeightPixels, double initialXPosition, double initialYPosition, Direction direction)
            : base(gameBoardWidthPixels, gameBoardHeightPixels, direction)
        {
            _xPosition = initialXPosition;
            _yPosition = initialYPosition;
            _width = Constants.HeadWidth;
            _height = Constants.HeadHeight;
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// The HitBoundary method is called to determine if the snake has hit the boundary.
        /// This is determined relative to the centre of the snake's head.
        /// Returns true if the snake has hit the boundary.
        /// Returns false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool HitBoundary()
        {
            if (_xPosition - (_width / 2.0) < 0)
            {
                return true;
            }
            else if (_xPosition + (_width / 2.0) > Constants.GameBoardWidthScale)
            {
                return true;
            }
            else if (_yPosition - (_height / 2.0) < 0)
            {
                return true;
            }
            else if (_yPosition + (_height / 2.0) > Constants.GameBoardHeightScale)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The HitSelf method is called to determine if the snake has hit itself.
        /// Returns true if the snake has itself.
        /// Returns false otherwise.
        /// </summary>
        /// <returns></returns>
        public bool HitSelf(ObservableCollection<SnakeBodyPart> snakeBody)
        {
            foreach (SnakeBodyPart bodyPart in snakeBody)
            {
                if (_xPosition == bodyPart.XPosition && _yPosition == bodyPart.YPosition)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The EatCherry method is called to determine if the snake can eat the cherry.
        /// Returns true if the cherry is eaten.
        /// Returns false otherwise.
        /// </summary>
        /// <param name="cherry"></param>
        /// <returns></returns>
        public bool EatCherry(Cherry cherry)
        {
            double xDiff = Math.Abs(_xPosition - cherry.XPosition);
            double yDiff = Math.Abs(_yPosition - cherry.YPosition);

            if (xDiff < _width && yDiff < _height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
