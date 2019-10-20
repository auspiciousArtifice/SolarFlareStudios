using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public float DashDistance = 10f;
    public Vector3 Drag = new Vector3(5f, 0f, 5f);
    public AudioClip hitGroundAudio;
    private AudioSource playerAudio;

    private Camera mainCamera;

    private Rigidbody m_rigidbody;
    private Collider m_collider;
    private Animator m_animator;

    private Transform leftFoot;
    private Transform rightFoot;

    private Vector3 move;

    // Animator variables
    private bool m_jump;
    private bool m_dance;
    private bool m_dash;
	private bool m_sprint;
    private float inputForward = 0f;
    private float inputTurn = 0f;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;
    private float forwardSpeedLimit = 1f;

    //private int groundContactCount;

    private bool isGrounded;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        if (m_animator == null)
            Debug.Log("Animator could not be found");

        m_rigidbody = GetComponent<Rigidbody>();
        if (m_rigidbody == null)
            Debug.Log("Rigid body could not be found");


        playerAudio = GetComponent<AudioSource>();

        m_collider = GetComponent<Collider>();
        if (m_collider == null)
        {
            Debug.Log("Collider could not be found for player object.");
        }

        /*m_controller = GetComponent<CharacterController>();
        if (m_controller == null)
            Debug.Log("Character Controller could not be found");*/
    }

    void Start()
    {
        move = new Vector3(0f, 0f, 0f);

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera");
        }

        leftFoot = this.transform.Find("knight_d_pelegrini@T-Pose/Hips/LeftUpLeg/LeftLeg/LeftFoot");
        rightFoot = this.transform.Find("knight_d_pelegrini@T-Pose/Hips/RightUpLeg/RightLeg/RightFoot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");
    }

    void Update()
    {
        Move();
        Jump();
        Dash();
		Sprint();
        
        //Rotate();
        //Dance();

		// Apply move vector
		//m_controller.Move(this.transform.forward * inputForward * Time.deltaTime * moveSpeed);

		//m_rigidbody.AddForce(move * Time.deltaTime);

        // send input and other state parameters to the animator
        UpdateAnimator();
        m_jump = false;
        m_dash = false;
    }

    // Calculate move
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis

        if (isGrounded)
        {
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);

            inputForward = Mathf.Clamp(Mathf.Lerp(inputForward, v,
                Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);
        }
        else
        {
            m_rigidbody.AddForce(transform.forward * v * 10);
        }
        inputTurn = Mathf.Lerp(inputTurn, h, Time.deltaTime * turnInputFilter);
    }

	private void Sprint()
	{
		if (Input.GetButtonDown("Fire3"))
		{
			m_sprint = true;
		} 
		else if (Input.GetButtonUp("Fire3"))
		{
			m_sprint = false;
		}
	}

    // Apply rotation
    private void Rotate()
    {
        transform.Rotate(0f, inputTurn, 0f);
    }

    // makes character jump if on ground and press space
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //move.y = JumpHeight;
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

            m_rigidbody.AddForce(mainCamera.transform.forward * 100, ForceMode.Impulse);
            //m_rigidbody.MovePosition(Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0,
                                       //(Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime))));
            m_dash = true;

        }
        //move.x /= 1 + Drag.x * Time.deltaTime;
        //move.y /= 1 + Drag.y * Time.deltaTime;
        //move.z /= 1 + Drag.z * Time.deltaTime;
    }

    //private void Dance()
    //{
    //    if (Input.GetKeyDown("k"))
    //    {
    //        m_dance = !m_dance;
    //    }
    //}

    /*private void OnAnimatorMove()
    {
        Vector3 newRootPosition;
        Quaternion newRootRotation;

        //bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        //if (isGrounded)
        //{
            //use root motion as is if on the ground		
            newRootPosition = m_animator.rootPosition;
        //}
        //else
        //{
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            //newRootPosition = new Vector3(m_animator.rootPosition.x, this.transform.position.y, m_animator.rootPosition.z);
        //}

        //use rotational root motion as is
        newRootRotation = m_animator.rootRotation;


        this.transform.position = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);
        this.transform.rotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootTurnSpeed);
    }*/

    private void UpdateAnimator()
    {
        m_animator.SetFloat("Forward", inputForward);
        m_animator.SetFloat("Turn", inputTurn);
        m_animator.SetBool("Jump", m_jump);
        m_animator.SetBool("Dance", m_dance);
        m_animator.SetBool("Sprint", m_sprint);
        m_animator.SetBool("Dash", m_dash);
        //m_animator.speed = animationSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "ground" && hitGroundAudio != null)
        {
            playerAudio.clip = hitGroundAudio;
            playerAudio.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
