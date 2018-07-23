using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerNotifier : MonoBehaviour
{
    [Tooltip("The game object that contains the LevelManagerScript to notify.")]
    [SerializeField]
    private GameObject levelManagerGameObjectToNotify = null;

    private LevelManager levelManagerScript;

    private void Awake()
    {
        levelManagerScript = levelManagerGameObjectToNotify.GetComponentInChildren<LevelManager>();

        if (levelManagerScript == null)
        {
            Debug.Log("LevelManager not found inside the GameObject serialized! Please fix this!");
        }
    }

    public void NotifyLevelManager()
    {
        levelManagerScript.OnFadeComplete();
    }
}
