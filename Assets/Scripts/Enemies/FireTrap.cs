using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRen;

    private bool triggered; //trap is touched
    private bool active; //trap can hurt player 


    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRen = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(activateFiretrap());
            }
            if (active)
                other.GetComponent<Health>().takeDamage(damage);
        }
    }

    private IEnumerator activateFiretrap()
    {
        triggered = true;
        spriteRen.color = Color.red; // Visual indication of trap being triggered
        yield return new WaitForSeconds(activationDelay);

        active = true;
        spriteRen.color = Color.white; // Visual indication off
        anim.SetBool("Activated", true);
        yield return new WaitForSeconds(activeTime);

        active = false;
        triggered = false;
        anim.SetBool("Activated", false);
    }

}
