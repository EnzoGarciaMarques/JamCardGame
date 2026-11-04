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
    public LakeCards lake;
    public FlorestCards florest;
    public EnemyCards enemy;
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
        your = FindAnyObjectByType<YourCards>();
        enemy = FindAnyObjectByType<EnemyCards>();
        sun = FindAnyObjectByType<SunCards>();
        lake = FindAnyObjectByType<LakeCards>();
        florest = FindAnyObjectByType<FlorestCards>();
    }
    private void Update()
    {
        number.text = roundsMin + "/" + roundsMax;
        if (turn > 2)
        {
            roundsMin += roundPlus;
            turn = 1;
            florest.lessInsanity = false;
            if (sun.roundsPass == 0)
            {
                sun.roundsPass += 1;
            }
            if (lake.lakePass == 0)
            {
                lake.lakePass += 1;
            }
        }
    }

}
