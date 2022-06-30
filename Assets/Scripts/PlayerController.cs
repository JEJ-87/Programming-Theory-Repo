using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PLayer General Settings")]
    [SerializeField] AudioClip[] playerClips;

    [Header("Player Health Settings")]
    [SerializeField][Min(0)] int hp;
    //ENCAPSULATION
    public int playerHealth
    {
        get
        {
            return hp;
        }
        private set {}
    }
    [SerializeField] float kbForce;
    [SerializeField][Min(0)] float ouchTime;
    [SerializeField][Min(0)] int iFrames;
    [SerializeField] Material playerMaterial;

    [Header("Player Movement Settings")]
    [SerializeField] float Speed;
    [SerializeField][Min(0)]float JumpForce;
    [SerializeField] float gravity;

    Rigidbody m_rb;
    Animator m_anim;
    AudioSource m_Audio;

    bool isGrounded = true;
    bool isOuching = false;
    bool isInvincible = false;

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
        if (!isOuching && !GameManager.instance.isGameOver)
        {
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

        if (playerHealth == 0 && !GameManager.instance.isGameOver)
        {
            GameManager.instance.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ground collision handling
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            m_anim.SetBool("isGrounded", true);
        }

        //Enemy collision handling
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            DamagePlayer(collision.transform.position);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //Enemy collision handling
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            DamagePlayer(collision.transform.position);
        }
    }


    //ABSTRACTION
    //Move in provided direction axis
    private void Move(float direction)
    {
        Vector2 target = new Vector2(transform.position.x + (direction * Time.deltaTime), transform.position.y);
        transform.LookAt(target);
        transform.position = target;
        m_anim.SetBool("isRunning", true);
    }

    //Reset rotation and anim to default
    void StopMoving()
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);
        transform.LookAt(target);

        m_anim.SetBool("isRunning", false);
    }

    //Do a jump
    private void Jump()
    {
        m_Audio.PlayOneShot(playerClips[0]);
        m_rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        m_anim.SetTrigger("Jump");
        m_anim.SetBool("isGrounded", false);
        isGrounded = false;
    }

    //Apply damage to player
    private void DamagePlayer(Vector3 enemyPos)
    {
        StartCoroutine(StopMove());
        StartCoroutine(I_Frames());

        Vector3 dir = enemyPos - transform.position;
        dir.Normalize();
        m_rb.AddForce(-dir.x * kbForce, kbForce, 0, ForceMode.Impulse);

        hp--;
        m_Audio.PlayOneShot(playerClips[1]);
    }

    //Force player to stop moving
    IEnumerator StopMove()
    {
        m_anim.SetBool("isRunning", false);
        m_anim.SetTrigger("Ouch");
        isGrounded = false;
        isOuching = true;
        yield return new WaitForSeconds(ouchTime);
        isOuching = false;
    }

    //Player invulnerability period
    IEnumerator I_Frames()
    {
        isInvincible = true;
        int flashCount = 0;
        while (flashCount < iFrames)
        {
            yield return new WaitForSeconds(0.1f);
            playerMaterial.color = new Color(1, 1,1);
            yield return new WaitForSeconds(0.1f);
            playerMaterial.color = new Color(0, 0.8392158f, 1);
            flashCount++;
        }
        isInvincible = false;
    }
}
