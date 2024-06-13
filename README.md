# SimpleNoteApp
SimpleNoteApp is a WPF application designed to manage tasks with varying priorities. It features a multilingual interface that supports English and German, and allows users to load, add, update, delete, and save tasks. The application is built with a clean MVVM architecture, utilizing `INotifyPropertyChanged` for data binding, `ObservableCollection` for task management, and `RelayCommand` for handling commands.

## Project Structure

### Namespaces and Classes

- **`Skuska.ViewModels`**
  - **`MainViewModel`**: The main ViewModel that coordinates the `TaskViewModel` and `LanguageViewModel`.
  - **`TaskViewModel`**: Manages the list of tasks, handles task-related operations, and implements command bindings.
  - **`LanguageViewModel`**: Manages the application language and the language toggle functionality.
  
- **`Skuska.Models`**
  - **`TaskUser`**: Represents a task with properties such as `TaskName`, `Description`, and `Priority`.

- **`Skuska.Commands`**
  - **`RelayCommand`**: A command implementation that relays its functionality by invoking delegates.

- **`Skuska.Views`**
  - **`MainWindow.xaml`**: The main window that defines the UI of the application.

## Features

### Task Management

- **Load Tasks**: Load tasks from a CSV file.
- **Add Task**: Add a new task with name, description, and priority.
- **Update Task**: Update the selected task's details.
- **Delete Task**: Remove the selected task from the list.
- **Save Tasks**: Save the current list of tasks to a CSV file.

### Language Toggle

- Switch between English and German using the language toggle button.

## User Interface

### Main Window

- **Task List**: Displays tasks with color-coded priorities (Green for Low, Gold for Medium, Red for High).
- **Task Details**: Editable fields for the selected task's name, description, and priority.
- **Control Buttons**: Buttons for loading, adding, updating, deleting, and saving tasks.
- **Language Toggle Button**: Button to switch the application language between English and German.

### Styles and Templates

- **RoundedButtonStyle**: Custom style for buttons with rounded corners and hover effects.
- **PriorityItemStyle**: Custom style for ListBox items based on task priority.
- **LanguageToggleButtonStyle**: Custom style for the language toggle button.

## Implementation Details

### `MainViewModel`

The `MainViewModel` initializes the `TaskViewModel` and `LanguageViewModel` and handles property change notifications.

### `TaskViewModel`

- **Properties**:
  - `TasksUsers`: Collection of tasks.
  - `SelectedTask`, `NewTaskName`, `NewDescription`, `NewPriority`: Properties for managing task details.
- **Commands**:
  - `LoadDataCommand`, `AddTaskCommand`, `UpdateTaskCommand`, `DeleteTaskCommand`, `SaveDataCommand`: Commands bound to respective methods.
- **Methods**:
  - `LoadData()`, `AddTask()`, `UpdateTask()`, `DeleteTask()`, `SaveData()`: Methods to manage task operations.
  - `CanAddOrUpdateTask()`, `CanDeleteTask()`, `CanSaveData()`: Validation methods for command execution.

### `LanguageViewModel`

- **Properties**:
  - `LanguageToggleButtonContent`: Content of the language toggle button.
- **Commands**:
  - `ToggleLanguageCommand`: Command bound to the `ToggleLanguage` method.
- **Methods**:
  - `ToggleLanguage()`: Toggles the application language between English and German.
  - `SetLanguageToggleButtonContent()`: Updates the toggle button content based on the current language.

### `TaskUser`

Implements `INotifyPropertyChanged` to notify the UI of property changes.

### `RelayCommand`

Implements `ICommand` to handle command logic with delegates and raise `CanExecuteChanged` events.

## Conclusion

SimpleNoteApp is a versatile and user-friendly task management application. With its MVVM architecture, it ensures a clean separation of concerns, making the application easy to maintain and extend. The multilingual support enhances usability for a diverse user base.
