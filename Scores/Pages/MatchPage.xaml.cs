using Scores.Pages;
using Scores.Models;

namespace Scores.Pages;

public partial class MatchPage : ContentPage
{

    public MatchPage()
    {
        InitializeComponent();
        RecupererPickers();
    }

    // Cette méthode permet de mettre à jour le Picker pour le vif d'or, pour que seules les équipes s'affrontant puissent être proposées.
    public void UpdateVifDorPicker(object sender, EventArgs e)
    {
        VifDorAttrapeParEquipePicker.Items.Clear();

        string? equipeDomicile = EquipeDomicilePicker.SelectedItem?.ToString();
        string? equipeExterieur = EquipeExterieurPicker.SelectedItem?.ToString();

        if (equipeDomicile != null)
            VifDorAttrapeParEquipePicker.Items.Add(EquipeDomicilePicker.SelectedItem.ToString());

        if (equipeExterieur != null)
            VifDorAttrapeParEquipePicker.Items.Add(EquipeExterieurPicker.SelectedItem.ToString());
    }

    // Méthode pour remplir les Pickers des équipes et date.
    private void RecupererPickers()
    {
        var equipes = Services.ServiceEquipe.ObtenirToutesLesEquipes();

        foreach (var equipe in equipes)
        {
            EquipeDomicilePicker.Items.Add(equipe.Nom);
            EquipeExterieurPicker.Items.Add(equipe.Nom);
        }
        DatePickerMatch.Date = DateTime.Now;
    }

    // Methode pour verifier la validitée des informations du formulaire et retourner un objet Match correctement formé.
    private Match? ValiderPickerAndEntry()
    {
        var equipes = Services.ServiceEquipe.ObtenirToutesLesEquipes();
        int scoreDomicile;
        int scoreExterieur;

        // On récupére les valeurs des Pickers
        string? equipeDomicileSelectionnee = EquipeDomicilePicker.SelectedItem?.ToString();
        string? equipeExterieurSelectionnee = EquipeExterieurPicker.SelectedItem?.ToString();
        string? vifDorEquipeSelectionnee = VifDorAttrapeParEquipePicker.SelectedItem?.ToString();
        DateTime dateMatch = DatePickerMatch.Date;

        // On vérifie d'abord que tous les champs sont remplis et correctement remplus
        if (equipeDomicileSelectionnee == null || equipeExterieurSelectionnee == null || vifDorEquipeSelectionnee == null ||
            string.IsNullOrWhiteSpace(ScoreDomicileEntry.Text) || string.IsNullOrWhiteSpace(ScoreExterieurEntry.Text))
        {
            DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return null;
        }
        if (!Int32.TryParse(ScoreDomicileEntry.Text, out scoreDomicile) ||
            !Int32.TryParse(ScoreExterieurEntry.Text, out scoreExterieur) ||
            scoreDomicile < 0 || scoreExterieur < 0)
        {
            DisplayAlert("Erreur", "Le score doit être un chiffre entier supérieur ou égale à 0.", "OK");
            return null;
        }
        if (dateMatch > DateTime.Now)
        {
            DisplayAlert("Erreur", "La date du match ne peut pas être dans le futur.", "OK");
            return null;
        }

        // On récupére ensuite les valeurs qui nous intéresses
        int equipeDomicileId = equipes.FirstOrDefault(e => e.Nom == equipeDomicileSelectionnee).Id;
        int equipeExterieurId = equipes.FirstOrDefault(e => e.Nom == equipeExterieurSelectionnee).Id;
        int vifDorAttrapeParEquipeId = equipes.FirstOrDefault(e => e.Nom == vifDorEquipeSelectionnee).Id;
        
        // Et on fait la dernière vérification logique avant de créer le nouvel objet Match.
        if (equipeDomicileId == equipeExterieurId)
        {
            DisplayAlert("Erreur", "L'équipe domicile et l'équipe extérieur ne peuvent pas être les mêmes.", "OK");
            return null;
        }

        var nouveauMatch = new Match
        {
            DateMatch = dateMatch,
            EquipeDomicileId = equipeDomicileId,
            EquipeExterieurId = equipeExterieurId,
            ScoreDomicile = scoreDomicile,
            ScoreExterieur = scoreExterieur,
            VifDorAttrapeParEquipeId = vifDorAttrapeParEquipeId
        };

        return nouveauMatch;
    }

    // Methode appelée en cliquant sur Enregistrer. 
    public async void OnEnregistrerClicked(object sender, EventArgs e)
    {
        Match? nouveauMatch = ValiderPickerAndEntry();

        if (nouveauMatch != null)
        {
            Services.ServiceMatch.AjouterMatch(nouveauMatch);
            await DisplayActionSheet("Match ajouté avec succès!", "OK", null);
            await AppelVifDorVolant();
            await Navigation.PopAsync();
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
}