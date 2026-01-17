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
        ActualiserMatchDeLequipe(equipe);
    }

	public void ActualiserMatchDeLequipe(Equipe equipe)
	{
		var matchs = ServiceMatch.ObtenirTousLesMatchs().Where(m => m.EquipeDomicileId == equipe.Id || m.EquipeExterieurId == equipe.Id).ToList();

		foreach (var match in matchs)
		{
			match.NomEquipeSuivie = equipe.Nom;

			if (match.EquipeDomicileId == equipe.Id)
            {
				match.NomEquipeAdverse = ServiceEquipe.ObtenirEquipeParId(match.EquipeExterieurId).Nom;
			}
			else
			{
				match.NomEquipeAdverse = ServiceEquipe.ObtenirEquipeParId(match.EquipeDomicileId).Nom;
            }
        }
        MatchsCollectionView.ItemsSource = matchs;
    }

	public void ModifierEquipeClicked(object sender, EventArgs e)
	{
		int id = Convert.ToInt32(Id.Text);
        Equipe equipe = ServiceEquipe.ObtenirEquipeParId(id);
        Navigation.PushAsync(new EquipePage(equipe));
    }

	public void ActualiserEquipe(string id) 
	{
		int equipeId = int.Parse(id);
		Equipe equipe = ServiceEquipe.ObtenirEquipeParId(equipeId);
		TitreLabel.Text = equipe.Nom;
		DescriptionLabel.Text = equipe.Description;
		ActualiserMatchDeLequipe(equipe);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ActualiserEquipe(Id.Text);
    }


}