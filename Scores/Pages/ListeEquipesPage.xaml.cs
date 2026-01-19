using Scores.Models;
using Scores.Services;

namespace Scores.Pages;

public partial class ListeEquipesPage : ContentPage
{
    public ListeEquipesPage()
    {
        InitializeComponent();
        ActualiserListeEquipes();
    }

    // Methode apelée au clique du bouton Ajouter.
    public async void OnAjouterEquipeClicked(object sender, EventArgs e)
    {
        if(!SaisonEnCours())
        {
            AppelEquipeVolante();
            await Navigation.PushAsync(new EquipePage());
        }
    }

    // Methode qui permet de récupérer la liste de toutes les équipes pour mettre à jour la collection view.
    public void ActualiserListeEquipes()
    {
        var equipes = ServiceEquipe.ObtenirToutesLesEquipes();

        foreach (var equipe in equipes) CalculerStats(equipe);

        EquipesCollectionView.ItemsSource = equipes.OrderByDescending(e => e.Points);
    }

    // Methode appelée automatiquement à l'apparition de la page. On appel la méthode pour actualiser la liste des équipes.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ActualiserListeEquipes();
    }

    // Méthode de calcul des points pour l'équipe en argument.
    // J'ai repris mot à mot ce qui est demandé dans l'énoncé. 3 points si gagne, 1 point si égalité, et 0 si perdu.
    // Pour savoir qui gagne je compare juste les scores.
    public static Equipe CalculerStats(Equipe equipe)
    {
        var matchs = ServiceMatch.ObtenirTousLesMatchs().Where(m => m.EquipeDomicileId == equipe.Id || m.EquipeExterieurId == equipe.Id);

        foreach (var match in matchs)
        {
            bool domicile = match.EquipeDomicileId == equipe.Id;
            bool exterieur = match.EquipeExterieurId == equipe.Id;
            
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
        return equipe;
    }

    // Méthode appelée lorsqu'une équipe est selectionée dans la liste.
    public async void EquipeSelected(object sender, SelectionChangedEventArgs e)
    {
        // Permet de convertir l'objet de la selection en objet Equipe dans la variable.
        if (e.CurrentSelection.FirstOrDefault() is Equipe equipeSelectionnee)
        {
            AppelEquipeVolante();
            await Navigation.PushAsync(new InformationEquipePage(equipeSelectionnee));
        }
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

    // Methode utilisée pour vérifier si une saison est en cours ou pas.
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