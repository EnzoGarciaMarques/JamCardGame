using NUnit.Framework.Internal;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardsEnemy : MonoBehaviour
{
    public int type;
    public int color;
    public bool playedCard = false;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] string descriptiontxt;
    public float velocity;
    [SerializeField] Transform playPos;
    public Transform start;
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
    [SerializeField] AudioClip playClip;

    private void Start()
    {
        play = FindAnyObjectByType<PlayedCard>();
        manager = FindAnyObjectByType<GameManager>();
        enemy = FindAnyObjectByType<EnemyCards>();
        sr = GetComponent<SpriteRenderer>();
        florest = FindAnyObjectByType<FlorestCards>();
        lake = FindAnyObjectByType<LakeCards>();
        moon = FindAnyObjectByType<MoonCards>();
    }
    private void Update()
    {
        if (playPos != null && playedCard && !steal)
        {
            transform.position = Vector3.MoveTowards(transform.position, playPos.position, velocity * Time.deltaTime);
        }
        if (transform.position == playPos.position && !update)
        {
            description.text = descriptiontxt;
            update = true;
            if (!florest.notEnemy && effect != 0 && !moon.noOnePLays)
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
                florest.notEnemy = false;
            }
            manager.roundsMin += manager.roundPlus;
            florest.lessInsanity = false;
            if (sun.roundsPass >= 0)
            {
                sun.roundsPass += 1;
            }
            if (lake.lakePass >= 0)
            {
                lake.lakePass += 1;
            }
            moon.moonPass += 1;
            if (type == 4 && effect == 6 && !florest.notEnemy && !moon.noOnePLays)
            {
                manager.turn += 2;
                manager.canPlay = true;
            }
            else
            {
                manager.turn += 1;
                manager.canPlay = true;
            }
            

        }
        if (sr.sortingOrder <= play.layer - 3 && playedCard)
        {
            playedCard = false;
            update = false;
            transform.position = start.position;
            enemy.pileCenter.Remove(this);
            enemy.deck.Add(this);
        }
    }
    public void Play()
    {
        Debug.Log("joguei");
        SFXManagement.Instance.PlaySoundFXClip(playClip, transform, 1f);
        placedTurn = manager.turn; 
        playedCard = true;
        sr.sortingOrder = play.layer;
        play.type = type;
        play.color = color;
        play.layer += 1;
    }
}
