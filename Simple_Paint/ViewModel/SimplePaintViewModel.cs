using System.ComponentModel;
using System.Runtime.CompilerServices;
using Simple_Paint.Annotations;

namespace Simple_Paint.ViewModel
{
    public class SimplePaintViewModel : INotifyPropertyChanged
    {
        
        
        
        
        
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}