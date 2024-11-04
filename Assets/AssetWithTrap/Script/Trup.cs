using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

internal class Trup : MonoBehaviour
{
    [SerializeField] private PanIndicator _colorIndicator;
    private bool IsTrap = true;
    private bool IsTrap2 = true;
    private bool IsTrap3 = true;
    private bool IsTrap4 = false;

    private void RandomTrap()
    {
        if(TaimingTrup.namePlatformM == gameObject.name || TaimingTrup.namePlatform2M == gameObject.name)
        {
            StopCoroutine(TaimForTrap());
                if (IsTrap3)
                {
                    StartCoroutine(TaimForTrap());
                } 
        }
    }
    private void Update()
    {
        RandomTrap();   
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerT") && IsTrap)
        {
            IsTrap = false;
            IsTrap4 = true;

            StopAllCoroutines();
            if (IsTrap4)
            {
                StartCoroutine(TaimForTrap());
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IsTrap4 = false;
        StopCoroutine(TaimForTrap());
    }

    private IEnumerator TaimForTrap()
    {
        // animator.SetTrigger("DefaultTrigger");
        IsTrap3 = false;
        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;

        _colorIndicator.PlayWarningTransition();
        yield return new WaitForSeconds(TaimingTrup.Time);
        _colorIndicator.PlayDangerTransition();
        yield return new WaitForSeconds(_colorIndicator.DangerTransitionTime);
        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = true;
        transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
        transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = false;
        _colorIndicator.PlayDefaultTransition();
        if(IsTrap4)
        {
           
            IsTrap = true;
            IsTrap3 = false;
        }
        else if (!IsTrap4)
        {
            IsTrap = true;
            IsTrap3 = true;
        }
        
        

    }
}

