using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turn = 1;
    public bool canPlay;
    public int roundsMax;
    public int roundsMin;
    public int roundPlus = 1;
    [SerializeField] TextMeshProUGUI number;
    public static GameManager instance;
    public SunCards sun;
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

        sun = FindAnyObjectByType<SunCards>();
    }
    private void Update()
    {
        number.text = roundsMin + "/" + roundsMax;
        if (turn > 2)
        {
            roundsMin += roundPlus;
            turn = 1;
            if (sun.roundsPass == 0)
            {
                sun.roundsPass = 1;
            }
        }
    }

}
