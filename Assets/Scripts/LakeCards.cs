using UnityEngine;

public class LakeCards : MonoBehaviour
{
    public GameManager manager;
    public PlayedCard play;
    public int notInHand = 0;
    public int lakePass;
    public int roundsEnd;
    public Cards card;
    public bool multiplier;
    public bool division;
    int round;
    public static LakeCards instance;
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
            san.sanity += 10 * notInHand;
        }
        if (a == 2)
        {
            if (multiplier)
            {
                san.sanity -= (30 - notInHand * 5)*2;
            }
            else if (division)
            {
                san.sanity -= (30 - notInHand * 5)/2;
            }
            else
            {
                san.sanity -= 30 - notInHand * 5;
            }
        }
        if (a == 3)
        {
            san.sanity += 10;
        }
        if (a == 4)
        {
            if (multiplier)
            {
                san.sanity -= 10 * 2;
            }
            else if (division)
            {
                san.sanity -= 10 / 2;
            }
            else
            {
                san.sanity -= 10;
            }
        }
        if (a == 5)
        {
            san.sanity += 5;
        }
        if (a == 6)
        {
            if (multiplier)
            {
                san.sanity -= 5 * 2;
            }
            else if (division)
            {
                san.sanity -= 5 / 2;
            }
            else
            {
                san.sanity -= 5;
            }
        }  
        if (a == 7)
        {
            division = false;
            multiplier = true;
            lakePass = 0;
            roundsEnd = 2;
        }
        if (a == 8)
        {
            division = true;
            multiplier = false;
            lakePass = 0;
            roundsEnd = 2;
        }
    }
    private void Update()
    {
        if (lakePass == roundsEnd)
        {
            multiplier = false;
            division = false;
        }
    }
}
