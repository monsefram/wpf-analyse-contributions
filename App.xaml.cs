using System.Globalization;
using System.Threading;
using System.Windows;

namespace tpfred
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Lire la langue sauvegardée
            string langue = tpfred.Properties.Settings.Default.langue;

            // Si aucune valeur, on met fr par défaut
            if (string.IsNullOrWhiteSpace(langue))
                langue = "fr";

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(langue);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(langue);
        }
    }
}
