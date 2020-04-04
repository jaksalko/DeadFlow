using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Players : LivingEntity
{

    public float moveSpeed = 5;

    public Crosshairs crosshairs;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;

    public VariableJoystick lookJoystick;
    public VariableJoystick variableJoystick;

    protected override void Start()
    {
        base.Start();

        lookJoystick.Release += Handle_Release;//eventhandler '+='
        lookJoystick.Shoot += Handle_Shoot;

        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    void Handle_Release()
    {
        Debug.Log("released");
        gunController.OnTriggerHold();
        gunController.OnTriggerRelease();
    }
    void Handle_Shoot()
    {
        Debug.Log("hold");
        //gunController.OnTriggerHold();
    }

    void Update()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        controller.Move(direction);
        // Movement input
        /*Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);*/


        Vector3 look = Vector3.forward * lookJoystick.Vertical + Vector3.right * lookJoystick.Horizontal;
        controller.LookAt(look);

        
        // Look input
        /*Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);//input is mouse position --> joystick..?
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.GunHeight);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point,Color.red);
            controller.LookAt(point);
            crosshairs.transform.position = point;
            crosshairs.DetectTargets(ray);
            if((new Vector2(point.x, point.z) - new Vector2(transform.position.x,transform.position.z)).sqrMagnitude > 1)
            {
                gunController.Aim(point);
            }
           
        }*/



        // Weapon input
        /*if (Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.OnTriggerRelease();
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            gunController.Reload();
        }
    }
    public override void GetHeal(float percent)
    {
        base.GetHeal(percent);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "WeaponItem")
        {
            Debug.Log("trigger with weapon");
            int weapon_num = other.gameObject.GetComponent<WeaponItem>().num;
            gunController.EquipGun(gunController.guns[weapon_num]);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "PotionItem")
        {
            Debug.Log("trigger with potion");
            float recovery_percent = other.gameObject.GetComponent<PotionItem>().recovery_percent;
            GetHeal(recovery_percent);
            Destroy(other.gameObject);



        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "WeaponItem")
        {
            Debug.Log("collision");
           

        }
        else if(collision.gameObject.tag == "Potion")
        {

        }
        else if(collision.gameObject.tag == "Gold")
        {

        }
    }
}