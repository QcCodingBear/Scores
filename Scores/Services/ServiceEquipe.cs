using Scores.Models;

namespace Scores.Services
{
    public static class ServiceEquipe
    {
        public static List<Equipe> ObtenirToutesLesEquipes()
        {
            return ServiceBD.ConnexionBD.Table<Equipe>().ToList();
        }

        public static Equipe ObtenirEquipeParId(int id)
        {
            return ServiceBD.ConnexionBD.Table<Equipe>().FirstOrDefault(e => e.Id == id);
        }

        public static void AjouterEquipe(Equipe equipe)
        {
            ServiceBD.ConnexionBD.Insert(equipe);
        }

        public static void MettreAJourEquipe(Equipe equipe)
        {
            ServiceBD.ConnexionBD.Update(equipe);
        }

        public static void SupprimerEquipe(int id)
        {
            var equipe = ObtenirEquipeParId(id);
            if (equipe != null)
            {
                ServiceBD.ConnexionBD.Delete(equipe);
            }
        }


    }
}
