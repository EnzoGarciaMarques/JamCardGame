using Unity.XR.Oculus.Input;
using UnityEngine;

public class GeralManager : MonoBehaviour
{
    public GeralManager instance;
    public bool capelobo;
    public bool curupira;
    public bool boitata;

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
