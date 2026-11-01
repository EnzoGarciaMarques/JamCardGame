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
    [SerializeField] Transform[] slots;
    public bool[] available;
    public static YourCards instance;
    public GameManager manager;
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
    }
    private void Update()
    {
        if (deck.Count >= 1)
        {
            Cards randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            for (int i = 0; i < available.Length; i++)
            {
                if (available[i] == true && manager.turn % 2 != 0)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = slots[i].position;
                    available[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }
}
        
    
