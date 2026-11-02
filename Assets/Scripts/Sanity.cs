using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    public int sanity = 100;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI amount;
    [SerializeField] RawImage blink;
    bool blinked = false;
    public static Sanity instance;
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
    private void Update()
    {
        slider.value = sanity;
        amount.text = sanity + "%";
        if (sanity <= 50 && !blinked)
        {
            StartCoroutine(Blink());
        }
    }
    public void Insanity(int a)
    {
        sanity -= a;
    }
    IEnumerator Blink()
    {
        blink.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        blink.gameObject.SetActive(false);
        blinked = true;
    }
}
