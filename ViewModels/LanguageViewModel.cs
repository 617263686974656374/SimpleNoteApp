using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Skuska.Commands;

namespace Skuska.ViewModels
{
    public class LanguageViewModel : INotifyPropertyChanged
    {
        private string _languageToggleButtonContent;

        /// <summary>
        /// Ruft den Inhalt des Sprachumschalters ab oder legt diesen fest.
        /// </summary>
        public string LanguageToggleButtonContent
        {
            get { return _languageToggleButtonContent; }
            set
            {
                _languageToggleButtonContent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Befehl zum Umschalten der Sprache.
        /// </summary>
        public ICommand ToggleLanguageCommand { get; set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="LanguageViewModel"/> Klasse.
        /// </summary>
        public LanguageViewModel()
        {
            ToggleLanguageCommand = new RelayCommand(ToggleLanguage);
            SetLanguageToggleButtonContent();
        }

        /// <summary>
        /// Schaltet die aktuelle Sprache der Anwendung um.
        /// </summary>
        private void ToggleLanguage()
        {
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            var newCulture = currentCulture == "en-US" ? "de-DE" : "en-US";

            ResourceDictionary newResource = new ResourceDictionary();
            string resourcePath = newCulture == "de-DE" ? "Resources/Strings.de.xaml" : "Resources/Strings.en.xaml";

            try
            {
                newResource.Source = new Uri(resourcePath, UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(newResource);

                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(newCulture);
                CultureInfo.CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;

                SetLanguageToggleButtonContent();

                OnPropertyChanged(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load resource: {resourcePath}\nException: {ex.Message}");
            }
        }

        /// <summary>
        /// Setzt den Inhalt des Sprachumschalters.
        /// </summary
        private void SetLanguageToggleButtonContent()
        {
            LanguageToggleButtonContent = CultureInfo.CurrentUICulture.Name == "en-US" ? "DE" : "EN";
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
