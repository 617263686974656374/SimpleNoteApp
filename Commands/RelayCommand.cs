using System;
using System.Windows.Input;

namespace Skuska.Commands
{
    /// <summary>
    /// Ein Befehl, der seine Funktionalität durch das Aufrufen von Delegaten weitergibt.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;// Aktion, die beim Ausführen des Befehls ausgeführt wird.
        private readonly Func<bool> _canExecute;// Funktion, die bestimmt, ob der Befehl ausgeführt werden kann.

        /// <summary>
        /// Tritt auf, wenn sich Änderungen ergeben, die beeinflussen, ob der Befehl ausgeführt werden soll.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="RelayCommand"/> Klasse.
        /// </summary>
        /// <param name="execute">Die Logik zur Ausführung.</param>
        /// <param name="canExecute">Die Logik zum Überprüfen des Ausführungsstatus.</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;// Weist die Ausführungslogik zu.
            _canExecute = canExecute;// Weist die Logik zum Überprüfen des Ausführungsstatus zu.
        }

        /// <summary>
        /// Definiert die Methode, die bestimmt, ob der Befehl im aktuellen Zustand ausgeführt werden kann.
        /// </summary>
        /// <param name="parameter">Daten, die vom Befehl verwendet werden. Wenn der Befehl keine Daten benötigt, kann dieses Objekt auf null gesetzt werden.</param>
        /// <returns>true, wenn dieser Befehl ausgeführt werden kann; andernfalls false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();// Überprüft, ob der Befehl ausgeführt werden kann.
        }

        /// <summary>
        /// Definiert die Methode, die aufgerufen wird, wenn der Befehl ausgeführt wird.
        /// </summary>
        /// <param name="parameter">Daten, die vom Befehl verwendet werden. Wenn der Befehl keine Daten benötigt, kann dieses Objekt auf null gesetzt werden.</param>
        public void Execute(object parameter)
        {
            _execute();// Führt die Ausführungslogik aus.
        }

        /// <summary>
        /// Methode zum Auslösen des CanExecuteChanged-Ereignisses, um anzuzeigen, dass sich der Rückgabewert der CanExecute-Methode geändert hat.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);// Löst das CanExecuteChanged-Ereignis aus.
        }
    }
}
