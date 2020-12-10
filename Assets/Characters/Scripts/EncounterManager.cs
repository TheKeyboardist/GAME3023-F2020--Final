using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class EncounterManager : MonoBehaviour
{
    public UnityEvent onEnterEncounter;
    public UnityEvent onExitEncounter;


    public void EnterEncounter()
    {
        StartCoroutine(DelayBattle());
    }

    IEnumerator DelayBattle()
    {
        onEnterEncounter.Invoke();

        yield return new WaitForSeconds(2.0f);
        transform.root.gameObject.SetActive(false);
        SceneManager.LoadScene("BattleScene");
        
    }

    void DelayedOverworld()
    {
        transform.root.gameObject.SetActive(true);  
        SceneManager.LoadScene("Overworld");

    }


    public void ExitEncounter()
    {
        onExitEncounter.Invoke();
        Invoke("DelayedOverworld", 2.0f);
    }
}
