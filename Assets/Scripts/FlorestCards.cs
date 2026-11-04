using UnityEngine;

public class FlorestCards : MonoBehaviour
{
    public GameManager manager;
    public PlayedCard play;
    public Cards card;
    public bool notPlayer =  false;
    public bool notEnemy = false;
    public bool invert = false;
    public bool lessInsanity = false;
    public static FlorestCards instance;
    public Sanity san;
    public YourCards your;
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
        your = FindAnyObjectByType<YourCards>();
    }
    public void Ability(int a)
    {
        if (a == 1)
        {
            san.shield = 1;
        }
        if (a == 2)
        {
            san.shield = 0;
        }
        if (a == 3)
        {
            notEnemy = true;
        }
        if (a == 4)
        {
            notPlayer = true;
        }
        if (a == 6)
        {
            invert = true;
        }
        if (a == 7)
        {
            san.Insanity(5);
        }
        if (a == 8)
        {
            if (lessInsanity)
            {
                san.Insanity(20);
                lessInsanity = false;
            }
        }
        if (a == 9)
        {
            san.Insanity(-10);
            san.shield = 2;
        }
    }

}
