using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private GameObject[] billes;
    private Compteur compteur;

    [HideInInspector] public int totalBilles = 0;

    void Start()
    {
        compteur = GetComponentInChildren<Compteur>();
        SetBilles(0);
    }

    void Update()
    {
        int count = 0;
        foreach (GameObject bille in billes)
        {
            if (bille != null && bille.activeSelf)
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
            if (billes[i] != null)
            {
                billes[i].SetActive(i < amount);
            }
        }
    }

    public void AddBilles(int amount)
    {
        int billesAjoutees = 0;
        foreach (GameObject bille in billes)
        {
            if (bille != null && !bille.activeSelf)
            {
                bille.SetActive(true);
                billesAjoutees++;
                if (billesAjoutees >= amount) break;
            }
        }
    }
}