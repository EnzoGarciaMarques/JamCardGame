using UnityEngine;

public class CardsEnemy : MonoBehaviour
{
    public int type;
    public int color;
    [SerializeField] bool playedCard = false;
    [SerializeField] float velocity;
    [SerializeField] Transform playPos;
    private bool update = false;
    public PlayedCard play;
    public GameManager manager;
    SpriteRenderer sr;

    private void Start()
    {
        play = FindAnyObjectByType<PlayedCard>();
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
            update = false;
            manager.canPlay = true;
        }
    }
    public void Play()
    {
        manager.canPlay = false;
        manager.turn += 1;
        playedCard = true;
        sr.sortingOrder = play.layer;
        play.type = type;
        play.color = color;
        play.layer += 1;
    }
}
