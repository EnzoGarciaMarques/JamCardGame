using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MoonCards : MonoBehaviour
{
    public GameManager manager;
    public PlayedCard play;
    public Cards card;
    public int moonPass;
    public int roundsEnd;
    public GameObject panel;
    [SerializeField] SpriteRenderer card1;
    [SerializeField] SpriteRenderer card2;
    [SerializeField] SpriteRenderer card3;
    [SerializeField] SpriteRenderer card4;
    public static MoonCards instance;
    public bool noOnePLays = false;
    public bool nextEnemy = false;
    public bool nextPlayer = false;
    public bool boitata = false;
    public bool boitata2 = false;
    public Sanity san;
    public YourCards your;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
        san = FindAnyObjectByType<Sanity>();
        your = FindAnyObjectByType<YourCards>();
        play = FindAnyObjectByType<PlayedCard>();
    }
    public void Ability(int a)
    {
        if (a == 1)
        {
            manager.turn += 1;
            manager.roundsMin += 1;
        }
        if (a == 2)
        {
            panel.SetActive(true);

        }
        if (a == 3)
        {
            noOnePLays = true;
            moonPass = -1;
            roundsEnd = 2;
        }
        if (a == 4)
        {
            if(your.pileCenter.Count >= 1)
            {
                your.Shuffle();
            }
        }
        if(a == 5)
        {
            boitata = true;
            boitata2 = false;
        }
        if (a == 12)
        {
            boitata2 = true;
            boitata = false;
        }
    }
    private void Update()
    {
        if (moonPass >= roundsEnd)
        {
            noOnePLays = false;
        }
    }
    public void Yellow()
    {
        play.type = 0;
        play.color = 1;
        panel.SetActive(false);
    }
    public void Red()
    {
        play.type = 0;
        play.color = 3;
        panel.SetActive(false);
    }
    public void White()
    {
        play.color = 2;
        play.type = 0;
        panel.SetActive(false);
    }
    public void Black()
    {
        play.type = 0;
        play.color = 4;
        panel.SetActive(false);
    }
}

