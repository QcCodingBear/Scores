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

            var eq = ConnexionBD.Table<Equipe>().ToList();

            var matchs = new List<Match>
            {
                new Match { EquipeDomicileId = eq[9].Id, EquipeExterieurId = eq[5].Id, DateMatch = DateTime.Now.AddDays(-20), ScoreDomicile = 210, ScoreExterieur = 80, VifDorAttrapeParEquipeId = eq[9].Id },
                new Match { EquipeDomicileId = eq[7].Id, EquipeExterieurId = eq[0].Id, DateMatch = DateTime.Now.AddDays(-19), ScoreDomicile = 150, ScoreExterieur = 170, VifDorAttrapeParEquipeId = eq[0].Id },
                new Match { EquipeDomicileId = eq[1].Id, EquipeExterieurId = eq[8].Id, DateMatch = DateTime.Now.AddDays(-18), ScoreDomicile = 120, ScoreExterieur = 130, VifDorAttrapeParEquipeId = eq[8].Id },
                new Match { EquipeDomicileId = eq[3].Id, EquipeExterieurId = eq[6].Id, DateMatch = DateTime.Now.AddDays(-17), ScoreDomicile = 90, ScoreExterieur = 100, VifDorAttrapeParEquipeId = eq[6].Id },
                new Match { EquipeDomicileId = eq[8].Id, EquipeExterieurId = eq[2].Id, DateMatch = DateTime.Now.AddDays(-16), ScoreDomicile = 60, ScoreExterieur = 200, VifDorAttrapeParEquipeId = eq[2].Id },
                new Match { EquipeDomicileId = eq[4].Id, EquipeExterieurId = eq[9].Id, DateMatch = DateTime.Now.AddDays(-15), ScoreDomicile = 180, ScoreExterieur = 170, VifDorAttrapeParEquipeId = eq[4].Id },
                new Match { EquipeDomicileId = eq[5].Id, EquipeExterieurId = eq[1].Id, DateMatch = DateTime.Now.AddDays(-14), ScoreDomicile = 40, ScoreExterieur = 190, VifDorAttrapeParEquipeId = eq[1].Id },
                new Match { EquipeDomicileId = eq[6].Id, EquipeExterieurId = eq[7].Id, DateMatch = DateTime.Now.AddDays(-13), ScoreDomicile = 200, ScoreExterieur = 30, VifDorAttrapeParEquipeId = eq[6].Id },
                new Match { EquipeDomicileId = eq[8].Id, EquipeExterieurId = eq[5].Id, DateMatch = DateTime.Now.AddDays(-12), ScoreDomicile = 150, ScoreExterieur = 140, VifDorAttrapeParEquipeId = eq[8].Id },
                new Match { EquipeDomicileId = eq[3].Id, EquipeExterieurId = eq[9].Id, DateMatch = DateTime.Now.AddDays(-11), ScoreDomicile = 100, ScoreExterieur = 90, VifDorAttrapeParEquipeId = eq[3].Id },
                new Match { EquipeDomicileId = eq[0].Id, EquipeExterieurId = eq[4].Id, DateMatch = DateTime.Now.AddDays(-10), ScoreDomicile = 220, ScoreExterieur = 180, VifDorAttrapeParEquipeId = eq[0].Id },
                new Match { EquipeDomicileId = eq[2].Id, EquipeExterieurId = eq[7].Id, DateMatch = DateTime.Now.AddDays(-9), ScoreDomicile = 190, ScoreExterieur = 210, VifDorAttrapeParEquipeId = eq[7].Id },
                new Match { EquipeDomicileId = eq[6].Id, EquipeExterieurId = eq[0].Id, DateMatch = DateTime.Now.AddDays(-5), ScoreDomicile = 130, ScoreExterieur = 160, VifDorAttrapeParEquipeId = eq[0].Id },
                new Match { EquipeDomicileId = eq[1].Id, EquipeExterieurId = eq[3].Id, DateMatch = DateTime.Now.AddDays(-3), ScoreDomicile = 200, ScoreExterieur = 100, VifDorAttrapeParEquipeId = eq[1].Id },
                new Match { EquipeDomicileId = eq[9].Id, EquipeExterieurId = eq[8].Id, DateMatch = DateTime.Now.AddDays(-1), ScoreDomicile = 170, ScoreExterieur = 160, VifDorAttrapeParEquipeId = eq[9].Id }
            };

            ConnexionBD.InsertAll(matchs);
        }
    }
}