using UnityEngine;

public class SunCards : MonoBehaviour
{
    public GameManager manager;
    public PlayedCard play;
    public int roundsPass;
    public int roundsEnd;
    public Cards card;
    int round;
    public static SunCards instance;
    public Sanity san;
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
    }
    public void Ability(int a)
    {
        if (a == 1)
        {
            manager.roundPlus = 2;
            roundsPass = 0;
            roundsEnd = 1;
        }
        if (a == 2)
        {
            manager.roundsMax += 1;
        }
        if (a == 3)
        {
            manager.roundPlus = 2;
            roundsPass = 0;
            roundsEnd = 2;
        }
        if (a == 4)
        {
            manager.roundsMax += 3;
        }
        if (a == 5)
        {
            manager.roundPlus = 1;
            san.Insanity(10);
        }
        if (a == 6)
        {
            manager.roundPlus = 1;
            san.Insanity(-10);
        }
        if (a == 7)
        {
            manager.roundPlus = 2;
            roundsPass = 0;
            roundsEnd = (60 - san.sanity)/10;
        }
        if (a == 8)
        {
            manager.roundPlus = 1;
        }
        if (a == 9)
        {
            manager.roundsMax += 4;
        }
    }
    private void Update()
    {
        if (roundsPass == roundsEnd)
        {
            manager.roundPlus = 1;
        }
        if(roundsEnd <= 0)
        {
            roundsEnd = 0;
        }
    }
}
