using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [Min(0)] [SerializeField] float Speed;
    [Min(0)] [SerializeField] float JumpForce;
    [SerializeField] float gravity;

    Rigidbody m_rb;
    Animator m_anim;

    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponentInChildren<Animator>();

        Physics.gravity = Vector3.up * gravity;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Controls
        //Move
        if (Input.GetKey(KeyCode.A))
        {
            Move(Speed * -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(Speed);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void Move(float direction)
    {
        Vector2 target = new Vector2(transform.position.x + (direction * Time.deltaTime), transform.position.y);
        transform.LookAt(target);
        transform.position = target;
    }

    private void Jump()
    {
        m_rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        m_anim.SetTrigger("Jump");
        isGrounded = false;
    }
}
