using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject phase1;
    [SerializeField] GameObject phase2;
    public Sanity san;

    private void Awake()
    {
        san = FindAnyObjectByType<Sanity>();
    }
    private void Update()
    {
        if (san.sanity <= 50)
        {
            phase1.SetActive(false);
            phase2.SetActive(true);
        }
    }
}
