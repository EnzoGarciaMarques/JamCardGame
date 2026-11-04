using System;
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
    public int shield;
    public static Sanity instance;
    public LakeCards lake;
    public FlorestCards florest;
    public CameraShake cam;
    [SerializeField] float shakeTime;
    [SerializeField] float shakeIntensity;
    [SerializeField] Image changeColor;
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

        lake = FindAnyObjectByType<LakeCards>();
        cam = FindAnyObjectByType<CameraShake>();
        florest = FindAnyObjectByType<FlorestCards>();
    }
    private void Update()
    {
        slider.value = sanity;
        amount.text = sanity + "%";
        if (sanity > 100)
        {
            sanity = 100;
        }
        if (sanity <= 50 && !blinked)
        {
            StartCoroutine(Blink());
        }
        if (shield >= 1)
        {
            changeColor.material.color = Color.blue;
        }
        else
        {
            changeColor.material.color = Color.purple;
        }
    }
    public void Insanity(int a)
    {
        if (shield <= 0)
        {
            if (florest.invert)
            {
                a *= -1;
            }
            if (lake.multiplier)
            {
                sanity += a * 2;
            }
            else if (lake.division)
            {
                sanity += a / 2;
            }
            else
            {
                sanity += a;
            }
            if (a >= 0)
            {
                florest.lessInsanity = true;
            }
            else
            {
                cam.Shake(shakeTime, shakeIntensity);
            }
            florest.invert = false;
        }
        else
        {
            shield -= 1;
        }
    }
    IEnumerator Blink()
    {
        blink.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        blink.gameObject.SetActive(false);
        blinked = true;
    }
}
