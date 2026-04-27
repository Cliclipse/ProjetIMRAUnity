using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] private GameObject[] billes;
    private Compteur compteur;
    private int totalBilles = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        compteur = GetComponentInChildren<Compteur>();
        AddBilles(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
