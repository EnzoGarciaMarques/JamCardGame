using System;
using System.Drawing;
using UnityEngine;

public class MoonCards : MonoBehaviour
{
    public GameManager manager;
    public PlayedCard play;
    public Cards card;
    public int moonPass;
    public int roundsEnd;
    public GameObject panel;
    public static MoonCards instance;
    public bool noOnePLays = false;
    public bool nextEnemy = false;
    public bool nextPlayer = false;
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
    }
    public void Ability(int a)
    {
        if (a == 1)
        {
            manager.turn += 1;
        }
        if (a == 2)
        {
            play.color = 0;
            play.type = 0;
            panel.SetActive(true);
        }
        if (a == 3)
        {
            noOnePLays = true;
            moonPass = 0;
            roundsEnd = 2;
        }
        if (a == 4)
        {
            your.Shuffle();
        }
    }
    private void Update()
    {
        if (moonPass == roundsEnd)
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

