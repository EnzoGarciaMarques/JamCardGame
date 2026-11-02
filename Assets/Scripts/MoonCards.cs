using System;
using System.Drawing;
using UnityEngine;

public class MoonCards : MonoBehaviour
{
    public GameManager manager;
    public PlayedCard play;
    Cards card;
    private void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
    }
    private void OnMouseDown()
    {


    }
}

