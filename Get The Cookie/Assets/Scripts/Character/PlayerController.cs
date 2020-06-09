using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player player;
    public Transform center;
    Vector3 centerPoint;

    private void Start()
    {
        centerPoint = center.position;
        player.healthChanged += ChangedHealth;
    }

    private void Update()
    {
        player.Hp += 1;
    }

    void ChangedHealth(float health)
    {
        Debug.Log("health " +health);
    }

    void Walk()
    {

    }
}
