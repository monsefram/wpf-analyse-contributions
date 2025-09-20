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
            string langue = ((LangueComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()) ?? "Français";
            string code = langue == "English" ? "en-US" : "fr";

            // Sauvegarde dans Settings
            tpfred.Properties.Settings.Default.langue = code;
            tpfred.Properties.Settings.Default.Save();

            if (ChkRestart.IsChecked == true)
            {
                MessageBox.Show("L'application va redémarrer pour appliquer les changements.",
                                        "Information",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information); System.Diagnostics.Process.Start(fileName: Environment.ProcessPath);
                // Si la ligne précédente de fonctionne pas, essayez celle-ci 
                // System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Les changements prendront effet au prochain démarrage.",
                                        "Information",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
            }
        }


        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
