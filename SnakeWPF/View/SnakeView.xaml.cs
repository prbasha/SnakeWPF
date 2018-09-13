using SnakeWPF.ViewModel;
using System.Windows;

namespace SnakeWPF.View
{
    /// <summary>
    /// The SnakeView class represents the View for the Snake game.
    /// </summary>
    public partial class SnakeView : Window
    {
        #region Fields
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SnakeView()
        {
            InitializeComponent();

            // Create the View Model.
            SnakeViewModel viewModel = new SnakeViewModel();
            DataContext = viewModel;    // Set the data context for all data binding operations.
        }

        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
