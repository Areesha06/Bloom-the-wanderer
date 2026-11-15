using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class StorySceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText; 

    [Header("Next Level Configuration")]
    [Tooltip("Exact name of the scene to load after the intro finishes")]
    public string firstLevelSceneName = "Game"; 

    [Header("Story Dialogue")]
    [TextArea(2, 4)]
    public string[] storyLines = new string[]
    {
        "Somewhere across the stars, the Little Prince woke up to find his Rose fading.",
        "Her petals had lost their glow, scattered across quiet, forgotten worlds.",
        "To help her bloom again, he has to journey through the dark and find each lost Heart Shard.",
        "Get past the King's guards, defeat the Evil King, and bring back the first piece of her heart."
    };

    private int currentIndex = 0;

    void Start()
    {
        if (storyLines.Length > 0)
        {
            currentIndex = 0;
            DisplayCurrentLine();
        }
        else
        {
            LoadFirstLevel();
        }
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null &&
            (keyboard.spaceKey.wasPressedThisFrame || keyboard.enterKey.wasPressedThisFrame))
        {
            OnNextClicked();
        }
    }

    public void OnNextClicked()
    {
        currentIndex++;

        if (currentIndex < storyLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            LoadFirstLevel();
        }
    }

    void DisplayCurrentLine()
    {
        if (dialogueText != null)
        {
            dialogueText.text = storyLines[currentIndex];
        }
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }
}