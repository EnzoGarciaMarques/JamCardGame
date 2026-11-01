using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyCards : MonoBehaviour
{
    public List<CardsEnemy> deck;
    public static EnemyCards instance;
    public GameManager manager;
    public PlayedCard play;
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
        if (manager.turn % 2 == 0 && manager.canPlay)
        {
            CardsEnemy randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            while (randCard.type != play.type || randCard.color != play.color)
            {
                randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            }

            randCard.gameObject.SetActive(true);
            randCard.Play();
            deck.Remove(randCard);
        }
    }
}
