using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    
    public LivingEntity target;
    Slider slider;
    public Image fill;
    // Start is called before the first frame update
    void Start()
    {
        if(target.tag == "Player")
        {
            fill.color = Color.green;
        }
        else if(target.tag == "Enemy")
        {
            fill.color = Color.red;
        }
        transform.position = target.transform.position + new Vector3(0,0,0.8f);
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = target.transform.position + new Vector3(0, 0, 0.8f);
            //Update_HP();
        
            
        

    }

    public void Update_HP()
    {
        slider.value = target.health / target.startingHealth;

        if (target.health <= 0)
            Destroy(this.gameObject);
    }
}
