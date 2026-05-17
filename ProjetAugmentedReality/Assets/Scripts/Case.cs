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
        couleurOriginale = rend.material.color; // On sauvegarde la couleur de base

        // Initialisation de l'Awalé : 4 billes par case
        SetBilles(4);
    }

    void Update()
    {
        // On parcourt la liste des transforms pour compter les billes actives
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

    // Fonction pour définir un nombre précis de billes (au démarrage ou lors d'un semis)
    public void SetBilles(int amount)
    {
        for (int i = 0; i < billes.Count; i++)
        {
            billes[i].gameObject.SetActive(i < amount);
        }
    }

    public void AddBille()
    {
        // On active la première bille inactive trouvée
        foreach (Transform bille in billes)
        {
            if (!bille.gameObject.activeSelf)
            {
                bille.gameObject.SetActive(true);
                break;
            }
        }
    }

    // Vide la case et retourne le nombre de billes prises
    public int TakeAllBilles()
    {
        int prises = totalBilles;
        foreach (Transform bille in billes)
        {
            bille.gameObject.SetActive(false);
        }
        return prises;
    }

    // --- GESTION DE LA SOURIS ---

    private void OnMouseEnter()
    {
        // Se colore en vert si on la survole (et qu'elle n'est pas vide, optionnel)
        rend.material.color = Color.green;
    }

    private void OnMouseExit()
    {
        // Reprend sa couleur normale
        rend.material.color = couleurOriginale;
    }

    private void OnMouseDown()
    {
        // Quand on clique dessus, on prévient le manager du jeu
        AwaleManager.Instance.JouerCoup(this);
    }
}