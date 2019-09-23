using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Camera c;

    //// Start is called before the first frame update
    //CharacterController characterController;

    //public float speed = 6.0f;
    //public float jumpSpeed = 8.0f;
    //public float gravity = 20.0f;
    //private GameObject target = null;
    //private Vector3 offset;

    //private Vector3 moveDirection = Vector3.zero;

    //void Start()
    //{
    //    characterController = GetComponent<CharacterController>();
    //    target = null;
    //}

    //void Update()
    //{
    //    if (characterController.isGrounded)
    //    {
    //        // We are grounded, so recalculate
    //        // move direction directly from axes

    //        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    //        moveDirection *= speed;

    //        if (Input.GetButton("Jump"))
    //        {
    //            moveDirection.y = jumpSpeed;
    //        }
    //    }

    //    // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
    //    // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
    //    // as an acceleration (ms^-2)
    //    moveDirection.y -= gravity * Time.deltaTime;

    //    // Move the controller
    //    characterController.Move(moveDirection * Time.deltaTime);
    //}

    public float speed;
    private Rigidbody rb;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
        }

            Vector3 tempVect = new Vector3(h, 0, v);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + tempVect);
    }

    public void FixedUpdate()
    {
        
    }

    void Death()
    {
        transform.gameObject.SetActive(false);
        c.gameObject.SetActive(true);
    }

}
