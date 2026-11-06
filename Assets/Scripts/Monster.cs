using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject phase1;
    [SerializeField] GameObject phase2;
    [SerializeField] GameObject phase3;
    [SerializeField] GameObject phase4;
    [SerializeField] GameObject blink;
    public bool death = false;
    public Sanity san;
    public GameManager manager;

    private void Awake()
    {
        san = FindAnyObjectByType<Sanity>();
        manager = FindAnyObjectByType<GameManager>();
    }
    private void Update()
    {
        if (san.sanity <= 50)
        {
            phase1.SetActive(false);
            phase2.SetActive(true);
        }
        if (san.sanity <= 0 && !death && !manager.win)
        {
            death = true;
            phase1.SetActive(false);
            phase2.SetActive(false);
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        blink.SetActive(true);
        yield return new WaitForSeconds(1f);
        phase3.SetActive(true);
        yield return new WaitForSeconds(2f);
        phase3.SetActive(false);
        phase4.SetActive(true);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
