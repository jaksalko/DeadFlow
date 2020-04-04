using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LivingEntity : MonoBehaviour, IDamageable
{

    public float startingHealth;
    public float health;//was protected...
    protected bool dead;

    public event System.Action OnDeath;

    //UI
    
    public HealthBar slider;

    private GameController instance;

    protected virtual void Start()
    {
        health = startingHealth;
        instance = GameController.instance;

        slider = Instantiate(GameController.UIController.hpslider);
       
        
        slider.target = this;
        slider.transform.SetParent(GameObject.Find("Canvas_HP").transform);
       
    }
    public virtual void GetHeal(float recovery_percent)
    {
        if (health + startingHealth * recovery_percent <= startingHealth)
        {
            health += startingHealth * recovery_percent;
        }

        else if (health + startingHealth * recovery_percent > startingHealth)
        {
            health = startingHealth;
        }

        slider.Update_HP();
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // Do some stuff here with hit var
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        slider.Update_HP();

      
        if (health <= 0 && !dead)
        {
            //Destroy(slider.gameObject);
            Die();
        }
    }

    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
        
    }
}