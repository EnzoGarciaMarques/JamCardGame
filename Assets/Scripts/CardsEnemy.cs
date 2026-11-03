using NUnit.Framework.Internal;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardsEnemy : MonoBehaviour
{
    public int type;
    public int color;
    [SerializeField] bool playedCard = false;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] string descriptiontxt;
    public float velocity;
    [SerializeField] Transform playPos;
    [SerializeField] Transform start;
    private bool update = false;
    [SerializeField] int effect;
    private int placedTurn;
    public PlayedCard play;
    public GameManager manager;
    public bool steal = false;
    public EnemyCards enemy;
    SpriteRenderer sr;
    public LakeCards lake;
    public SunCards sun;
    public MoonCards moon;
    public FlorestCards florest;

    private void Start()
    {
        play = FindAnyObjectByType<PlayedCard>();
        manager = FindAnyObjectByType<GameManager>();
        enemy = FindAnyObjectByType<EnemyCards>();
        sr = GetComponent<SpriteRenderer>();
        sun = FindAnyObjectByType<SunCards>();
        florest = FindAnyObjectByType<FlorestCards>();
        lake = FindAnyObjectByType<LakeCards>();
        moon = FindAnyObjectByType<MoonCards>();
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
            manager.canPlay = true;
            if (!florest.notEnemy || effect != 0 || !moon.noOnePLays)
            {
                if (type == 1)
                {
                    sun.Ability(effect);
                }
            }
            else
            {
                florest.notEnemy = false;
            }
            manager.turn += 1;
            moon.moonPass += 1;

        }
        if (steal)
        {
            transform.position = Vector3.MoveTowards(transform.position, start.position, velocity * Time.deltaTime);
        }
        if (transform.position == start.position && steal)
        {
            steal = false;
            Play();
        }

    }
    public void Play()
    {
        manager.canPlay = false;
        placedTurn = manager.turn; 
        playedCard = true;
        sr.sortingOrder = play.layer;
        play.type = type;
        play.color = color;
        enemy.pileCenter.Add(this);
        play.layer += 1;
    }
    private void OnMouseOver()
    {
        description.text = descriptiontxt;
    }
    private void OnMouseExit()
    {
        description.text = "";
    }
}
