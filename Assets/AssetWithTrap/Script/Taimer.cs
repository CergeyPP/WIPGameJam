using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Taimer : MonoBehaviour
{
    public static int sec = 0;
    private protected int milisec = 0;
    private protected int min = 0;
    private protected TMP_Text TimerText;
    private int delta = 1;
    private void Start()
    {
        TaimStart();
    }
    private void TaimStart()
    {
        StartTime();
    }
    private protected IEnumerator TimerCoroutine()
    {
        if (TimerText != null)
        {
            while (true)
            {

                if (milisec == 59)
                {
                    sec++;
                    milisec = -1;
                }
                if (sec == 59)
                {
                    min++;
                    sec = -1;
                }

                milisec += delta;
                TimerText.text = min.ToString("D2") + ":" + sec.ToString("D2") + ":" + milisec.ToString("D2");
                //  TimerText.text = min.ToString("D2") +" минут" + " : " + sec.ToString("D2") + " секунд";
                yield return new WaitForSeconds(0.01f);

            }
        }
    }
    private void StartTime()
    {
        TimerText = GameObject.Find("Taimer").GetComponent<TMP_Text>();
        StartCoroutine(TimerCoroutine());
    }
}
