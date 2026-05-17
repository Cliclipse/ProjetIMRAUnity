using System;
using TMPro;
using UnityEngine;

public class Compteur : MonoBehaviour
{
    private int _value = 0;
    [SerializeField] private TextMeshPro text;
    

    void Start()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        text.text = _value.ToString();
    }

    public void SetToZero()
    {
        _value = 0;
        UpdateValue();
    }
    
    public void SetValue(int x)
    {
        _value = x;
        UpdateValue();
    }

    public void AddOne()
    {
        _value++;
        UpdateValue();

    }

    public void AddValue(int x)
    {
        _value += x;
        UpdateValue();

    }
    
    
}
