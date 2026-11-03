using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Cards : MonoBehaviour
{
    [SerializeField] int type;
    [SerializeField] int color;
    [SerializeField] int effect;
    [SerializeField] bool playedCard = false;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] string descriptiontxt;
    public float velocity;
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
        input.Card.Play.performed += Play;
        input.Card.Rip.performed += Rip;
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
        sun = FindAnyObjectByType<SunCards>();
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
            update = true;
            if (!florest.notPlayer || effect != 0 || !moon.noOnePLays)
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
            else
            {
                florest.notPlayer = false;
            }
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
                    manager.canPlay = false;
                    playedCard = true;
                    lake.notInHand += 1;
                    sr.sortingOrder = play.layer;
                    play.type = type;
                    play.color = color;
                    if (effect == 2 && type == 4) 
                    {
                        play.type = 0;
                        play.color = 0;
                    }
                    your.pileCenter.Add(this);
                    your.available[handIndex] = true;
                    play.layer += 1;
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

        if (hit.collider != null && hit.transform == transform)
        {
            if (type == 3 && effect == 7)
            {
                sun.Ability(effect);
            }
            description.text = "";
            your.sanity = 0;
            lake.notInHand += 1;
            your.ripped.Add(this);
            your.available[handIndex] = true;
            gameObject.SetActive(false);
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
