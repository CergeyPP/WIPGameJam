using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

internal class ForCorotine : MonoBehaviour
{
    [SerializeField] protected bool IsPlace = false;
    public static string namePan;
    public static bool IsTaim4;
    // private Trup trup;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerT"))
        {
           
            namePan = transform.GetChild(0).gameObject.name;
            IsTaim4 = true;
           // Debug.Log(namePan);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IsTaim4 = false;
    }



}
