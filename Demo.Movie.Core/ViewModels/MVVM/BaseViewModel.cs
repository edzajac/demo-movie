
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Demo.Movie.Core.ViewModels.MVVM
{
    public class BaseViewModel : INotifyPropertyChanged, IViewModel
    {
        protected bool _isLoading = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoading
        {
            get => _isLoading;
            set => RaiseAndUpdate(ref _isLoading, value);
        }

        public virtual Task InitAsync() => Task.FromResult(true);

        /// <summary>
        /// Raises the specified property and updates the inputted field with the new value.
        /// </summary>
        /// <returns><c>true</c>, if and update was raised, <c>false</c> otherwise.</returns>
        /// <param name="field">Field.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected bool RaiseAndUpdate<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;

            Raise(propertyName);

            return true;
        }

        /// <summary>
        /// Raises the specified property.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void Raise([CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
