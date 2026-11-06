using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class YourCards : MonoBehaviour
{
    public List<Cards> deck;
    public List<Cards> pileCenter;
    public List<Cards> ripped;
    [SerializeField] AudioClip shuffleClip;
    [SerializeField] AudioClip playClip;
    public int sanity;
    [SerializeField] Transform[] slots;
    public bool[] available;
    public static YourCards instance;
    public Sanity san;
    public GameManager manager;
    bool first = true;
    public LakeCards lake;
    InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        input.Card.Play.performed += Play;
        input.Card.Enable();
    }
    private void OnDisable()
    {
        input.Card.Play.performed -= Play;
        input.Card.Disable();
    }
    private void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
        san = FindAnyObjectByType<Sanity>();
        lake = FindAnyObjectByType<LakeCards>();
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
                lake.notInHand -= 1;
                SFXManagement.Instance.PlaySoundFXClip(playClip, transform, 1f);
                san.Insanity(sanity);
                sanity = -5;
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
        SFXManagement.Instance.PlaySoundFXClip(shuffleClip, transform, 1f);
        pileCenter.Clear();
    }
    void Play(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null && hit.transform == transform)
        {
            if (deck.Count >= 1)
            {
                Buy();
            }
        }
    }
}
        
    
