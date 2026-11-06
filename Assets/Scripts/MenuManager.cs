using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject anima;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    public void Play()
    {
        anima.SetActive(true);
        button1.SetActive(false);
        button2.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
