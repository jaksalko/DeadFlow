using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public delegate void PositionEvent(Vector3 vector);
    public event PositionEvent positionChanged;

    public delegate void HealthEvent(float myhealth);
    public event HealthEvent healthChanged;

    //Transform
    Vector3 position_;
    public Vector3 Position
    {
        get { return position_; }
        set
        {
            if (position_ != value)
            {
                position_ = value;
                positionChanged?.Invoke(position_);
            }
        }
    }

    //Health
    float hp;
    public float Hp
    {
        get { return hp; }
        set
        {
            if (hp != value)
            {
                hp = value;
                healthChanged?.Invoke(hp);
            }
        }
    }

}
