using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turn = 1;
    public bool canPlay;
    public static GameManager instance;
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

}
