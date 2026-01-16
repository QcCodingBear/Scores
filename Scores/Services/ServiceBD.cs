using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Scores.Models;

namespace Scores.Services
{
    public static class ServiceBD
    {
        public static SQLiteConnection ConnexionBD { get; set; }
        private const string NOM_BD = "ScoreSqlite.db3";

        public static void ConfigurerBD()
        {
            // A la racine du projet MAUI
            string cheminBD = Path.Combine(FileSystem.AppDataDirectory, NOM_BD);

            ConnexionBD = new SQLiteConnection(cheminBD);
            ConnexionBD.CreateTable<Equipe>();
            ConnexionBD.CreateTable<Match>();
        }

    }
}
