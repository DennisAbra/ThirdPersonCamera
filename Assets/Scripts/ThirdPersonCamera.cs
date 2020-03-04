using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject camFollowObj;

    public float camMoveSpeed = 100.0f;
    public float clampAngleMax = 90.0f;
    public float clampAngleMin = -90.0f;
    public float inputSens = 100.0f;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputY;

    float rotX = 0.0f;
    float rotY = 0.0f;
    Transform target = null;

    private void Start()
    {
        camFollowObj = FindObjectOfType<Movement>().gameObject;
        Transform[] children = camFollowObj.GetComponentsInChildren<Transform>();
        foreach(Transform trans in children)
        {
            if(trans.tag == "CameraFollow")
            {
                camFollowObj = trans.gameObject;
                break;
            }
        }
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputY = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputY = inputY + mouseY;

        rotY += finalInputX * inputSens * Time.deltaTime;
        rotX += finalInputY * inputSens * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, clampAngleMin, clampAngleMax);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        target = camFollowObj.transform;

        float step = camMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
