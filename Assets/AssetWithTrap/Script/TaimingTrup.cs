using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

internal class TaimingTrup : MonoBehaviour
{
    private bool IsTaim = true;
    private bool IsTaim2 = true;
    private static float randomPlatforma;
    public static string namePlatform;
    [SerializeField] private TMP_Text text;
    private static float Time = 1f;
    private void RandomTrapActive()
    {
        if(Taimer.sec == 15 && IsTaim)
        {
            IsTaim = false;
            Debug.Log("Hard");
            StartCoroutine(TaimForTrap());
        }
        else if (Taimer.sec == 30 && IsTaim2)
        {
            IsTaim2 = false;
            Debug.Log("Hard2");
            Time = 0.8f;
        }
        else if (Taimer.sec == 40)
        {
            StartCoroutine(TaimForTrap2());
            Debug.Log(Time);
        }
    }
    private IEnumerator TaimForTrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time);
            randomPlatforma = Random.Range(0, 4);
            yield return new WaitForSeconds(Time);
            namePlatform = $"pan{randomPlatforma}";
            Debug.Log(namePlatform);
            yield return new WaitForSeconds(Time);
        }
    }
    private IEnumerator TaimForTrap2()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time);
            randomPlatforma = Random.Range(0, 4);
            yield return new WaitForSeconds(Time);
            namePlatform = $"pan{randomPlatforma}";
            Debug.Log(namePlatform);
            yield return new WaitForSeconds(Time);
        }
    }
    private void Update()
    {
        RandomTrapActive();
    }
}
