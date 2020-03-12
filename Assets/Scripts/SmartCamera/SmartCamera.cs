using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    public GameObject playerObj;

    public float currentZoom;
    public float camMovespeed = 100.0f;
    public float clampAngleMax = 90.0f;
    public float clampAngleMin = -90.0f;
    public float inputSens = 100.0f;

    float mouseX;
    float mouseY;
    float finalInputX;
    float finalInputY;
    float rotX;
    float rotY;

    GameObject cam;


    private void Start()
    {
        cam = GetComponentInChildren<Camera>().gameObject;
        rotX = transform.localRotation.eulerAngles.x;
        rotX = transform.localRotation.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCameraWithInput();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }


    void RotateCameraWithInput()
    {
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputY = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputY = inputY + mouseY;

        rotX += finalInputY * inputSens * Time.deltaTime;
        rotY += finalInputX * inputSens * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, clampAngleMin, clampAngleMax);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRotation;
    }

    void UpdateCameraPosition()
    {
        float step = camMovespeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, playerObj.transform.position, step);
    }

    //void CollisionAvoidance()
    //{
    //    RaycastHit hit;


    //    if (Physics.Linecast(playerObj.transform.position, cam.transform.position, out hit, toHit))
    //    {

    //    }
    //    else if (Physics.Linecast(playerObj.transform.position, thirtyDegLeft, out hit, toHit))
    //    {
    //        t = 0;
    //    }
    //    else if (Physics.Linecast(playerObj.transform.position, sixtyDegLeft, out hit, toHit))
    //    {
    //        //MOVE TO SEPERATE SCRIPT
    //        //TODO: figure out how to allow the player to override this avoidence system
    //        cam.transform.position = Vector3.Lerp(cam.transform.position, thirtyDegRight, Time.deltaTime * speed);
    //        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.LookRotation((transform.position - cam.transform.position)), Time.deltaTime * speed);
    //        t = 0;
    //    }
    //    else
    //    {

    //        t += Time.deltaTime;
    //        if (t > 0.5f)
    //        {
    //        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, initialLocalPos, Time.deltaTime * speed * 0.5f);
    //        cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, initialLocalRotation, Time.deltaTime * speed * 0.5f);
    //        }
    //    }
    //    //else if (Physics.Linecast(playerObj.transform.position, thirtyDegRight, out hit, toHit))
    //    //{
    //    //    //30 deg right from player to cam
    //    //    //rotate 30 deg to avoid
    //    //    Vector3 newPos = thirtyDegLeft;
    //    //    cam.transform.position = newPos; //Vector3.Lerp(cam.transform.position, newPos, step);
    //    //}
    //    //else if (Physics.Linecast(playerObj.transform.position, sixtyDegRight, out hit, toHit))
    //    //{
    //    //    //60 deg right from player to cam
    //    //    //rotate 60 deg to avoid
    //    //    // PUT IN LOGIC TO MOVE CAMERA CORRECTLY
    //    //    Vector3 newPos = thirtyDegLeft;
    //    //    cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, step);
    //    //}
    //}
}
