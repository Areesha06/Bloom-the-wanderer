using System.Collections;
using UnityEngine;

public class IntroBanner : MonoBehaviour
{
    [Header("Display Timing")]
    public float displayDuration = 3.5f; // Time in seconds the message stays visible

    void Start()
    {
        StartCoroutine(ShowAndHideBanner());
    }

    IEnumerator ShowAndHideBanner()
    {
        // 1. Make sure the text is visible
        gameObject.SetActive(true);

        // 2. Wait for the player to read it
        yield return new WaitForSeconds(displayDuration);

        // 3. Hide the text
        gameObject.SetActive(false);
    }
}