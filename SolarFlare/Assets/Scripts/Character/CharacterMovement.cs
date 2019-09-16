using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float rotateSpeed = 5f;
    public float JumpHeight = 8f;
    public float DashDistance = 10f;
    public Vector3 Drag = new Vector3(5f, 10f, 5f);
    public float gravityScale = 1f;

    private CharacterController m_controller;
    private Rigidbody m_rigidbody;
    private Animator m_animator;

    private Vector3 move;

    // Animator variables
    private bool m_jump;
    private bool m_dance;
    private float m_TurnAmount;
    private float m_ForwardAmount;
    private float inputForward;
    private float inputTurn;


    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();

        move = new Vector3(0f, 0f, 0f);

        //m_rigidbody = GetComponent<Rigidbody>();
        //m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis

        h = h * Mathf.Sqrt(1f - 0.5f * v * v);
        v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        inputForward = Mathf.Clamp(Mathf.Lerp(inputForward, v,
            Time.deltaTime * 5f), -5f, 5);

        inputTurn = Mathf.Lerp(inputTurn, h,
            Time.deltaTime * 5f);

        if (inputForward < 0f)
            inputTurn = -inputTurn;

        //Move();
        Jump();
        Dash();
        //Gravity();
        Rotate();
        Dance();

        // Apply move vector
        move.x /= 1 + Drag.x * Time.deltaTime;
        move.y /= 1 + Drag.y * Time.deltaTime;
        move.z /= 1 + Drag.z * Time.deltaTime;
        m_TurnAmount =inputTurn;
        m_ForwardAmount = inputForward;
        m_controller.Move(this.transform.forward * inputForward * Time.deltaTime * moveSpeed);

        m_controller.Move(move * Time.deltaTime);

        // send input and other state parameters to the animator
        UpdateAnimator();
    }

    // Calculate move
    private void Move()
    {
        //move = m_camera.transform.TransformDirection(move).normalized * moveSpeed;
    }

    // Apply rotation
    private void Rotate()
    {
        transform.Rotate(0f, inputTurn, 0f);
    }

    // Adds gravity to character controller
    private void Gravity()
    {
        move.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
    }

    // makes character jump if on ground and press space
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            move.y = JumpHeight;
            m_jump = true;
            StartCoroutine(FinishJump());
        }
    }

    IEnumerator FinishJump()
    {
        yield return new WaitForSeconds(0.5f);
        m_jump = false;
    }

    // makes character dash if press left alt
    private void Dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            move += Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0,
                                        (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));


        }
    }

    private void Dance()
    {
        if (Input.GetKeyDown("k"))
        {
            m_dance = !m_dance;
        }
    }

    private void UpdateAnimator()
    {
        m_animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_animator.SetBool("Jump", m_jump);
        m_animator.SetBool("Dance", m_dance);
    }
}
