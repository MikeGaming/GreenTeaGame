using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    float defaultMoveSpeed;

    [SerializeField] float groundDrag;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;

    [SerializeField] Transform orientation;

    float horInput;
    float verInput;
    bool crouchInput;

    Vector3 defaultScale;

    Vector3 moveDir;

    Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        defaultScale = transform.localScale;
        defaultMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        SpeedControl();

        if (grounded)
        {
            body.drag = groundDrag;
        }
        else
        {
            body.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void MyInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        verInput = Input.GetAxisRaw("Vertical");
        crouchInput = Input.GetKey(KeyCode.LeftControl);
    }

    void Move()
    {
        moveDir = (orientation.forward * verInput) + (orientation.right * horInput);

        body.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);

        if (crouchInput)
        {
            body.transform.localScale = defaultScale / 2f;
            moveSpeed = defaultMoveSpeed / 2f;
        }
        else
        {
            body.transform.localScale = defaultScale;
            moveSpeed = defaultMoveSpeed;
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(body.velocity.x, 0, body.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limVel = flatVel.normalized * moveSpeed;
            body.velocity = new Vector3(limVel.x, body.velocity.y, limVel.z);
        }
    }
}
