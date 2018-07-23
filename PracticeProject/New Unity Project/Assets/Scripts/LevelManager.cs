using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("The GameObject that contains the animator to fade out.")]
    [SerializeField]
    private GameObject faderManager = null;

    [Tooltip("Do you want to automaticaly change the level after a defined time in seconds?")]
    [SerializeField]
    private bool autoLoadNextLevelEnabled = false;

    [Tooltip("The remaining time in seconds before switching to the next scene.")]
    public float autoLoadNextLevelAfter;

    private const string LOAD_NEXT_LEVEL_NAME = "LoadNextLevel";

    private Animator faderAnimator;

    private void Awake()
    {
        faderAnimator = faderManager.GetComponentInChildren<Animator>();
        if (faderAnimator == null)
        {
            Debug.Log("FaderAnimator was not found! Please fix this!");
        }
    }

    private void Start()
    {
        if (autoLoadNextLevelEnabled)
        {
            Invoke(LOAD_NEXT_LEVEL_NAME, autoLoadNextLevelAfter);
        }
    }

    public void LoadLevel(string name)
    {
        Debug.Log("New level load: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitReset()
    {
        Debug.Log("Quit requested.");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        faderAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
