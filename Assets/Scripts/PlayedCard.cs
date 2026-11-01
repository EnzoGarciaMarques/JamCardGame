using UnityEngine;

public class PlayedCard : MonoBehaviour
{
    public int type;
    public int color;
    public static PlayedCard instance;
    public int layer;
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
    private void OnMouseDown()
    {
        
    }
}
