using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Threading;
using SnakeWPF.Common;

namespace SnakeWPF.Model
{
    /// <summary>
    /// The Snake class represents the snake.
    /// </summary>
    public class Snake : NotificationBase
    {
        #region Fields

        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private ObservableCollection<SnakeBodyPart> _snakeBody;
        private volatile bool _updatingSnake;
        private static object _itemsLock = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Snake(double gameBoardWidthPixels, double gameBoardHeightPixels)
        {
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;
            TheSnakeHead = new SnakeHead(gameBoardWidthPixels, gameBoardHeightPixels, Constants.DefaultXposition, Constants.DefaultYposition, Constants.DefaultDirection);
            TheSnakeEye = new SnakeEye(gameBoardWidthPixels, gameBoardHeightPixels, Constants.DefaultXposition, Constants.DefaultYposition, Constants.DefaultDirection);
            _snakeBody = new ObservableCollection<SnakeBodyPart>();
            BindingOperations.EnableCollectionSynchronization(_snakeBody, _itemsLock);
            _updatingSnake = false;
        }

        #endregion
        
        #region Events

        public static event HitBoundary OnHitBoundary;
        public static event HitSnake OnHitSnake;
        public static event EatCherry OnEatCherry;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the game board width in pixels.
        /// </summary>
        public double GameBoardWidthPixels
        {
            get
            {
                return _gameBoardWidthPixels;
            }
            set
            {
                _gameBoardWidthPixels = value;
                RaisePropertyChanged();

                // Update the snake.
                TheSnakeHead.GameBoardWidthPixels = value;
                TheSnakeEye.GameBoardWidthPixels = value;
                foreach (SnakeBodyPart bodyPart in _snakeBody)
                {
                    bodyPart.GameBoardWidthPixels = value;
                }
            }
        }

        /// <summary>
        /// Gets the game board height in pixels.
        /// </summary>
        public double GameBoardHeightPixels
        {
            get
            {
                return _gameBoardHeightPixels;
            }
            set
            {
                _gameBoardHeightPixels = value;
                RaisePropertyChanged();

                // Update the snake.
                TheSnakeHead.GameBoardHeightPixels = value;
                TheSnakeEye.GameBoardHeightPixels = value;
                foreach (SnakeBodyPart bodyPart in _snakeBody)
                {
                    bodyPart.GameBoardHeightPixels = value;
                }
            }
        }

        /// <summary>
        /// Gets the snake's head.
        /// </summary>
        public SnakeHead TheSnakeHead { get; }

        /// <summary>
        /// Gets the snake's eye.
        /// </summary>
        public SnakeEye TheSnakeEye { get; }

        /// <summary>
        /// Gets the snake's body.
        /// </summary>
        public ObservableCollection<SnakeBodyPart> TheSnakeBody
        {
            get
            {
                if (_snakeBody == null)
                {
                    _snakeBody = new ObservableCollection<SnakeBodyPart>();
                }

                return _snakeBody;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The UpdateSnakeStatus method is called to update the status of the snake.
        /// The snake's position is updated.
        /// Events are raised if the snake has hit a boundary, hit itself, or eaten the cherry.
        /// </summary>
        public void UpdateSnakeStatus(Cherry theCherry)
        {
            while (_updatingSnake)
            {
                Thread.Sleep(50);
            }

            _updatingSnake = true;

            TheSnakeHead.UpdatePosition();    // Update the position of the snake's head.
            TheSnakeEye.UpdatePosition();     // Update the position of the snake's eye.

            // Update the position of the snake's body.
            Direction previousDirection;
            Direction nextDirection = TheSnakeHead.DirectionOfTravel;
            foreach (SnakeBodyPart bodyPart in _snakeBody)
            {
                bodyPart.UpdatePosition();
                previousDirection = bodyPart.DirectionOfTravel;
                bodyPart.DirectionOfTravel = nextDirection;
                nextDirection = previousDirection;
            }
            

            // Check if the snake has hit a boundary.
            if (TheSnakeHead.HitBoundary())
            {
                // The snake has hit the boundary - raise an OnHitBoundary event.
                OnHitBoundary?.Invoke();
            }

            // Check if the snake has hit itself.
            if (TheSnakeHead.HitSelf(_snakeBody))
            {
                // The snake has hit itself - raise an OnHitSnake event.
                OnHitSnake?.Invoke();
            }

            // Check if the snake can eat the cherry.
            if (TheSnakeHead.EatCherry(theCherry))
            {
                // The snake has eaten the cherry - increase the length of the snake.
                SnakeBodyPart snakeBodyPart = new SnakeBodyPart(_gameBoardWidthPixels, _gameBoardHeightPixels, this);
                _snakeBody.Add(snakeBodyPart);

                // The snake has eaten the cherry - raise an OnEatCherry event.
                OnEatCherry?.Invoke();
            }

            _updatingSnake = false;
        }

        /// <summary>
        /// The SetSnakeDirection method is called to set the snake's direction of travel.
        /// </summary>
        /// <param name="direction"></param>
        public void SetSnakeDirection(Direction direction)
        {
            while (_updatingSnake)
            {
                Thread.Sleep(50);
            }

            _updatingSnake = true;
            TheSnakeHead.DirectionOfTravel = direction;
            TheSnakeEye.DirectionOfTravel = direction;
            _updatingSnake = false;
        }

        #endregion
    }
}
