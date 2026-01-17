using Scores.Services;
using Scores.Pages;

namespace Scores
{
    public partial class App : Application
    {
        public App()
        {
            Application.Current.UserAppTheme = AppTheme.Light;

            InitializeComponent();

            ServiceBD.ConfigurerBD();

            MainPage = new AppShell();
        }
    }
}
