using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;

    float horizontal;
    float vertical;
    public float speed;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
            MoveTo();
    }

    void MoveTo()
    {
        rigidBody.MovePosition(transform.position + new Vector3(horizontal * speed, 0, vertical * speed).normalized);
    }
}
