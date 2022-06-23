using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            other.GetComponent<Health>().takeDamage(damage);
    }
}
