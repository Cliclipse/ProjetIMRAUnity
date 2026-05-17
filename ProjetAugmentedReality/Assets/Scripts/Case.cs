using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] private GameObject[] billes;
    [SerializeField] private int initialNbBilles = 4;

    private Compteur compteur;
    
    private int totalBilles = 0;

    public int getTotalBille()
    {
        return totalBilles;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalBilles = initialNbBilles;
        compteur = GetComponentInChildren<Compteur>();
        compteur.SetValue(totalBilles);
        AddBilles(totalBilles);
    }

    // Update is called once per frame

    public void AddBille()
    {
        if (totalBilles < billes.Length)
        {
            billes[totalBilles].SetActive(true);
            compteur.AddOne();
            totalBilles++;
        }
        else
        {
            compteur.AddOne();
            totalBilles++;
        }
    }

    public void AddBilles(int x)
    {
        for (int i = 0; i < x; i++)
        {
            AddBille();
        }
    }

    public void Vider()
    {
        totalBilles = 0;
        compteur.SetToZero();
    }
}
