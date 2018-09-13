using System.ComponentModel;

namespace SnakeWPF.Common
{
    /// <summary>
    /// The NotificationBase class represents an implementation of the INotifyPropertyChanged interface.
    /// It is used by classes that need to raise a property changed event.
    /// </summary>
    public class NotificationBase : INotifyPropertyChanged
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// The RaisePropertyChanged method is called raise a property changed event. 
        /// </summary>
        /// <param name="property"></param>
        protected void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
