
namespace SnakeWPF.Model
{
    /// <summary>
    /// The SnakePart represents part of a snake.
    /// It is an abstract class.
    /// </summary>
    public abstract class SnakePart : GameBoardItem
    {
        #region Fields

        protected Direction _directionOfTravel;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SnakePart()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SnakePart(double gameBoardWidthPixels, double gameBoardHeightPixels, Direction direction)
            : base(gameBoardWidthPixels, gameBoardHeightPixels)
        {
            _directionOfTravel = direction;
        }

        #endregion

        #region Events
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current direction of travel.
        /// </summary>
        public Direction DirectionOfTravel
        {
            get
            {
                return _directionOfTravel;
            }
            set
            {
                _directionOfTravel = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(DirectionOfTravelDegrees));
            }
        }

        /// <summary>
        /// Gets the direction of travel in degrees.
        /// </summary>
        public double DirectionOfTravelDegrees
        {
            get
            {
                double direction = 0;

                if (_directionOfTravel == Direction.Up)
                {
                    direction = Constants.DirectionUpDegrees;
                }
                else if (_directionOfTravel == Direction.Right)
                {
                    direction = Constants.DirectionRightDegrees;
                }
                else if (_directionOfTravel == Direction.Down)
                {
                    direction = Constants.DirectionDownDegrees;
                }
                else if (_directionOfTravel == Direction.Left)
                {
                    direction = Constants.DirectionLeftDegrees;
                }

                return direction;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The UpdatePosition method is called to update the position of the snake part, based on the current direction of travel.
        /// </summary>
        public void UpdatePosition()
        {
            if (_directionOfTravel == Direction.Up)
            {
                YPosition = YPosition - Constants.StepSize;
            }
            else if (_directionOfTravel == Direction.Right)
            {
                XPosition = XPosition + Constants.StepSize;
            }
            else if (_directionOfTravel == Direction.Down)
            {
                YPosition = YPosition + Constants.StepSize;
            }
            else if (_directionOfTravel == Direction.Left)
            {
                XPosition = XPosition - Constants.StepSize;
            }
        }

        #endregion
    }
}
