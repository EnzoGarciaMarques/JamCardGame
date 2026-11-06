using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] Transform go;
    [SerializeField] float velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, go.position, velocity * Time.deltaTime);
        if(transform.position == go.position)
        {
            SceneManager.LoadScene("Toy Store");
        }
    }
}
