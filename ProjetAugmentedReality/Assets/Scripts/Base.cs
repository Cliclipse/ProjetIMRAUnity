using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] GameObject[] billes;
    Compteur compteur;
    private int totalBilles = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        compteur = GetComponentInChildren<Compteur>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBille()
    {
        if (totalBilles < billes.Length)
        {
            billes[0].SetActive(true);
            compteur.AddOne();
            totalBilles++;
        }
    }
}
