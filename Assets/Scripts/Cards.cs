using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Cards : MonoBehaviour
{
    [SerializeField] int type;
    [SerializeField] AudioClip playClip;
    [SerializeField] int color;
    [SerializeField] int effect;
    [SerializeField] bool playedCard = false;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] string descriptiontxt;
    public float velocity;
    public int sanity = 0;
    [SerializeField] Transform playPos;
    private bool update = false;
    public PlayedCard play;
    public YourCards your;
    public GameManager manager;
    public int handIndex;
    SpriteRenderer sr;
    public SunCards sun;
    public LakeCards lake;
    public FlorestCards florest;
    public MoonCards moon;

    InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Card.Play.performed += Play;
        input.Card.Rip.performed += Rip;
        input.Card.Enable();
    }
    private void OnDisable()
    {
        input.Card.Play.performed -= Play;
        input.Card.Rip.performed -= Rip;
        input.Card.Disable();
    }
    private void Start()
    {
        your = FindAnyObjectByType<YourCards>();
        play = FindAnyObjectByType<PlayedCard>();
        lake = FindAnyObjectByType<LakeCards>();
        florest = FindAnyObjectByType<FlorestCards>();
        moon = FindAnyObjectByType<MoonCards>();
        manager = FindAnyObjectByType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (playPos != null && playedCard)
        {
            transform.position = Vector3.MoveTowards(transform.position, playPos.position, velocity * Time.deltaTime);
        }
        if (transform.position == playPos.position && !update)
        {
            playedCard = false;
            update = true;
            if (!florest.notPlayer && effect != 0 && !moon.noOnePLays)
            {
                Power();
            }
            florest.notPlayer = false;
            your.pileCenter.Add(this);
            manager.turn += 1;
            moon.moonPass += 1;
            manager.canPlay = true;
        }
    }

    void Play(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null && hit.transform == transform)
        {
            if (play.type == type || play.color == color || play.type == 0 && !your.pileCenter.Contains(this) && !playedCard)
            {
                if (manager.turn % 2 != 0 && manager.canPlay)
                {
                    playedCard = true;
                    CardPlay();
                }
            }
        }      
    }
    void Rip(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.transform == transform && !your.pileCenter.Contains(this))
        {
            manager.Anima();
            if (type == 3 && effect == 7)
            {
                florest.Ability(effect);
            }
            description.text = "";
            if (moon.boitata)
            {
                your.sanity = -15;
                moon.boitata = false;
            }
            else if (moon.boitata2)
            {
                your.sanity = 10;
                moon.boitata2 = false;
            }
            else
            {
                your.sanity = 0;
            }
            lake.notInHand += 1;
            your.ripped.Add(this);
            your.available[handIndex] = true;
            gameObject.SetActive(false);
        }
    }
    void CardPlay()
    {
        SFXManagement.Instance.PlaySoundFXClip(playClip, transform, 1f);
        manager.canPlay = false;
        lake.notInHand += 1;
        sr.sortingOrder = play.layer;
        play.type = type;
        play.color = color;
        if (effect == 2 && type == 4 && !florest.notPlayer && !moon.noOnePLays)
        {
            play.type = 0;
            play.color = 0;
        }
        your.available[handIndex] = true;
        play.layer += 1;
    }

    void Power()
    {
        if (type == 1)
        {
            sun.Ability(effect);
        }
        else if (type == 2)
        {
            lake.Ability(effect);
        }
        else if (type == 3 && effect != 7)
        {
            florest.Ability(effect);
        }
        else if (type == 4)
        {
            moon.Ability(effect);
        }
    }
    private void OnMouseOver()
    {
        if (!your.pileCenter.Contains(this))
        {
            description.text = descriptiontxt;
        }
    }
    private void OnMouseExit()
    {
        description.text = "";
    }
    
}
