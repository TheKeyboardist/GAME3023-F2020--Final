using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScreenOverlayManager : MonoBehaviour
{
    [SerializeField]
    Animator canvasAnimator;

    private ScreenOverlayManager() { }

    private static ScreenOverlayManager instance;

    public static ScreenOverlayManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ScreenOverlayManager>();
            }
            return instance;
        }

        private set { }
    }


    void Start()
    {
        ScreenOverlayManager[] screenOverlayManagers = FindObjectsOfType<ScreenOverlayManager>();

        foreach(ScreenOverlayManager som in screenOverlayManagers)
        {
            if(som != Instance)
            {
                Destroy(som.gameObject);
            }
        }
        DontDestroyOnLoad(transform.root);


        //subscribe to encounter events
        var encounterManager = SpawnPoint.player.GetComponent<EncounterManager>();
        encounterManager.onEnterEncounter.AddListener(OnEnterCombat);
        encounterManager.onExitEncounter.AddListener(OnExitCombat);


        SceneManager.sceneLoaded += OnEnterNewScene; 
    }

    void OnEnterCombat()
    {
        canvasAnimator.Play("FadeToBlack");
    }


    void OnExitCombat()
    {
        canvasAnimator.Play("FadeToBlack");
    }


    void OnEnterNewScene(Scene newScene, LoadSceneMode node)
    {
        canvasAnimator.Play("FadeFromBlack");
    }
}
