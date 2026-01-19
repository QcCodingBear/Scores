using Scores.Models;
using Scores.Services;

namespace Scores.Pages;

public partial class EquipePage : ContentPage
{

    private int IdEquipe { get; set; }

	public EquipePage(Equipe? equipe = null)
	{
		InitializeComponent();

		if (equipe != null) PageEnModeUpdate(equipe);
    }

    // Si la page doit afficher une équipe à mettre à jour on récupére ses informations.
    public void PageEnModeUpdate(Equipe equipe) 
    {
        NomEntry.Text = equipe.Nom;

        DescriptionEntry.Text = equipe.Description;

        IdEquipe = equipe.Id;

        Enregistrer.Text = "Mettre à jour";
    }

    // Methode qui met à jour ou enregistre une nouvelle équipe en vérifiant que les données soient présentes.
	public async void OnEnregistrerOuMettreAJourClicked(object sender, EventArgs e)
	{
		if (!SaisonEnCours())
		{
			string nomEquipe = NomEntry.Text;
			string descriptionEquipe = DescriptionEntry.Text;

			if (!string.IsNullOrWhiteSpace(nomEquipe) && !string.IsNullOrWhiteSpace(descriptionEquipe))
			{
				if (IdEquipe >= 0)
				{
					var equipeExistante = ServiceEquipe.ObtenirEquipeParId(IdEquipe);

					equipeExistante.Nom = nomEquipe;
					equipeExistante.Description = descriptionEquipe;

                    ServiceEquipe.MettreAJourEquipe(equipeExistante);

                    await DisplayActionSheet("Équipe mise à jour avec succès!", "OK", null);
                }
				else 
				{
                    var nouvelleEquipe = new Equipe
                    {
                        Nom = nomEquipe,
                        Description = descriptionEquipe
                    };

                    ServiceEquipe.AjouterEquipe(nouvelleEquipe);

                    await DisplayActionSheet("Équipe ajoutée avec succès!", "OK", null);
                }

				await AppelVifDorVolant();
				await Navigation.PopAsync();
			}
			else
			{
				await DisplayAlert("Erreur", "Le nom de l'équipe ne peut pas être vide.", "OK");
			}
		}
    }

    // Methode pour l'animation au changement de page.
    // On valide les coordonnées entrées et on lance l'animation pour ensuite refaire disparaitre l'image hors écran.
    private async Task AppelVifDorVolant()
    {
        VifDorVolant.IsVisible = true;
        VifDorVolant.TranslationY = -1000;
        await VifDorVolant.TranslateTo(0, 1000, 400, Easing.Linear);
        VifDorVolant.IsVisible = false;
    }

    // Methode utilisée pour vérifier si une saison est en cours ou pas. Ajout ici par sécurité supplémentaire.
    private bool SaisonEnCours()
    {
        if (ServiceMatch.ObtenirTousLesMatchs().Count() > 0)
        {
            DisplayAlert("Action impossible!", "La saison de quidditch a commencé.", "OK");
            return true;
        }
        return false;
    }
}