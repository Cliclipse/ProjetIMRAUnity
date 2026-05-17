using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Renderer))]
public class Case : MonoBehaviour
{
    [SerializeField] private List<Transform> billes; // Liste de Transform comme demandé
    private Compteur compteur;

    [HideInInspector] public int totalBilles = 0;

    private Color couleurOriginale;
    private Renderer rend;

    void Start()
    {
        compteur = GetComponentInChildren<Compteur>();
        rend = GetComponent<Renderer>();
        couleurOriginale = rend.material.color; 


        SetBilles(4);
    }

    void Update()
    {

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


    public void SetBilles(int amount)
    {
        for (int i = 0; i < billes.Count; i++)
        {
            billes[i].gameObject.SetActive(i < amount);
        }
    }

    public void AddBille()
    {

        foreach (Transform bille in billes)
        {
            if (!bille.gameObject.activeSelf)
            {
                bille.gameObject.SetActive(true);
                break;
            }
        }
    }

    public int TakeAllBilles()
    {
        int prises = totalBilles;
        foreach (Transform bille in billes)
        {
            bille.gameObject.SetActive(false);
        }
        return prises;
    }



    private void OnMouseEnter()
    {
        rend.material.color = Color.green;
    }

    private void OnMouseExit()
    {

        rend.material.color = couleurOriginale;
    }

    private void OnMouseDown()
    {

        AwaleManager.Instance.JouerCoup(this);
    }
}