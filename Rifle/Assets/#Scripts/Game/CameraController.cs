using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Camera cam;
    Transform transform_cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        transform_cam = cam.transform;

    }

    // Update is called once per frame
    void Update()
    {
        //follow target transform position...
        if(GameController.instance.player != null)
            FollowCam();
    }

    void FollowCam()
    {
        Vector3 position_cam = new Vector3(target.position.x, 20, target.position.z);
        transform_cam.position = position_cam;

        //camera.fieldOfView = 10;

    }
}
