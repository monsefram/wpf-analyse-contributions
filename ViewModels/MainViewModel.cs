using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using tpfred.Models;

namespace tpfred.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private  AnalyseurContributions _analyseur = new AnalyseurContributions();

        private ObservableCollection<Contribution> _contributions = new();
        public ObservableCollection<Contribution> Contributions
        {
            get => _contributions;
            private set => SetProperty(ref _contributions, value);
        }

        private int _total;
        public int Total
        {
            get => _total;
            private set => SetProperty(ref _total, value);
        }

        // Commande pour activer/désactiver le filtre
        public RelayCommand ToggleIllegalesCommand { get; }

        private bool _filtrerIllegales;
        public bool FiltrerIllegales
        {
            get => _filtrerIllegales;
            set
            {
                if (SetProperty(ref _filtrerIllegales, value))
                {
                    RafraichirAffichage();
                }
            }
        }

        public RelayCommand ToggleFiltreCommand { get; }


        public MainViewModel()
        {
            ToggleIllegalesCommand = new RelayCommand(
                execute: _ => FiltrerIllegales = !FiltrerIllegales,
                canExecute: _ => Contributions.Any()
            );

            ToggleFiltreCommand = new RelayCommand(
    execute: _ =>
    {
        FiltrerIllegales = !FiltrerIllegales; // on inverse manuellement
        RafraichirAffichage();
    },
    canExecute: _ => Contributions.Count > 0
);


            // quand la liste change → on force CanExecute à se réévaluer
            Contributions.CollectionChanged += (s, e) =>
            {
                ToggleFiltreCommand.RaiseCanExecuteChanged();
            };

        }

        public void AjouterFichierCsv(string chemin)
        {
            _analyseur.AjouterContributions(chemin);
            Contributions = new ObservableCollection<Contribution>(_analyseur.GetContributions());
            Total = Contributions.Count;
            CommandManager.InvalidateRequerySuggested(); // force mise à jour CanExecute

            ToggleFiltreCommand.RaiseCanExecuteChanged();

        }

        public void EffacerContributions()
        {
            // remet l’analyseur à vide
            _analyseur = new AnalyseurContributions();

            // vide l’UI
            Contributions.Clear();
            Total = 0;
            FiltrerIllegales = false;

            ToggleFiltreCommand?.RaiseCanExecuteChanged();
        }


        private void RafraichirAffichage()
        {
            if (FiltrerIllegales)
                Contributions = new ObservableCollection<Contribution>(_analyseur.RechercherContributionsPossiblementIllegales());
            else
                Contributions = new ObservableCollection<Contribution>(_analyseur.GetContributions());

            Total = Contributions.Count;
        }




    }
}
