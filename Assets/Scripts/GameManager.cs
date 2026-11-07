using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float timer;
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
    [SerializeField] GameObject blink;
    [SerializeField] GameObject txt;
    [SerializeField] GameObject cute;
    [SerializeField] string scene;
    public bool win;
    public Monster mon;
    public GameObject ripped;
    public GameObject ripped2;
    public GameObject back;
    public PlayedCard play;
    bool now;
    [SerializeField] AudioClip rippedClip;
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
        mon = FindAnyObjectByType<Monster>();
    }
    private void Update()
    {
        number.text = roundsMin + "/" + roundsMax;
        
        if(roundsMin >= roundsMax && !win && !mon.death)
        {
            win = true;
            StartCoroutine(Win());
        }
        if (turn % 2 == 0)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if(timer >= 7)
        {
            int type = your.pileCenter[^1].type;
            int color = your.pileCenter[^1].color;
            timer = 0;
            play.type = type;
            play.color = color;
            canPlay = true;
        }
    }

    public void Anima()
    {
        StartCoroutine(Ripped());
    }
    IEnumerator Win()
    {
        blink.SetActive(true);
        yield return new WaitForSeconds(1f);
        txt.SetActive(true);
        cute.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator Ripped()
    {
        back.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SFXManagement.Instance.PlaySoundFXClip(rippedClip, transform, 1f);
        back.SetActive(false);
        ripped.SetActive(true);
        ripped2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ripped.SetActive(false);
        ripped2.SetActive(false);
    }
}
