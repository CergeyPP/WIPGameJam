using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

internal class TaimingTrup : MonoBehaviour
{
    private bool IsTaim = true;
    private bool IsTaim2 = true;
    private bool IsTaim3 = true;
    private bool IsTaim4 = true;
    public static float randomPlatforma;
    public static float randomPlatforma2;
    public static string namePlatform;
    public static string namePlatformM;
    public static string namePlatform2;
    public static string namePlatform2M;
    [SerializeField] private TMP_Text text;
    public static float Time = 2f;
    private void RandomTrapActive()
    {
        if(Taimer.sec >= 12 && IsTaim)
        {
            IsTaim = false;
            Debug.Log("Hard");
            StartCoroutine(TaimForTrap());
        }
        else if (Taimer.sec >= 27 && IsTaim2)
        {
            IsTaim4 = false;
            IsTaim2 = false;
            Debug.Log("Hard2");
            
        }
        else if (Taimer.sec == 42 && IsTaim3 )
        {
            IsTaim3 = false;
            Time = 1.5f;
            Debug.Log("Hard3");
        }
    }
    private IEnumerator TaimForTrap()
    {
        while (true)
        {
            if (IsTaim4)
            {
                yield return new WaitForSeconds(Time);
                randomPlatforma = Random.Range(0, 4);
                namePlatform = $"pan{randomPlatforma}";
                if (namePlatform != ForCorotine.namePan)
                {
                    Debug.Log(ForCorotine.namePan);
                    namePlatformM = $"pan{randomPlatforma}";
                }

            }
           else if (!IsTaim4)
            {
                yield return new WaitForSeconds(Time);
                randomPlatforma = Random.Range(0, 2);
                randomPlatforma2 = Random.Range(2, 4);
                namePlatform = $"pan{randomPlatforma}"; 
                namePlatform2 = $"pan{randomPlatforma2}";
                if (namePlatform != ForCorotine.namePan)
                {
                    Debug.Log(ForCorotine.namePan);
                    namePlatformM = $"pan{randomPlatforma}";
                }
                if (namePlatform2 != ForCorotine.namePan)
                {
                    Debug.Log(ForCorotine.namePan);
                    namePlatform2M = $"pan{randomPlatforma2}";
                }
            }
               
            
        }
    }
    private void Update()
    {
        RandomTrapActive();
    }
}
