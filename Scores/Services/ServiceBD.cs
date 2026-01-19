using SQLite;
using Scores.Models;

namespace Scores.Services
{
    public static class ServiceBD
    {
        public static SQLiteConnection ConnexionBD { get; set; }
        private const string NOM_BD = "Quidditch.db3";

        public static void ConfigurerBD()
        {
            string cheminBD = Path.Combine(FileSystem.AppDataDirectory, NOM_BD);

            ConnexionBD = new SQLiteConnection(cheminBD);

            // Vide la BD avant de recreer les tables et seed (utile pour le TP afin d'avoir toujours les memes données de test, a retirer en contexte réel évidemment)
            ConnexionBD.DropTable<Equipe>();
            ConnexionBD.DropTable<Match>();

            ConnexionBD.CreateTable<Equipe>();
            ConnexionBD.CreateTable<Match>();

            SeedDonnees();
        }

        // J'ai fais un seed pour le TP afin d'avoir facilement des données de test (j'ai jugé ca pertinent dans le cadre scolaire. Mais a retiré en contexte réel également)
        private static void SeedDonnees()
        {
            if (ConnexionBD.Table<Equipe>().Any())
                return;

            var equipes = new List<Equipe>
            {
                new Equipe { Nom = "Gryffondor", Description = "Courage et bravoure" },
                new Equipe { Nom = "Serpentard", Description = "Ruse et ambition" },
                new Equipe { Nom = "Serdaigle", Description = "Sagesse et intelligence" },
                new Equipe { Nom = "Poufsouffle", Description = "Loyauté et persévérance" },
                new Equipe { Nom = "Bulldozards de Ballycastle", Description = "Équipe irlandaise légendaire" },
                new Equipe { Nom = "Frelons de Wimbourne", Description = "Rapides et imprévisibles" },
                new Equipe { Nom = "Harpies de Holyhead", Description = "Équipe entièrement féminine, redoutable" },
                new Equipe { Nom = "Tornades de Tutshill", Description = "Style offensif agressif" },
                new Equipe { Nom = "Canons de Chudley", Description = "Fans passionnés malgré des résultats mitigés" },
                new Equipe { Nom = "Pies de Montrose", Description = "Club le plus titré de Grande-Bretagne" }
            };

            ConnexionBD.InsertAll(equipes);
        }
    }
}