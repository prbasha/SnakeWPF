using SnakeWPF.Common;

namespace SnakeWPF.Model
{
    /// <summary>
    /// The GameBoardItem class represents a single item on the game board.
    /// It is an abstract class.
    /// </summary>
    public abstract class GameBoardItem : NotificationBase
    {
        #region Fields

        protected double _gameBoardWidthPixels;
        protected double _gameBoardHeightPixels;
        protected double _xPosition;
        protected double _yPosition;
        protected double _width;
        protected double _height;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameBoardItem()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameBoardItem(double gameBoardWidthPixels, double gameBoardHeightPixels)
        {
            _gameBoardWidthPixels = gameBoardWidthPixels;
            _gameBoardHeightPixels = gameBoardHeightPixels;
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
                return _gameBoardWidthPixels;
            }
            set
            {
                _gameBoardWidthPixels = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Width));
                RaisePropertyChanged(nameof(XPosition));
                RaisePropertyChanged(nameof(XPositionPixels));
                RaisePropertyChanged(nameof(XPositionPixelsScreen));
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
                RaisePropertyChanged(nameof(Height));
                RaisePropertyChanged(nameof(YPosition));
                RaisePropertyChanged(nameof(YPositionPixels));
                RaisePropertyChanged(nameof(YPositionPixelsScreen));
            }
        }

        /// <summary>
        /// Gets or sets the current x ordinate.
        /// </summary>
        public double XPosition
        {
            get
            {
                return _xPosition;
            }
            protected set
            {
                _xPosition = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(XPosition));
                RaisePropertyChanged(nameof(XPositionPixels));
                RaisePropertyChanged(nameof(XPositionPixelsScreen));
            }
        }

        /// <summary>
        /// Gets or sets the current y ordinate.
        /// </summary>
        public double YPosition
        {
            get
            {
                return _yPosition;
            }
            protected set
            {
                _yPosition = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(YPosition));
                RaisePropertyChanged(nameof(YPositionPixels));
                RaisePropertyChanged(nameof(YPositionPixelsScreen));
            }
        }

        /// <summary>
        /// Gets or sets the current x ordinate in pixels.
        /// </summary>
        public double XPositionPixels
        {
            get
            {
                return (_xPosition / Constants.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }

        /// <summary>
        /// Gets or sets the current y ordinate in pixels.
        /// </summary>
        public double YPositionPixels
        {
            get
            {
                return (_yPosition / Constants.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }

        /// <summary>
        /// Gets or sets the current x ordinate in pixels, shifted for correct rendering on a screen.
        /// </summary>
        public double XPositionPixelsScreen
        {
            get
            {
                return ((_xPosition - (_width / 2.0)) / Constants.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }

        /// <summary>
        /// Gets or sets the current y ordinate in pixels, shifted for correct rendering on a screen.
        /// </summary>
        public double YPositionPixelsScreen
        {
            get
            {
                return ((_yPosition - (_height / 2.0)) / Constants.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public double Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Gets the width in pixels.
        /// </summary>
        public double WidthPixels
        {
            get
            {
                return (_width / Constants.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }

        /// <summary>
        /// Gets the height in pixels.
        /// </summary>
        public double HeightPixels
        {
            get
            {
                return (_height / Constants.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }

        #endregion

        #region Methods
        #endregion
    }
}
