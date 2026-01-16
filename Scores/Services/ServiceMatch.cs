using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scores.Models;

namespace Scores.Services
{
    public static class ServiceMatch
    {
        public static List<Match> ObtenirTousLesMatchs()
        {
            return ServiceBD.ConnexionBD.Table<Match>().ToList();
        }
        public static Match ObtenirMatchParId(int id)
        {
            return ServiceBD.ConnexionBD.Table<Match>().FirstOrDefault(m => m.Id == id);
        }
        public static void AjouterMatch(Match match)
        {
            ServiceBD.ConnexionBD.Insert(match);
        }
        public static void MettreAJourMatch(Match match)
        {
            ServiceBD.ConnexionBD.Update(match);
        }
        public static void SupprimerMatch(int id)
        {
            var match = ObtenirMatchParId(id);
            if (match != null)
            {
                ServiceBD.ConnexionBD.Delete(match);
            }
        }
    }
}
