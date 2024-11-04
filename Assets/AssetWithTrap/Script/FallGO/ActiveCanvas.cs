using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ActiveCanvas : MonoBehaviour
{
    public Gameflow gameflow;
    private void OnTriggerEnter(Collider coll)
    {
        gameflow.OnPlayerKilled();
    }
}
