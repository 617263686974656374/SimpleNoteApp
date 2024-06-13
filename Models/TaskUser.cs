using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Skuska.Models
{
    /// <summary>
    /// Repräsentiert eine Aufgabe mit Name, Beschreibung und Priorität.
    /// </summary>
    public class TaskUser : INotifyPropertyChanged
    {
        private string _taskName;

        /// <summary>
        /// Ruft den Namen der Aufgabe ab oder legt diesen fest.
        /// </summary>
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }

        private string _description;

        /// <summary>
        /// Ruft die Beschreibung der Aufgabe ab oder legt diese fest.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();// Benachrichtigt über die Eigenschaftsänderung
            }
        }

        private string _priority;

        /// <summary>
        /// Ruft die Priorität der Aufgabe ab oder legt diese fest.
        /// </summary>
        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Tritt auf, wenn sich eine Eigenschaft ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Benachrichtigt Listener, dass eine Eigenschaft geändert wurde.
        /// </summary>
        /// <param name="propertyName">Name der geänderten Eigenschaft.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));// Löst das PropertyChanged-Ereignis aus
        }
    }
}
