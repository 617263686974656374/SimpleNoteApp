using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CsvHelper;
using System.Windows.Input;
using Skuska.Commands;
using Skuska.Models;
namespace Skuska.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Sammlung der Aufgabenbenutzer.
        /// </summary>
        public ObservableCollection<TaskUser> TasksUsers { get; set; }

        private string _newPriority;
        private string _newDescription;
        private string _newTaskName;
        private TaskUser _selectedTask;

        /// <summary>
        /// Ruft die ausgewählte Aufgabe ab oder legt diese fest.
        /// </summary>
        public TaskUser SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
                ((RelayCommand)UpdateTaskCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteTaskCommand).RaiseCanExecuteChanged();

                if (_selectedTask != null)
                {
                    NewTaskName = _selectedTask.TaskName;
                    NewDescription = _selectedTask.Description;
                    NewPriority = _selectedTask.Priority;
                }
            }
        }

        /// <summary>
        /// Ruft den neuen Aufgabennamen ab oder legt diesen fest.
        /// </summary>
        public string NewTaskName
        {
            get { return _newTaskName; }
            set
            {
                _newTaskName = value;
                OnPropertyChanged();
                ((RelayCommand)AddTaskCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateTaskCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Ruft die neue Beschreibung ab oder legt diese fest.
        /// </summary>
        public string NewDescription
        {
            get { return _newDescription; }
            set
            {
                _newDescription = value;
                OnPropertyChanged();
                ((RelayCommand)AddTaskCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateTaskCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Ruft die neue Priorität ab oder legt diese fest.
        /// </summary>
        public string NewPriority
        {
            get { return _newPriority; }
            set
            {
                _newPriority = value;
                OnPropertyChanged();
                ((RelayCommand)AddTaskCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateTaskCommand).RaiseCanExecuteChanged();
            }
        }

        // Befehle für die Benutzeroberfläche
        public ICommand LoadDataCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand UpdateTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand SaveDataCommand { get; set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der TaskViewModel-Klasse.
        /// </summary>
        public TaskViewModel()
        {
            TasksUsers = new ObservableCollection<TaskUser>();
            LoadDataCommand = new RelayCommand(LoadData);
            AddTaskCommand = new RelayCommand(AddTask, CanAddOrUpdateTask);
            UpdateTaskCommand = new RelayCommand(UpdateTask, CanAddOrUpdateTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            SaveDataCommand = new RelayCommand(SaveData, CanSaveData);
        }

        /// <summary>
        /// Speichert die Daten in eine CSV-Datei.
        /// </summary>
        private void SaveData()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(TasksUsers);
                }
            }
        }

        /// <summary>
        /// Überprüft, ob Daten gespeichert werden können.
        /// </summary>
        /// <returns>True, wenn Daten gespeichert werden können; andernfalls false.</returns>
        private bool CanSaveData()
        {
            return TasksUsers != null && TasksUsers.Any();
        }

        /// <summary>
        /// Lädt die Daten aus einer CSV-Datei.
        /// </summary>
        private void LoadData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                using (var reader = new StreamReader(openFileDialog.FileName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<TaskUser>().ToList();
                    TasksUsers.Clear();
                    foreach (var record in records)
                    {
                        TasksUsers.Add(record);
                    }
                }
                ((RelayCommand)SaveDataCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Fügt eine neue Aufgabe hinzu.
        /// </summary>
        private void AddTask()
        {
            TasksUsers.Add(new TaskUser { TaskName = NewTaskName, Description = NewDescription, Priority = NewPriority });// Neue Aufgabe mit den neuen Eigenschaften erstellen
            ClearNewTaskFields();// Textfelder nach dem Hinzufügen der Aufgabe leeren
            ((RelayCommand)SaveDataCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Überprüft, ob eine Aufgabe hinzugefügt oder aktualisiert werden kann.
        /// </summary>
        /// <returns>True, wenn die Aufgabe hinzugefügt oder aktualisiert werden kann; andernfalls false.</returns>
        private bool CanAddOrUpdateTask()
        {
            return !string.IsNullOrWhiteSpace(NewTaskName) &&
                   !string.IsNullOrWhiteSpace(NewDescription) &&
                   !string.IsNullOrWhiteSpace(NewPriority);
        }

        /// <summary>
        /// Aktualisiert die ausgewählte Aufgabe.
        /// </summary>
        private void UpdateTask()
        {
            if (SelectedTask != null)
            {
                SelectedTask.TaskName = NewTaskName;
                SelectedTask.Description = NewDescription;
                SelectedTask.Priority = NewPriority;
                OnPropertyChanged(nameof(TasksUsers));
                SelectedTask = null;
                ClearNewTaskFields();
                ((RelayCommand)SaveDataCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Leert die Textfelder für neue Aufgaben.
        /// </summary>
        private void ClearNewTaskFields()
        {
            NewTaskName = string.Empty;
            NewDescription = string.Empty;
            NewPriority = null;
            OnPropertyChanged(nameof(NewPriority));
            ((RelayCommand)AddTaskCommand).RaiseCanExecuteChanged();
            ((RelayCommand)UpdateTaskCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Löscht die ausgewählte Aufgabe.
        /// </summary>
        private void DeleteTask()
        {
            if (SelectedTask != null)
            {
                TasksUsers.Remove(SelectedTask);
                ClearNewTaskFields();
                ((RelayCommand)SaveDataCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Überprüft, ob eine Aufgabe gelöscht werden kann.
        /// </summary>
        /// <returns>True, wenn die Aufgabe gelöscht werden kann; andernfalls false.</returns>
        private bool CanDeleteTask()
        {
            return SelectedTask != null;
        }

        /// <summary>
        /// Tritt auf, wenn sich eine Eigenschaft ändert.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Benachrichtigt Listener, dass eine Eigenschaft geändert wurde.
        /// </summary>
        /// <param name="name">Name der geänderten Eigenschaft.</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));// Löst das PropertyChanged-Ereignis aus
        }
    }
}
