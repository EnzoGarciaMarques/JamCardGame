using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyCards : MonoBehaviour
{
    public List<CardsEnemy> deck;
    public List<CardsEnemy> pileCenter;
    public static EnemyCards instance;
    public GameManager manager;
    public PlayedCard play;
    public int tries;
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
        play = FindAnyObjectByType<PlayedCard>();
    }
    private void Update()
    {
        while (manager.turn % 2 == 0 && manager.canPlay == true && play.color != 0)
        {
            CardsEnemy randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            if (randCard.type == play.type || randCard.color == play.color && !randCard.playedCard)
            {
                randCard.gameObject.SetActive(true);
                randCard.Play();
            }
        }
    }
}
