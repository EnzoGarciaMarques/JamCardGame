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

            for (int i = 0; i < deck.Count; i++)
            {
                int randomIndex = UnityEngine.Random.Range(i, deck.Count);

                CardsEnemy temp = deck[i];
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
            foreach (CardsEnemy randCard in deck)
            {
                if (randCard.type == play.type || randCard.color == play.color)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.Play();
                    return;
                }
            }
        }
    }
}
