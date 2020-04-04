using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Box is damageable but not livingEntitiy
public class BoxEntity : MonoBehaviour , IDamageable // Obstacle  but damageable ... if it is itembox , it drop item when it is broken....
{
    public float startingHealth;
    protected float health;
    protected bool broken;

    //public event System.Action OnDeath;
    protected virtual void Start()
    {
        health = startingHealth;
    }
    public void GetHeal(float percent)
    {
        //no heal box;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health + "  " + damage);
        if (health <= 0 && !broken)
        {
            Broken();
        }
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // Do some stuff here with hit var
        TakeDamage(damage);
    }

    public void Broken()
    {
        broken = true;
        /*if (OnDeath != null)
        {
            OnDeath();
        }*/

        // drop item?
        GameObject.Destroy(gameObject);
    }

    
}
