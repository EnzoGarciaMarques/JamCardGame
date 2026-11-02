using System;
using System.Linq;
using TMPro;
using UnityEngine;
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

    private void Start()
    {
        your = FindAnyObjectByType<YourCards>();
        play = FindAnyObjectByType<PlayedCard>();
        sun = FindAnyObjectByType<SunCards>();
        lake = FindAnyObjectByType<LakeCards>();
        //sun = FindAnyObjectByType<SunCards>();
        //sun = FindAnyObjectByType<SunCards>();
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
            manager.canPlay = true;
            if (type == 1)
            {
                sun.Ability(effect);
            }
        }
    }
    private void OnMouseDown()
    {
        if (play.type == type || play.color == color || play.type == 0 && !playedCard)
        {
            if (manager.turn % 2 != 0 && manager.canPlay == true)
            {
                manager.canPlay = false;
                manager.turn += 1;
                playedCard = true;
                lake.notInHand += 1;
                sr.sortingOrder = play.layer;
                play.type = type;
                play.color = color;
                your.pileCenter.Add(this);
                your.available[handIndex] = true;
                play.layer += 1;
            }
        }
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
