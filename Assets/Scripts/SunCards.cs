using UnityEngine;

public class SunCards : MonoBehaviour
{
    public GameManager manager;
    public PlayedCard play;
    public int roundsPass;
    public Cards card;
    int round;
    public static SunCards instance;
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
    public void Hability(int a)
    {
        if (a == 1)
        {
            manager.roundPlus = 2;
        }
    }
}
