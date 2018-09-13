using System;
using System.Windows.Input;

namespace SnakeWPF.Common
{
    /// <summary>
    /// The DelegateCommand class represents an implementation of the ICommand interface.
    /// It is used by classes that need to execute commands.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        #region Fields

        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// Creates a new DelegateCommand with the provided action.
        /// </summary>
        /// <param name="execute"></param>
        public DelegateCommand(Action<object> execute)
        {
            _execute = execute;
            _canExecute = (x) => { return true; };
        }

        /// <summary>
        /// Constructor.
        /// Creates a new DelegateCommand with the provided action and condition.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// The CanExecute method is called to inform if this command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        /// <summary>
        /// The RaiseCanExecuteChanged method is called to assess if this command can be executed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// The Execute command is called to execute this command.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
