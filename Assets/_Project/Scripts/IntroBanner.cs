using System.Collections;
using UnityEngine;

public class IntroBanner : MonoBehaviour
{
    [Header("Display Timing")]
    public float displayDuration = 3.5f;

    void Start()
    {
        StartCoroutine(ShowAndHideBanner());
    }

    IEnumerator ShowAndHideBanner()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        gameObject.SetActive(false);
    }
}