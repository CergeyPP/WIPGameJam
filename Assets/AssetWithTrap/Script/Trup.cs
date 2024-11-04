using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

internal class Trup : MonoBehaviour
{
    private bool IsTrap = true;
    private bool IsTrap2 = true;
    private bool IsTrap3 = true;
    private bool IsTrap4 = false;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
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
        yield return new WaitForSeconds(TaimingTrup.Time -0.5f);
        animator.SetBool("Warning", true);
        yield return new WaitForSeconds(TaimingTrup.Time -0.5f);
        animator.SetBool("Warning", false);
        animator.SetBool("Danger", true);
        yield return new WaitForSeconds(0.7f);
        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = true;
        transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
        transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = false;
        animator.SetBool("Danger", false);
        animator.SetBool("Default Color", true);
        animator.SetTrigger("DefaultTrigger");
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

