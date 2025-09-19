using System.Windows;
using System.Windows.Controls;

namespace tpfred.Views
{
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
        }

        private void Sauvegarder_Click(object sender, RoutedEventArgs e)
        {
            // Ici tu pourrais sauvegarder dans un fichier config ou Properties.Settings
            string langue = ((LangueComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()) ?? "Français";
            bool restart = ChkRestart.IsChecked == true;

            MessageBox.Show($"Langue choisie : {langue}\nRedémarrer : {restart}",
                            "Configuration",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

            this.DialogResult = true;
            this.Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
