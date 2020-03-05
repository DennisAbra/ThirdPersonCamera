using UnityEngine;


public class Movement : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float speed;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveVector = transform.right * horizontal + transform.forward * vertical;
        controller.Move(moveVector * speed * Time.deltaTime);
    }


}
