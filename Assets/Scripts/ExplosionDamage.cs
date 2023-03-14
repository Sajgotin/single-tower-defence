using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy") collision.GetComponent<Enemy>().HurtEnemy();
    }

    //used in animation event
    void StopDamage()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
