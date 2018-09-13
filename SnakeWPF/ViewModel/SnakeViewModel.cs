using SnakeWPF.Common;
using SnakeWPF.Model;
using System.Windows.Input;

namespace SnakeWPF.ViewModel
{
    /// <summary>
    /// The SnakeViewModel class represents the View Model for the Snake game.
    /// </summary>
    public class SnakeViewModel : NotificationBase
    {

        #region Fields
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SnakeViewModel()
        {
            SnakeGameLogic = new SnakeGame();

            UpKeyPressedCommand = new DelegateCommand(OnUpKeyPressed);
            RightKeyPressedCommand = new DelegateCommand(OnRightKeyPressed);
            DownKeyPressedCommand = new DelegateCommand(OnDownKeyPressed);
            LeftKeyPressedCommand = new DelegateCommand(OnLeftKeyPressed);
        }

        #endregion

        #region Events
        #endregion

        #region Properties

        /// <summary>
        /// Gets the snake game logic.
        /// </summary>
        public SnakeGame SnakeGameLogic { get; }

        /// <summary>
        /// Gets or sets the UP key pressed command.
        /// </summary>
        public ICommand UpKeyPressedCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the RIGHT key pressed command.
        /// </summary>
        public ICommand RightKeyPressedCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the DOWN key pressed command.
        /// </summary>
        public ICommand DownKeyPressedCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the LEFT key pressed command.
        /// </summary>
        public ICommand LeftKeyPressedCommand
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OnUpKeyPressed method is called when the UpKeyPressedCommand is triggered.
        /// It processes the keyboard UP key pressed event.
        /// </summary>
        /// <param name="arg"></param>
        private void OnUpKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Up);
            }
        }

        /// <summary>
        /// The OnRightKeyPressed method is called when the RightKeyPressedCommand is triggered.
        /// It processes the keyboard RIGHT key pressed event.
        /// </summary>
        /// <param name="arg"></param>
        private void OnRightKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Right);
            }
        }

        /// <summary>
        /// The OnDownKeyPressed method is called when the DownKeyPressedCommand is triggered.
        /// It processes the keyboard DOWN key pressed event.
        /// </summary>
        /// <param name="arg"></param>
        private void OnDownKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Down);
            }
        }

        /// <summary>
        /// The OnLeftKeyPressed method is called when the LeftKeyPressedCommand is triggered.
        /// It processes the keyboard LEFT key pressed event.
        /// </summary>
        /// <param name="arg"></param>
        private void OnLeftKeyPressed(object arg)
        {
            if (SnakeGameLogic.IsGameRunning)
            {
                SnakeGameLogic.ProcessKeyboardEvent(Direction.Left);
            }
        }

        #endregion
    }
}
