using UnityEngine;

public class DisableOnBuild : MonoBehaviour
{
    void Awake()
    {
    #if !UNITY_EDITOR
        gameObject.SetActive(false); //Permet de pas essayer de simuler un env sur le vrai build
    #endif
    }
}