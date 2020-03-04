using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    public GameObject playerObj;
    public LayerMask toHit;

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
    Vector3 thirtyDegRight;
    Vector3 sixtyDegRight;
    Vector3 thirtyDegLeft;
    Vector3 sixtyDegLeft;

    bool inAnimation = false;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>().gameObject;
        rotX = transform.localRotation.eulerAngles.x;
        rotX = transform.localRotation.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        DrawLines();
        RotateCameraWithInput();
        CollisionAvoidance();
    }

    private void LateUpdate()
    {
        if (!inAnimation)
            UpdateCameraPosition();
    }

    void DrawLines()
    {
        Vector3 forwardVector = cam.transform.position - playerObj.transform.position;
        Vector3 rightVector = Vector3.Cross(forwardVector.normalized, Vector3.up) * forwardVector.magnitude;

        thirtyDegRight = playerObj.transform.position + rightVector * Mathf.Sin(30 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(30 * Mathf.Deg2Rad);
        sixtyDegRight = playerObj.transform.position + rightVector * Mathf.Sin(60 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(60 * Mathf.Deg2Rad);
        thirtyDegLeft = playerObj.transform.position - rightVector * Mathf.Sin(30 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(30 * Mathf.Deg2Rad);
        sixtyDegLeft = playerObj.transform.position - rightVector * Mathf.Sin(60 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(60 * Mathf.Deg2Rad);


        //Linetraces
        Debug.DrawLine(playerObj.transform.position, cam.transform.position);
        Debug.DrawLine(playerObj.transform.position, thirtyDegLeft);
        Debug.DrawLine(playerObj.transform.position, thirtyDegRight);
        Debug.DrawLine(playerObj.transform.position, sixtyDegLeft);
        Debug.DrawLine(playerObj.transform.position, sixtyDegRight);



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

    void CollisionAvoidance()
    {
        RaycastHit hit;
        float step = camMovespeed * Time.deltaTime;

        if (Physics.Linecast(playerObj.transform.position, cam.transform.position, out hit, toHit))
        {
            //Straight line from player to camera
            Debug.Log("HIT POS: " + hit.collider.gameObject.transform.position);
            Debug.Log("SIXTY POS: " + sixtyDegLeft);
            hit.collider.gameObject.transform.position += new Vector3(5, 5, 5);
        }
        else if (Physics.Linecast(playerObj.transform.position, thirtyDegLeft, out hit, toHit))
        {
            //30 deg left from player to cam
            //rotate 30 deg right to avoid obstacle
            if (!inAnimation)
            {
                inAnimation = true;
                //Check when we reach location
                //Turn off inAnimation
                //LookAt - Player
                //Undersök DollyDir i CameraCollision.
                Vector3 newPos = thirtyDegRight;
                Vector3.Lerp(cam.transform.position, newPos, step);
            }
        }
        else if (Physics.Linecast(playerObj.transform.position, sixtyDegLeft, out hit, toHit))
        {
            //60 deg left from player to cam
            //rotate 30 deg right to avoid obstacle
            Vector3 newPos = thirtyDegRight;
            transform.position = Vector3.Lerp(cam.transform.position, newPos, step);
        }
        else if (Physics.Linecast(playerObj.transform.position, thirtyDegRight, out hit, toHit))
        {
            //30 deg right from player to cam
            //rotate 30 deg to avoid
            Vector3 newPos = thirtyDegLeft;
            Vector3.Lerp(cam.transform.position, newPos, step);
        }
        else if (Physics.Linecast(playerObj.transform.position, sixtyDegRight, out hit, toHit))
        {
            //60 deg right from player to cam
            //rotate 60 deg to avoid
            // PUT IN LOGIC TO MOVE CAMERA CORRECTLY
            Vector3 newPos = thirtyDegLeft;
            Vector3.Lerp(cam.transform.position, newPos, step);
        }
    }
}
