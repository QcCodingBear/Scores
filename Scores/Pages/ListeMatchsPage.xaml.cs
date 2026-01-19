using Scores.Models;
using Scores.Services;

namespace Scores.Pages;

public partial class ListeMatchsPage : ContentPage
{
    public ListeMatchsPage()
    {
        InitializeComponent();
        ActualiserListeMatchs();
    }

    // Methode apelée au clique du bouton Ajouter.
    public async void AjouterMatchClicked(object sender, EventArgs e)
    {
        AppelEquipeVolante();
        await Navigation.PushAsync(new MatchPage());
    }

    // Methode qui permet de récupérer la liste de tous les matchs pour mettre à jour la collection view.
    public void ActualiserListeMatchs()
    {
        var matchs = ServiceMatch.ObtenirTousLesMatchs();

        foreach (var match in matchs)
        {
            match.NomEquipeSuivie = ServiceEquipe.ObtenirEquipeParId(match.EquipeDomicileId).Nom;
            match.NomEquipeAdverse = ServiceEquipe.ObtenirEquipeParId(match.EquipeExterieurId).Nom;
        }

        MatchsCollectionView.ItemsSource = matchs.OrderByDescending(e => e.DateMatch);
    }

    // Methode appelée automatiquement à l'apparition de la page. On appel la méthoe pour actualiser la liste des matchs.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ActualiserListeMatchs();
    }

    // Methode pour l'animation au changement de page.
    // On valide les coordonnées entrées et on lance l'animation pour ensuite refaire disparaitre l'image hors écran.
    private void AppelEquipeVolante()
    {
        EquipeVolante.IsVisible = true;
        EquipeVolante.TranslationX = -500;
        EquipeVolante.TranslateTo(1000, 0, 400, Easing.Linear);
        EquipeVolante.IsVisible = false;
    }


}