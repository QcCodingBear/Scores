using Scores.Models;

namespace Scores.Pages;

public partial class ListeEquipesPage : ContentPage
{
    public ListeEquipesPage()
    {
        InitializeComponent();
        ActualiserListeEquipes();
    }

    public void OnAjouterEquipeClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EquipePage());
    }

    public void ActualiserListeEquipes()
    {
        var equipes = Services.ServiceEquipe.ObtenirToutesLesEquipes();

        foreach (var equipe in equipes) CalculerStats(equipe);

        EquipesCollectionView.ItemsSource = equipes.OrderByDescending(e => e.Points);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ActualiserListeEquipes();
    }

    public static Equipe CalculerStats(Equipe equipe)
    {
        var matchs = Services.ServiceMatch.ObtenirTousLesMatchs();

        foreach (var match in matchs)
        {
            bool domicile = match.EquipeDomicileId == equipe.Id;
            bool exterieur = match.EquipeExterieurId == equipe.Id;

            if (domicile || exterieur)
            {
                equipe.NombreMatchs++;

                int scoreEquipe = domicile ? match.ScoreDomicile : match.ScoreExterieur;
                int scoreAdverse = domicile ? match.ScoreExterieur : match.ScoreDomicile;

                equipe.ScoresMarques += scoreEquipe;
                equipe.ScoresAdverses += scoreAdverse;

                if (match.VifDorAttrapeParEquipeId == equipe.Id)
                    equipe.VifsDorAttrapes++;

                if (scoreEquipe > scoreAdverse)
                    equipe.Points += 3;
                else if (scoreEquipe == scoreAdverse)
                    equipe.Points += 1;
            }
        }
        return equipe;
    }

    public void EquipeClicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Equipe equipeSelectionnee)
            Navigation.PushAsync(new InformationEquipePage(equipeSelectionnee));
    }
}