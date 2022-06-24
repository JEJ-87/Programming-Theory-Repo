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
    AudioSource m_Audio;

    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponentInChildren<Animator>();
        m_Audio = GetComponent<AudioSource>();

        Physics.gravity = Vector3.up * gravity;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Controls
        //Move
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Move(Speed * -1);
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Move(Speed);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            StopMoving();
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            StopMoving();
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            m_anim.SetBool("isGrounded", true);
        }
    }

    private void Move(float direction)
    {
        Vector2 target = new Vector2(transform.position.x + (direction * Time.deltaTime), transform.position.y);
        transform.LookAt(target);
        transform.position = target;
        m_anim.SetBool("isRunning", true);
    }

    void StopMoving()
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
        transform.LookAt(target);

        m_anim.SetBool("isRunning", false);
    }

    private void Jump()
    {
        m_Audio.Play();
        m_rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        m_anim.SetTrigger("Jump");
        m_anim.SetBool("isGrounded", false);
        isGrounded = false;
    }
}
