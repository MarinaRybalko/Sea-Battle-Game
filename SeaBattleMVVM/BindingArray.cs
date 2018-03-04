
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace SeaBattleMVVM
{
    public class BindingArray : INotifyPropertyChanged
    {
        private readonly int[,] _data;

        public BindingArray()
        {
            _data = new int[10, 10];


        }

        private void Notify(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        }
        public int this[int c1, int c2]
        {
            get { return _data[c1, c2]; }
            set
            {
                _data[c1, c2] = value;
                Notify(Binding.IndexerName);
            }
        }
        public static implicit operator int[,](BindingArray a)
        {
            return a._data;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
