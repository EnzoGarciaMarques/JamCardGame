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
        if (manager.turn % 2 == 0 && manager.canPlay == true && play.color != 0)
        {
            CardsEnemy randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            for (tries = 1; tries <= 32; tries++)
            {
                if (randCard.type == play.type || randCard.color == play.color)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.Play();
                    deck.Remove(randCard);
                    return;
                }
                randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            }
            if (tries >= 32)
            {
                CardsEnemy randSteal = pileCenter[UnityEngine.Random.Range(0, pileCenter.Count)];
                while (randSteal.type == play.type || randSteal.color == play.color)
                {
                    randSteal = deck[UnityEngine.Random.Range(0, deck.Count)];
                }
                randSteal.gameObject.SetActive(true);
                randSteal.steal = true;
            }
        }
    }
}
