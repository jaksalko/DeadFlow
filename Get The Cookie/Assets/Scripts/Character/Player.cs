using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : LivingEntity
{

    public delegate void StateEvent(Status state);
    public event StateEvent stateChanged;

    private void Awake()
    {
        Hp = 0;
    }

    public enum Status
    {
        Idle,//can attack , climb , move
        Move,// can move(move back)
        OnTower,//can move up , down
        InTower,//can move attack
        OnTop//can win

    }


    //State
    Status state;
    public Status State
    {
        get { return state;}
        set
        {
            if(state != value)
            {
                state = value;
                stateChanged?.Invoke(state);
            }
        }
    }



     
}
