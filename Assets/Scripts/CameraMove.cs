using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform cam;

    private void Update()
    {
        transform.position = cam.position;
    }
}
