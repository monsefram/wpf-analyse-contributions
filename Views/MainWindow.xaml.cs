using System.Windows;
using Microsoft.Win32;
using tpfred.ViewModels;

namespace tpfred.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm; // ✅ UN SEUL DataContext, ici
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                Title = "Choisir un fichier contributions.csv"
            };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    _vm.AjouterFichierCsv(dlg.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Fichier CSV non valide.",
                                    "Erreur",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }

        private void Effacer_Click(object sender, RoutedEventArgs e)
        {
            _vm.EffacerContributions();
        }


        private void Configuration_Click(object sender, RoutedEventArgs e)
        {
            var configWin = new ConfigurationWindow();
            configWin.Owner = this; // optionnel, garde la fenêtre devant
            configWin.ShowDialog(); // fenêtre modale
        }

    }
}
