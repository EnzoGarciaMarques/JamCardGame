using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class YourCards : MonoBehaviour
{
    public List<Cards> deck;
    public List<Cards> pileCenter;
    public List<Cards> ripped;
    public int sanity;
    [SerializeField] Transform[] slots;
    public bool[] available;
    public static YourCards instance;
    public Sanity san;
    public GameManager manager;
    bool first = true;
    public LakeCards lake;
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
        lake = FindAnyObjectByType<LakeCards>();
    }
    private void OnMouseDown()
    {
        if (deck.Count >= 1)
        {
            san.Insanity(sanity);
            sanity = -5;
            Buy();
        }
    }
    private void Update()
    {
        if (first)
        {
            Cards randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            for (int i = 0; i < available.Length; i++)
            {
                if (available[i] == true && manager.turn % 2 != 0)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = gameObject.transform.position;
                    while (randCard.transform.position != slots[i].position)
                    {
                        randCard.transform.position = Vector3.MoveTowards(randCard.transform.position, slots[i].position, randCard.velocity * Time.deltaTime);
                    }
                    available[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
            first = false;
        }
        if(deck.Count <= 0)
        {
            Shuffle();
        }
    }
    public void Buy()
    {
        Cards randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
        for (int i = 0; i < available.Length; i++)
        {
            if (available[i] == true && manager.turn % 2 != 0)
            {
                randCard.gameObject.SetActive(true);
                randCard.handIndex = i;
                randCard.transform.position = gameObject.transform.position;
                while (randCard.transform.position != slots[i].position)
                {
                    randCard.transform.position = Vector3.MoveTowards(randCard.transform.position, slots[i].position, randCard.velocity * Time.deltaTime);
                }
                available[i] = false;
                deck.Remove(randCard);
                return;
            }
        }
    }
    public void Shuffle()
    {
        foreach (Cards card in pileCenter)
        {
            deck.Add(card);
            card.gameObject.SetActive(false);

        }
        pileCenter.Clear();
    }
}
        
    
