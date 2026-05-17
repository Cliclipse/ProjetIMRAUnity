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
        if (Instance == null) Instance = this;
    }

    public void JouerCoup(Case caseCliquee)
    {
        int indexCase = System.Array.IndexOf(plateau, caseCliquee);
        if (indexCase == -1) return;

        if (tourJoueur1 && (indexCase < 0 || indexCase > 5)) return;
        if (!tourJoueur1 && (indexCase < 6 || indexCase > 11)) return;

        int nbBilles = caseCliquee.totalBilles;
        if (nbBilles == 0) return;

        // --- NOUVELLE RÈGLE : NOURRIR L'ADVERSAIRE ---
        // On vérifie d'abord si le camp d'en face est totalement vide
        if (CampAdverseVide(tourJoueur1))
        {
            // Si oui, on vérifie mathématiquement que le coup traverse la frontière
            if (!CoupNourritAdversaire(indexCase, nbBilles, tourJoueur1))
            {
                Debug.LogWarning("Coup invalide : L'adversaire est affamé, tu dois jouer un coup qui lui donne des billes !");
                return; // Le coup est annulé, la fonction s'arrête ici
            }
        }
        // ----------------------------------------------

        int billesEnMain = caseCliquee.TakeAllBilles();
        int indexActuel = indexCase;

        // 4. Semer les billes
        while (billesEnMain > 0)
        {
            indexActuel = (indexActuel + 1) % 12;

            if (indexActuel == indexCase) continue;

            plateau[indexActuel].AddBille();
            billesEnMain--;
        }

        Capturer(indexActuel);

        tourJoueur1 = !tourJoueur1;
    }

    // --- FONCTIONS POUR VÉRIFIER LES RÈGLES ---

    private bool CampAdverseVide(bool joueur1Joue)
    {
        // On regarde les cases 6 à 11 si c'est J1, ou 0 à 5 si c'est J2
        int debut = joueur1Joue ? 6 : 0;
        int fin = joueur1Joue ? 11 : 5;

        for (int i = debut; i <= fin; i++)
        {
            if (plateau[i].totalBilles > 0)
            {
                return false; // Il y a au moins une bille, le camp n'est pas vide
            }
        }
        return true; // Le camp adverse est complètement vide
    }

    private bool CoupNourritAdversaire(int indexCase, int nbBilles, bool joueur1Joue)
    {
        // Pour que le J1 (cases 0-5) atteigne le camp adverse, la portée (index + billes) doit être au moins 6
        if (joueur1Joue && (indexCase + nbBilles >= 6)) return true;

        // Pour que le J2 (cases 6-11) atteigne le J1, la portée doit déborder la fin du plateau (donc au moins 12)
        if (!joueur1Joue && (indexCase + nbBilles >= 12)) return true;

        return false; // Le coup s'arrête avant la frontière
    }

    // ------------------------------------------

    private void Capturer(int dernierIndex)
    {
        bool estCampAdverse = tourJoueur1 ? (dernierIndex >= 6 && dernierIndex <= 11) : (dernierIndex >= 0 && dernierIndex <= 5);

        while (estCampAdverse)
        {
            int nbBilles = plateau[dernierIndex].totalBilles;

            if (nbBilles == 2 || nbBilles == 3)
            {
                int billesCapturees = plateau[dernierIndex].TakeAllBilles();

                if (tourJoueur1) baseJoueur1.AddBilles(billesCapturees);
                else baseJoueur2.AddBilles(billesCapturees);

                dernierIndex--;
                if (dernierIndex < 0) dernierIndex = 11;

                estCampAdverse = tourJoueur1 ? (dernierIndex >= 6 && dernierIndex <= 11) : (dernierIndex >= 0 && dernierIndex <= 5);
            }
            else
            {
                break;
            }
        }
    }
}