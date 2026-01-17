using Scores.Pages;
using Scores.Models;

namespace Scores.Pages;

public partial class EquipePage : ContentPage
{
	public EquipePage(Equipe equipe = null)
	{
		InitializeComponent();

		if (equipe != null)
		{
			NomEntry.Text = equipe.Nom;

			DescriptionEntry.Text = equipe.Description;

			Id.Text = equipe.Id.ToString();

			Enregistrer.Text = "Mettre à jour";
			Enregistrer.Clicked -= OnEnregistrerClicked;
            Enregistrer.Clicked += OnUpdateClicked;
        }
    }

	public void OnEnregistrerClicked(object sender, EventArgs e)
	{
		string nomEquipe = NomEntry.Text;
		string descriptionEquipe = DescriptionEntry.Text;

        if (!string.IsNullOrWhiteSpace(nomEquipe) && !string.IsNullOrWhiteSpace(descriptionEquipe))
		{
			var nouvelleEquipe = new Equipe
			{
				Nom = nomEquipe, 
				Description = descriptionEquipe
            };
			Services.ServiceEquipe.AjouterEquipe(nouvelleEquipe);

            Navigation.PopAsync();
        }
		else
		{
			DisplayAlert("Erreur", "Le nom de l'équipe ne peut pas être vide.", "OK");
		}
    }

	public void OnUpdateClicked(object sender, EventArgs e)
	{
		string nomEquipe = NomEntry.Text;
		string descriptionEquipe = DescriptionEntry.Text;

		if (!string.IsNullOrWhiteSpace(nomEquipe) && !string.IsNullOrWhiteSpace(descriptionEquipe))
		{
			int equipeId = int.Parse(Id.Text);
			var equipeExistante = Services.ServiceEquipe.ObtenirEquipeParId(equipeId);

			equipeExistante.Nom = nomEquipe;
			equipeExistante.Description = descriptionEquipe;

            Services.ServiceEquipe.MettreAJourEquipe(equipeExistante);

			Navigation.PopAsync();
		}
		else
		{
			DisplayAlert("Erreur", "Le nom de l'équipe ne peut pas être vide.", "OK");
        }
    }
}