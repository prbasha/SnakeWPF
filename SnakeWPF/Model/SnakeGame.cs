using SnakeWPF.Common;
using System;
using System.Windows.Threading;

namespace SnakeWPF.Model
{
    /// <summary>
    /// The SnakeGame represents an implementation of the Snake game.
    /// </summary>
    public class SnakeGame : NotificationBase
    {
        #region Fields

        private Snake _theSnake;
        private Cherry _theCherry;
        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private DispatcherTimer _gameTimer;
        private int _gameStepMilliSeconds;
        private int _gameLevel;
        private bool _isGameOver;
        private int _restartCountdownSeconds;
        private DispatcherTimer _restartTimer;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SnakeGame()
        {
            // Initialise the game board.
            GameBoardWidthPixels = Constants.DefaultGameBoardWidthPixels;
            GameBoardHeightPixels = Constants.DefaultGameBoardHeightPixels;

            // Listen for events from the snake.
            Snake.OnHitBoundary += new HitBoundary(HitBoundaryEventHandler);
            Snake.OnHitSnake += new HitSnake(HitSnakeEventHandler);
            Snake.OnEatCherry += new EatCherry(EatCherryEventHandler);

            // Start a new game.
            StartNewGame();
        }

        #endregion

        #region Events
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the game board width in pixels.
        /// </summary>
        public double GameBoardWidthPixels
        {
            get
            {
                return (int)_gameBoardWidthPixels;
            }
            set
            {
                _gameBoardWidthPixels = value;
                RaisePropertyChanged();

                TheSnake.GameBoardWidthPixels = value;
            }
        }

        /// <summary>
        /// Gets or sets the game board height in pixels.
        /// </summary>
        public double GameBoardHeightPixels
        {
            get
            {
                return (int)_gameBoardHeightPixels;
            }
            set
            {
                _gameBoardHeightPixels = value;
                RaisePropertyChanged();

                TheSnake.GameBoardHeightPixels = value;
            }
        }

        /// <summary>
        /// Gets or sets the snake.
        /// </summary>
        public Snake TheSnake
        {
            get
            {
                if (_theSnake == null)
                {
                    _theSnake = new Snake(GameBoardWidthPixels, GameBoardHeightPixels);
                }

                return _theSnake;
            }
            private set
            {
                _theSnake = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the cherry.
        /// </summary>
        public Cherry TheCherry
        {
            get
            {
                if (_theCherry == null)
                {
                    _theCherry = new Cherry(_gameBoardWidthPixels, _gameBoardHeightPixels, TheSnake.TheSnakeHead.XPosition, TheSnake.TheSnakeHead.YPosition);
                }

                return _theCherry;
            }
            private set
            {
                _theCherry = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the title text.
        /// </summary>
        public string TitleText
        {
            get
            {
                return "Snake " + _gameLevel + "/" + Constants.EndLevel;
            }
        }

        /// <summary>
        /// Gets or sets the game over boolean flag.
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return _isGameOver;
            }
            private set
            {
                _isGameOver = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsGameRunning));
            }
        }

        /// <summary>
        /// Gets the game running boolean flag.
        /// </summary>
        public bool IsGameRunning
        {
            get
            {
                return !IsGameOver;
            }
        }

        /// <summary>
        /// Gets or sets the current restart countdown status.
        /// </summary>
        public int RestartCountdownSeconds
        {
            get
            {
                return _restartCountdownSeconds;
            }
            private set
            {
                _restartCountdownSeconds = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The StartNewGame method is called to start a new game.
        /// </summary>
        private void StartNewGame()
        {
            // Initialise the snake and cherry.
            TheSnake = new Snake(_gameBoardWidthPixels, _gameBoardHeightPixels);
            TheCherry = new Cherry(_gameBoardWidthPixels, _gameBoardHeightPixels, TheSnake.TheSnakeHead.XPosition, TheSnake.TheSnakeHead.YPosition);

            // Set the game over flag.
            IsGameOver = false;

            // Reset the restart timer.
            RestartCountdownSeconds = Constants.RestartCountdownStartSeconds;

            // Initialise the game timer.
            _gameLevel = Constants.StartLevel;
            RaisePropertyChanged(nameof(TitleText));
            _gameStepMilliSeconds = Constants.DefaultGameStepMilliSeconds;
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            _gameTimer.Tick += new EventHandler(GameTimerEventHandler);
            _gameTimer.Start();
        }

        /// <summary>
        /// The RestartGame method is called to restart the game.
        /// </summary>
        private void RestartGame()
        {
            // Initialise the restart countdown.
            RestartCountdownSeconds = Constants.RestartCountdownStartSeconds;
            _restartTimer = new DispatcherTimer();
            _restartTimer.Interval = TimeSpan.FromMilliseconds(Constants.RestartStepMilliSeconds);
            _restartTimer.Tick += new EventHandler(RestartTimerEventHandler);
            _restartTimer.Start();
        }

        /// <summary>
        /// The HitBoundaryEventHandler is called to process an OnHitBoundary event.
        /// </summary>
        private void HitBoundaryEventHandler()
        {
            IsGameOver = true;
        }

        /// <summary>
        /// The HitSnakeEventHandler is called to process an OnHitSnake event.
        /// </summary>
        private void HitSnakeEventHandler()
        {
            IsGameOver = true;
        }

        /// <summary>
        /// The EatCherryEventHandler is called to process an OnEatCherry event.
        /// </summary>
        private void EatCherryEventHandler()
        {
            // Move the cherry to a new location, away from the snake.
            TheCherry.MoveCherry(TheSnake);

            // Increase the game level and speed.
            _gameLevel++;
            RaisePropertyChanged(nameof(TitleText));
            if (_gameLevel < Constants.EndLevel)
            {
                _gameStepMilliSeconds = _gameStepMilliSeconds - Constants.DecreaseGameStepMilliSeconds;
                _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            }
            else
            {
                // Maximum level reached - game is complete.
                IsGameOver = true;
            }
        }

        /// <summary>
        /// The GameTimerEventHandler method is called to update the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTimerEventHandler(object sender, EventArgs e)
        {
            if (IsGameOver)
            {
                // Game over.
                if (_gameTimer.IsEnabled)
                {
                    _gameTimer.Stop();  // Stop the game timer.
                    RestartGame();      // Restart the game.
                }
            }
            else
            {
                // Game running.
                TheSnake.UpdateSnakeStatus(TheCherry);
            }
        }

        /// <summary>
        /// The RestartTimerEventHandler method is called to update the restart sequence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestartTimerEventHandler(object sender, EventArgs e)
        {
            RestartCountdownSeconds--;

            if (RestartCountdownSeconds == 0)
            {
                _restartTimer.Stop();   // Stop the restart timer.
                StartNewGame();         // Start a new game.
            }
        }

        /// <summary>
        /// The ProcessKeyboardEvent method is called to process a keyboard event.
        /// </summary>
        /// <param name="direction"></param>
        public void ProcessKeyboardEvent(Direction direction)
        {
            TheSnake.SetSnakeDirection(direction);
        }

        #endregion
    }
}
