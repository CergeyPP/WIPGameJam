using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ActiveGameOver : MonoBehaviour
{
   // [SerializeField] private GameObject player;
    [SerializeField] private GameObject GO;

    private void OnTriggerStay(Collider other)
    {
        GO.SetActive(true);
        Time.timeScale = 0;
    }

    //private IEnumerator StayTrap()
    //{
    //    yield return null;
    //    if (IsExit)
    //    {
           
    //    }
    //    else if (!IsExit)
    //    {
    //        IsStay = true;
    //    }
    //}
}
