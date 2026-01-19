using Scores.Models;
using Scores.Services;

namespace Scores.Pages;

public partial class InformationEquipePage : ContentPage
{
	public InformationEquipePage(Equipe equipe)
	{
		InitializeComponent();
		Id.Text = equipe.Id.ToString();
		ActualiserEquipe(Id.Text);
        RécupérerMatchsDeLequipe(equipe);
    }

    // Méthode permettant de récupérer les matchs de l'équipe en argument, selon l'affichage voulu par l'ennoncé.
	public void RécupérerMatchsDeLequipe(Equipe equipe)
	{
		var matchs = ServiceMatch.ObtenirTousLesMatchs().Where(m => m.EquipeDomicileId == equipe.Id || m.EquipeExterieurId == equipe.Id).ToList();

		foreach (var match in matchs)
		{
			match.NomEquipeSuivie = equipe.Nom;

			if (match.EquipeDomicileId == equipe.Id)
            {
                match.ScoreEquipeSuivie = match.ScoreDomicile;
                match.ScoreExterieur = match.ScoreExterieur;
				match.NomEquipeAdverse = ServiceEquipe.ObtenirEquipeParId(match.EquipeExterieurId).Nom;
			}
			else
			{
                match.ScoreEquipeSuivie = match.ScoreExterieur;
                match.ScoreEquipeAdverse = match.ScoreDomicile;
				match.NomEquipeAdverse = ServiceEquipe.ObtenirEquipeParId(match.EquipeDomicileId).Nom;
            }
        }
        MatchsCollectionView.ItemsSource = matchs;
    }

    // Méthode appelée lors du clique sur le bouton modifier.
	public async void ModifierEquipeClicked(object sender, EventArgs e)
	{
        if (!SaisonEnCours())
        {
            int id = Convert.ToInt32(Id.Text);
            Equipe equipe = ServiceEquipe.ObtenirEquipeParId(id);
            await AppelVifDorVolant();
            await Navigation.PushAsync(new EquipePage(equipe));
        }
    }

    // Méthode permettant de récupérer toutes les informations de l'équipe voulue.
	public void ActualiserEquipe(string id) 
	{
		int equipeId = int.Parse(id);
		Equipe equipe = ServiceEquipe.ObtenirEquipeParId(equipeId);
		TitreLabel.Text = equipe.Nom;
		DescriptionLabel.Text = equipe.Description;
        RécupérerMatchsDeLequipe(equipe);
    }

    // Methode appelée automatiquement à l'apparition de la page. On appel la méthode pour actualiser l'équipe si elle a été update.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ActualiserEquipe(Id.Text);
    }

    // Methode pour l'animation au changement de page.
    // On valide les coordonnées entrées et on lance l'animation pour ensuite refaire disparaitre l'image hors écran.
    private async Task AppelVifDorVolant()
    {
        VifDorVolant.IsVisible = true;
        VifDorVolant.TranslationX = 1000;
        await VifDorVolant.TranslateTo(-500, -200, 400, Easing.Linear);
        VifDorVolant.IsVisible = false;
    }

    // Metjode utilisée pour vérifier si une saison est en cours ou pas.
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