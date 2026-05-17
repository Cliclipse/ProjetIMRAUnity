using UnityEngine;

public class AwaleManager : MonoBehaviour
{
    public static AwaleManager Instance;

    [Header("Plateau de jeu")]
    [Tooltip("Les 12 cases du plateau. Indices 0 à 5 pour J1, 6 à 11 pour J2.")]
    public Case[] plateau = new Case[12];

    public Base baseJoueur1;
    public Base baseJoueur2;

    [Header("État du jeu")]
    public bool tourJoueur1 = true;

    void Awake()
    {
        // Création d'un Singleton pour y accéder facilement depuis les Cases
        if (Instance == null) Instance = this;
    }

    public void JouerCoup(Case caseCliquee)
    {
        // 1. Trouver l'index de la case cliquée
        int indexCase = System.Array.IndexOf(plateau, caseCliquee);
        if (indexCase == -1) return;

        // 2. Vérifier si c'est bien le tour du bon joueur
        if (tourJoueur1 && (indexCase < 0 || indexCase > 5)) return;
        if (!tourJoueur1 && (indexCase < 6 || indexCase > 11)) return;

        // 3. Vérifier que la case n'est pas vide
        if (caseCliquee.totalBilles == 0) return;

        // --- DÉBUT DU COUP ---

        int billesEnMain = caseCliquee.TakeAllBilles();
        int indexActuel = indexCase;

        // 4. Semer les billes
        while (billesEnMain > 0)
        {
            indexActuel = (indexActuel + 1) % 12; // On avance (boucle de 0 à 11)

            // Règle de l'Awalé : on saute la case de départ si on fait un tour complet
            if (indexActuel == indexCase) continue;

            plateau[indexActuel].AddBille();
            billesEnMain--;
        }

        // 5. Gérer les captures (règles de l'Awalé)
        Capturer(indexActuel);

        // 6. Fin du tour, on passe à l'autre joueur
        tourJoueur1 = !tourJoueur1;
    }

    private void Capturer(int dernierIndex)
    {
        // On ne peut capturer que dans le camp adverse
        bool estCampAdverse = tourJoueur1 ? (dernierIndex >= 6 && dernierIndex <= 11) : (dernierIndex >= 0 && dernierIndex <= 5);

        while (estCampAdverse)
        {
            int nbBilles = plateau[dernierIndex].totalBilles;

            // Règle de l'Awalé : on capture si la case contient 2 ou 3 billes
            if (nbBilles == 2 || nbBilles == 3)
            {
                int billesCapturees = plateau[dernierIndex].TakeAllBilles();

                if (tourJoueur1) baseJoueur1.AddBilles(billesCapturees);
                else baseJoueur2.AddBilles(billesCapturees);

                // On recule d'une case pour voir si on peut capturer la précédente
                dernierIndex--;
                if (dernierIndex < 0) dernierIndex = 11;

                estCampAdverse = tourJoueur1 ? (dernierIndex >= 6 && dernierIndex <= 11) : (dernierIndex >= 0 && dernierIndex <= 5);
            }
            else
            {
                break; // Si la case n'a pas 2 ou 3 billes, la rafle s'arrête
            }
        }
    }
}