using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ActiveGameOver : MonoBehaviour
{
   // [SerializeField] private GameObject player;
    [SerializeField] private GameObject GO;
    private bool IsStay = true;
    private bool IsExit = true;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerT") && IsStay)
        {
            IsStay = false;
            StartCoroutine(StayTrap());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IsExit = false;
    }
    private void OnTriggerEnter(Collider other)
    {
            IsExit = true;
    }
    private IEnumerator StayTrap()
    {
        yield return new WaitForSeconds(0.2f);
        GO.SetActive(true);
        Time.timeScale = 0;
        IsStay = true;
    }
}
