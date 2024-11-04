using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [SerializeField] private AudioSource source;
    [SerializeField, Range(0f, 1f)] private float volume;
    [SerializeField] private List<AudioClip> clipList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        source.volume = volume;
        source.loop = false;
        if(clipList != null)
        {
            if(clipList.Count > 0)
            {
                source.clip = clipList[0];
                source.Play();
            }
            else
            {
                Debug.LogError("CLIP LIST IS EMPTY!");
            }
        }
        else
        {
            Debug.LogError("CLIP LIST IS NULL!");
        }

        StartCoroutine(MusicCycle());
    }

    private void OnDisable()
    {
        StopCoroutine(MusicCycle());
        //Debug.Log("Disabled!");
    }

    public void StopMusic()
    {
        StopCoroutine(MusicCycle());
        source.Stop();
    }

    private IEnumerator MusicCycle()
    {
        while(source.isPlaying && source.clip == clipList[0])
        {
            yield return null;
        }

        if(Time.timeScale >= 1)
        {
            source.Stop();
            source.clip = clipList[1];
            source.loop = true;
            source.Play();
        }
        
        yield return null;
    }
}
