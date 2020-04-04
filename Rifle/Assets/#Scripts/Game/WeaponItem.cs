using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : ItemEntity
{
    //List<Dictionary<string, object>> data;

    public int num = 0;
    float speed = 25f; // for rotation animations
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
        transform.rotation = Quaternion.Euler(new Vector3(0, y, 45f)); // weapon rotation animation in the smap
    }
    /*public float msBetweenShots = 100;
    //public float muzzleVelocity = 35;
    public int burstCount;
    public int projectilesPerMag;
    public float reloadTime = .3f;
    public int spawn;
    public int fireMode;// 0 Auto 1 Single 2 Burst


    // Start is called before the first frame update
    private void Start()
    {
        data = CSVReader.Read("Weapon");//Category , FireMode ,Spawn , BtwShot , BurstCount , Bullets , ReloadTime
        msBetweenShots = float.Parse(data[num]["BtwShot"].ToString());
        fireMode = int.Parse(data[num]["FireMode"].ToString());
        spawn = int.Parse(data[num]["Spawn"].ToString());
        burstCount = int.Parse(data[num]["BurstCount"].ToString());
        projectilesPerMag = int.Parse(data[num]["Bullets"].ToString());
        reloadTime = float.Parse(data[num]["ReloadTime"].ToString());
        
    }*/




}
