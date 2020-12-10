using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MusicManager : MonoBehaviour
{
    public enum Track
    {
        OVERWORLD,
        BATTLE
    }

    private MusicManager() { }
    static MusicManager instance;
    public static MusicManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
            }
            return instance;
        }

        private set { }

    }


    [SerializeField]
    public AudioSource musicSource;
     
    [SerializeField]
    public AudioClip[] trackList;

    [SerializeField]
    AudioMixer musicMixer;

    [SerializeField]
    float volumeMin_db = -80.0f;

    [SerializeField]
    float volumeMax_db = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        EncounterManager encounterManager = SpawnPoint.player.GetComponent<EncounterManager>();
        encounterManager.onEnterEncounter.AddListener(OnEncounterHandler);
        encounterManager.onExitEncounter.AddListener(OnEncounterExitHandler);
        //
        //look for copies of Music manager
        //destroy it
        MusicManager[] musicManagers = FindObjectsOfType<MusicManager>();
        foreach(MusicManager mgr in musicManagers)
        {
            if(mgr != Instance)
            {
                Destroy(mgr.gameObject);
            }
        }


        FindObjectsOfType<MusicManager>();

        DontDestroyOnLoad(transform.root);
    }

    public void OnEncounterHandler()
    {
        PlayTrack(Track.BATTLE);
    }

    public void OnEncounterExitHandler()
    {
        PlayTrack(Track.OVERWORLD);
        FadeInTrackOverSeconds(Track.OVERWORLD, 2.0f);
    }


    public void PlayTrack(Track trackId)
    {
        musicSource.clip = trackList[(int)trackId];
        musicSource.Play();
    }


    public void FadeInTrackOverSeconds(Track trackId, float duration)
    {
        musicSource.volume = 0.0f;
        PlayTrack(trackId);
        StartCoroutine(FadeInTrackOverSecondsCoroutine(trackId, duration));
    }

    IEnumerator FadeInTrackOverSecondsCoroutine(Track trackId, float duration)
    {
        
        float timer = 0.0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;

            float noramlizedTime = timer / duration;

            musicSource.volume = Mathf.SmoothStep(0, 1, noramlizedTime);


            //fade volume
            yield return new WaitForEndOfFrame();
        }
    }



    public void SetMusicVolume(float volumeNormalized)
    {
        musicMixer.SetFloat("Music Volume", Mathf.Lerp(volumeMin_db,volumeMax_db, volumeNormalized));
    }

}
