using UnityEngine;

public class Base : MonoBehaviour
{
    // On garde GameObject[] pour que Unity retrouve tes assignations automatiquement !
    [SerializeField] private GameObject[] billes;
    private Compteur compteur;

    [HideInInspector] public int totalBilles = 0;

    void Start()
    {
        compteur = GetComponentInChildren<Compteur>();
        // Au début du jeu, la base de stockage est vide
        SetBilles(0);
    }

    void Update()
    {
        int count = 0;
        foreach (GameObject bille in billes)
        {
            // Sécurité au cas où un élément du tableau serait vide dans l'émetteur
            if (bille.activeSelf)
            {
                count++;
            }
        }

        totalBilles = count;
        if (compteur != null)
        {
            compteur.SetValue(totalBilles);
        }
    }

    public void SetBilles(int amount)
    {
        for (int i = 0; i < billes.Length; i++)
        {
            
            billes[i].SetActive(i < amount);
            
        }
    }

    public void AddBilles(int amount)
    {
        int billesAjoutees = 0;
        foreach (GameObject bille in billes)
        {
            if (!bille.activeSelf)
            {
                bille.SetActive(true);
                billesAjoutees++;
                if (billesAjoutees >= amount) break;
            }
        }
    }
}