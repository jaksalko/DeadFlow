using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    List<Vector3> cameraPosition;
    [SerializeField]
    List<Vector3> cameraRotation;

    public void CameraSpot(int num)
    {
        mainCamera.transform.position = cameraPosition[num];
        mainCamera.transform.rotation = Quaternion.Euler(cameraRotation[num]);
    }
    
}
