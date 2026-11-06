using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteractPlayer : MonoBehaviour
{
    [SerializeField] GameObject docs1;
    [SerializeField] GameObject docs2;
    [SerializeField] GameObject docs3;
    [SerializeField] GameObject docs4;
    [SerializeField] GameObject docs5;
    [SerializeField] GameObject deckObject;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] TextMeshProUGUI txt2;
    [SerializeField]bool tutorial;
    
    bool deck;
    bool secret;
    bool fim;
    [SerializeField] string level;
    bool next;
    int activeDocs;
    InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player2D.interact.performed += Docs;
        input.Player2D.rules.performed += Rules;
        input.Player2D.Enable();
    }
    private void OnDisable()
    {
        input.Player2D.interact.performed -= Docs;
        input.Player2D.rules.performed -= Rules;
        input.Player2D.Disable();
    }
    private void Update()
    {
        if(activeDocs == 0)
        {
            docs1.SetActive(false);
            docs2.SetActive(false);
            docs3.SetActive(false);
            docs4.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "docs1")
        {
            txt.gameObject.SetActive(true);
            activeDocs = 1;
        }
        else if (collision.tag == "docs2")
        {
            txt.gameObject.SetActive(true);
            activeDocs = 2;
        }
        else if (collision.tag == "docs3")
        {
            txt.gameObject.SetActive(true);
            activeDocs = 3;
        }
        else if (collision.tag == "docs4")
        {
            txt.gameObject.SetActive(true);
            activeDocs = 4;
        }
        else
        {
            activeDocs = 0;
        }

        if (collision.tag == "Card")
        {
            txt.gameObject.SetActive(true);
            deck = true;
        }
        else
        {
            deck = false;
        }

        if (collision.tag == "nextLevel")
        {
            txt.gameObject.SetActive(true);
            next = true;
        }
        else
        {
            next = false;
        }

        if (collision.tag == "Bode")
        {
            txt.gameObject.SetActive(true);
            secret = true;
        }
        else
        {
            secret = false;
        }

        if (collision.tag == "Fim")
        {
            txt.gameObject.SetActive(true);
            fim = true;
        }
        else
        {
            fim = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        txt.text = "Aperte E";
        if (collision.tag == "docs1")
        {
            txt.gameObject.SetActive(false);
            activeDocs = 0;
        }
        else if (collision.tag == "docs2")
        {
            txt.gameObject.SetActive(false);
            activeDocs = 0;
        }
        else if (collision.tag == "docs3")
        {
            txt.gameObject.SetActive(false);
            activeDocs = 0;
        }
        else if (collision.tag == "docs4")
        {
            txt.gameObject.SetActive(false);
            activeDocs = 0;
        }

        if (collision.tag == "Card")
        {
            txt.gameObject.SetActive(false);
            deck = false;
        }

        if (collision.tag == "nextLevel")
        {
            txt.gameObject.SetActive(false);
            next = false;
        }

        if (collision.tag == "Bode")
        {
            txt.gameObject.SetActive(false);
            secret = false;
        }

        if (collision.tag == "Fim")
        {
            txt.gameObject.SetActive(false);
            fim = false;
        }
    }
    private void Docs(InputAction.CallbackContext ctx)
    {
        if (activeDocs == 1)
        {
            docs1.SetActive(!docs1.activeSelf);
        }
        else if (activeDocs == 2)
        {
            docs2.SetActive(!docs2.activeSelf);
        }
        else if (activeDocs == 3)
        {
            docs3.SetActive(!docs3.activeSelf);
        }
        else if (activeDocs == 4)
        {
            docs4.SetActive(!docs4.activeSelf);
        }
        if (deck)
        {
            txt2.gameObject.SetActive(true);
            Destroy(deckObject);
            tutorial = true;
        }
        if (next && tutorial)
        {
            SceneManager.LoadScene(level);
        }
        if (next && !tutorial)
        {
            txt.text = "Pegue o baralho";
        }

        if (secret)
        {
            SceneManager.LoadScene("Cabra");
        }

        if (fim)
        {
            Application.Quit();
        }
    }
    private void Rules(InputAction.CallbackContext ctx)
    {
        if (tutorial) 
        {
            txt2.gameObject.SetActive(false);
            docs5.SetActive(!docs5.activeSelf);
        }
    }
}
