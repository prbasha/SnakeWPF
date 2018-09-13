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

        private double _gameBoardWidthPixels;
        private double _gameBoardHeightPixels;
        private DispatcherTimer _gameTimer;
        private int _gameStepMilliSeconds;
        private int _gameLevel;
        private DispatcherTimer _restartTimer;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SnakeGame()
        {
            // Initialise the game board.
            _gameBoardWidthPixels = Constants.DefaultGameBoardWidthPixels;
            _gameBoardHeightPixels = Constants.DefaultGameBoardHeightPixels;

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
        /// Gets the game board width in pixels.
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
                RaisePropertyChanged("GameBoardWidthPixels");

                TheSnake.GameBoardWidthPixels = value;
            }
        }

        /// <summary>
        /// Gets the game board height in pixels.
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
                RaisePropertyChanged("GameBoardHeightPixels");

                TheSnake.GameBoardHeightPixels = value;
            }
        }

        /// <summary>
        /// Gets the snake.
        /// </summary>
        public Snake TheSnake { get; private set; }

        /// <summary>
        /// Gets the cherry.
        /// </summary>
        public Cherry TheCherry { get; private set; }

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
        /// Gets the game over boolean flag.
        /// </summary>
        public bool IsGameOver { get; private set; }

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
        /// Gets the current restart countdown status.
        /// </summary>
        public int RestartCountdownSeconds { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// The StartNewGame method is called to start a new game.
        /// </summary>
        private void StartNewGame()
        {
            // Initialise the snake and cherry.
            TheSnake = new Snake(_gameBoardWidthPixels, _gameBoardHeightPixels);
            RaisePropertyChanged("TheSnake");
            TheCherry = new Cherry(_gameBoardWidthPixels, _gameBoardHeightPixels, TheSnake.TheSnakeHead.XPosition, TheSnake.TheSnakeHead.YPosition);
            RaisePropertyChanged("TheCherry");

            // Set the game over flag.
            IsGameOver = false;
            RaisePropertyChanged("IsGameOver");
            RaisePropertyChanged("IsGameRunning");

            // Reset the restart timer.
            RestartCountdownSeconds = Constants.RestartCountdownStartSeconds;
            RaisePropertyChanged("RestartCountdownSeconds");

            // Initialise the game timer.
            _gameLevel = Constants.StartLevel;
            RaisePropertyChanged("TitleText");
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
            RaisePropertyChanged("RestartCountdownSeconds");
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
            RaisePropertyChanged("IsGameOver");
            RaisePropertyChanged("IsGameRunning");
        }

        /// <summary>
        /// The HitSnakeEventHandler is called to process an OnHitSnake event.
        /// </summary>
        private void HitSnakeEventHandler()
        {
            IsGameOver = true;
            RaisePropertyChanged("IsGameOver");
            RaisePropertyChanged("IsGameRunning");
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
            RaisePropertyChanged("TitleText");
            if (_gameLevel < Constants.EndLevel)
            {
                _gameStepMilliSeconds = _gameStepMilliSeconds - Constants.DecreaseGameStepMilliSeconds;
                _gameTimer.Interval = TimeSpan.FromMilliseconds(_gameStepMilliSeconds);
            }
            else
            {
                // Maximum level reached - game is complete.
                IsGameOver = true;
                RaisePropertyChanged("IsGameOver");
                RaisePropertyChanged("IsGameRunning");
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
            RaisePropertyChanged("RestartCountdownSeconds");

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
