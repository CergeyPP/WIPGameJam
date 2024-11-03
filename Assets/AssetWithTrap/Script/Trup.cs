using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Trup : MonoBehaviour
{
    private bool IsTrup = true;
    public static bool IsTrupWork = false;
    private bool IsRunColor = false;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerT") && IsTrup)
        {
            IsTrup = false;
            Debug.Log("True");
            StartCoroutine(TaimForTrap());
        }
    } 
    private void RandomTrap()
    {
        if(TaimingTrup.namePlatform == gameObject.name)
        {
            StartCoroutine(TaimForTrap());
        }
    }
    private void Update()
    {
        RandomTrap();   
    }
    private void OnColliderEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerT") && IsTrup)
        {
            IsTrup = false;
            Debug.Log("True");
            StartCoroutine(TaimForTrap());
        }
    }
    private IEnumerator TaimForTrap()
    {
        animator.SetTrigger("DefaultTrigger");
        yield return new WaitForSeconds(2);
        animator.SetBool("Warning", true);
        yield return new WaitForSeconds(1);
        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = true;
        animator.SetBool("Warning", false);
        animator.SetBool("Danger", true);
        yield return new WaitForSeconds(1);
        transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
        animator.SetBool("Danger", false);
        animator.SetBool("Default Color",true);
        animator.SetTrigger("DefaultTrigger");
        IsTrup = true;
    }
}

