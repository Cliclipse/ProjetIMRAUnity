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


        if (caseCliquee.totalBilles == 0) return;


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