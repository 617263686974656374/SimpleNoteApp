using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Skuska.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public TaskViewModel TaskViewModel { get; set; }
        public LanguageViewModel LanguageViewModel { get; set; }

        public MainViewModel()
        {
            TaskViewModel = new TaskViewModel();
            LanguageViewModel = new LanguageViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
