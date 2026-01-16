using Scores.Services;

namespace Scores
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServiceBD.ConfigurerBD();

            MainPage = new AppShell();
        }
    }
}
