using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : BoxEntity
{

    public ParticleSystem brokenEffect;
    //public WeaponItem[] weaponItem;
    //public PotionItem[] potionItem;

    public GameObject[] dropitem;
    int itemNum;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //random select item
        //...
        itemNum = Random.Range(0, dropitem.Length); // itemnum include weapon and potion item

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        Debug.Log("health : " + health + "  damage = " + damage);
        if (damage >= health)
        {
            //Instantiate(brokenEffect.gameObject, hitPoint,Quaternion.Euler(new Vector3(90,0,0)));
            Destroy(Instantiate(brokenEffect.gameObject, gameObject.transform.position, Quaternion.Euler(new Vector3(90,0,0))) as GameObject, brokenEffect.startLifetime);
            Instantiate(dropitem[itemNum], gameObject.transform.position + new Vector3(0,1,0), Quaternion.identity);// not weaponitem -> item
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }
}
