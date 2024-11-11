using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [SerializeField] private AudioSource source1;
    [SerializeField] private AudioSource source2;
    [SerializeField] private List<AudioClip> clipList;

    [SerializeField] private float source2_extraCoef = 0.35f;

    private float volume;

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

        volume = SetVolume();
    }

    private void Start()
    {
        source1.volume = volume;
        source1.loop = false;
        if(clipList != null)
        {
            if(clipList.Count > 0)
            {
                source1.clip = clipList[0];
                source1.Play();
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

        source2.volume = volume + source2_extraCoef;
        if (source2.volume > 1f)
            source2.volume = 1f;
        else if (source2.volume < 0f)
            source2.volume = 0f;
        source2.loop = true;
        source2.Play();

        StartCoroutine(MusicCycle());
        Debug.Log($"AudioManager volume: {volume}");
    }

    private void OnDisable()
    {
        StopCoroutine(MusicCycle());
        //Debug.Log("Disabled!");
    }

    private float SetVolume()
    {
        float temp;

        if (PlayerPrefs.HasKey("Volume"))
        {
            temp = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            temp = 0.85f; // default volume - not max, but close to it
        }

        return temp;
    }

    public void StopMusic()
    {
        StopCoroutine(MusicCycle());
        source1.Stop();
        source2.Stop();
    }

    public void PlayDeathSound()
    {
        StopMusic();
        source2.clip = clipList[2];
        source2.loop = false;
        if (volume >= 0.1f)
            source2.volume = volume + 0.45f;
        if (source2.volume > 1f)
            source2.volume = 1f;
        source2.Play();
    }

    // Method for non-AudioManager sources
    public float GetVolume(float coef = 0f)
    {
        float temp = source1.volume;

        if (temp < 0.15f)
            temp = 0;
        else
            temp += coef;

        return temp > 1 ? 1f : temp; // AudioSource volume range in Unity: 0f to 1f
    }

    private IEnumerator MusicCycle()
    {
        while(source1.isPlaying && source1.clip == clipList[0])
        {
            yield return null;
        }

        if(Time.timeScale >= 1)
        {
            source1.Stop();
            source1.clip = clipList[1];
            source1.loop = true;
            source1.Play();
        }
        
        yield return null;
    }
}
