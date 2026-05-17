using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Transform> billes; // Liste de Transform
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
        // On parcourt la liste dans l'Update pour compter les billes actives
        int count = 0;
        foreach (Transform bille in billes)
        {
            if (bille.gameObject.activeSelf)
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

    // Définit précisément le nombre de billes visibles
    public void SetBilles(int amount)
    {
        for (int i = 0; i < billes.Count; i++)
        {
            billes[i].gameObject.SetActive(i < amount);
        }
    }

    // Ajoute un nombre X de billes (lors d'une capture par exemple)
    public void AddBilles(int amount)
    {
        int billesAjoutees = 0;
        foreach (Transform bille in billes)
        {
            if (!bille.gameObject.activeSelf)
            {
                bille.gameObject.SetActive(true);
                billesAjoutees++;
                if (billesAjoutees >= amount) break;
            }
        }
    }
}