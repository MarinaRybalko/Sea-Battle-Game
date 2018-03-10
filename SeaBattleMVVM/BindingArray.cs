
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace SeaBattleMVVM
{
    public class BindingArray : INotifyPropertyChanged
    {
        private readonly int[,] _data;
        /// <summary>
        /// Initialize a new instance of the <see cref="BindingArray"/> class
        /// </summary>
        public BindingArray()
        {
            _data = new int[10, 10];


        }

        private void Notify(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        }
        /// <summary>
        /// Allow client code to use [,] notation
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>Array element by given index </returns>
        public int this[int c1, int c2]
        {
            get { return _data[c1, c2]; }
            set
            {
                _data[c1, c2] = value;
                Notify(Binding.IndexerName);
            }
        }
        /// <summary>
        /// Performs the necessary converting
        /// </summary>
        /// <param name="a"></param>
        public static implicit operator int[,](BindingArray a)
        {
            return a._data;
        }
        /// <summary>
        /// Occurs when a property is changed on a component
        /// </summary>       
        public event PropertyChangedEventHandler PropertyChanged;
      
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
