using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float minZoom;
    public float maxZoom;
    public float currentZoom;
    public int zoomSpeed;

    private void Awake()
    {
        minZoom = GetComponent<CameraCollision>().minDist;
        maxZoom = GetComponent<CameraCollision>().maxDist;
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Zoom") != 0)
        {
            currentZoom += Input.GetAxisRaw("Zoom") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }
    }
}
