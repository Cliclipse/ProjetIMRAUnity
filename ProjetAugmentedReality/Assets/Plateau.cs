using UnityEngine;

public class Plateau : MonoBehaviour
{
    [SerializeField]
    private Case[]
        cases; //Elles sont placées selon l'ordre inverse des aiguillies d'une montre en partant d'en haut à gauche

    public int[] scores;

    public void
        Semer(int caseChoisie,
            int joueur) // joueur = 0 ou 1, je traite pas encore l'edgecase du fait que l'autre doit pouvoir jouer
    {
        if (joueur != 0 && joueur != 1)
        {
            Debug.Log("joueur incorrect");
        }

        int billeLibre = cases[caseChoisie].getTotalBille();
        cases[caseChoisie].Vider();

        for (int i = 1; i <= billeLibre; i++)
        {
            int numeroCase = (caseChoisie + i) % 10;
            Case caseActuelle = cases[numeroCase];
            caseActuelle.AddBille();

            if (i == billeLibre && caseActuelle.getTotalBille() == 2 ||
                caseActuelle.getTotalBille() == 3) // dernière bille placée
            {
                scores[joueur] += caseActuelle.getTotalBille();
                caseActuelle.Vider();
            }
        }
    }
}
