using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ActiveCanvas : MonoBehaviour
{
    public GameObject GO;
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("PlayerT"))
        {
            Time.timeScale = 0;
            GO.SetActive(true);
        }
    }
}
