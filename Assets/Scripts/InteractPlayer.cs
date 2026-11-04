using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractPlayer : MonoBehaviour
{
    [SerializeField] GameObject docs1;
    [SerializeField] GameObject docs2;
    [SerializeField] GameObject docs3;
    [SerializeField] GameObject docs4;
    [SerializeField] GameObject txt;
    bool tutorial;
    GameObject activeDocs;
    InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player2D.interact.performed += Docs;
        input.Player2D.Enable();
    }
    private void OnDisable()
    {
        input.Player2D.interact.performed -= Docs;
        input.Player2D.Disable();
    }
    private void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        txt.SetActive(true);
        if (collision.tag == "docs1")
        {
            activeDocs = docs1;
        }
        else if (collision.tag == "docs2")
        {
            activeDocs = docs2;
        }
        else if (collision.tag == "docs3")
        {
            activeDocs = docs3;
        }
        else if (collision.tag == "docs4")
        {
            activeDocs = docs4;
        }
        else
        {
            activeDocs = null;
        }

        if (collision.tag == "Card")
        {
            tutorial = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        txt.SetActive(false);
    }
    private void Docs(InputAction.CallbackContext ctx)
    {
        if (activeDocs != null)
        {
            activeDocs.SetActive(!activeDocs.activeSelf);

            if (activeDocs.activeSelf == true)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        if (tutorial)
        {
            SceneManager.LoadScene("0");
        }
    }
}
