using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : ItemEntity
{
    // Start is called before the first frame update

    public float recovery_percent = 0;
    float speed = 25f;
    float y;

    /*private void Awake()
    {
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }

    void OnNewWave(int next)
    {
        Destroy(gameObject);
    }*/

    private void Update()
    {
        y += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
    }

}
