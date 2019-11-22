using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    public float DashDistance;
    public Vector3 Drag;
    public AudioClip hitGroundAudio;
    private AudioSource playerAudio;

    private Camera mainCamera;

    private Rigidbody m_rigidbody;
    private Collider m_collider;
    private Animator m_animator;
    private RuntimeAnimatorController ground_animator;
    public RuntimeAnimatorController air_animator;

    private Transform leftFoot;
    private Transform rightFoot;

    private Vector3 move;
    private Vector3 dashDirection;

    // Animator variables
    //private bool m_dance;
	private bool m_sprint;
    private float inputForward = 0f;
    private float inputTurn = 0f;
	private float extraTurn;

    public float forwardInputFilter;
    public float turnInputFilter;
    private float forwardSpeedLimit = 5f;
    public float airbornSpeedMult;
    public float swingingSpeedMult;

    //private int groundContactCount;

    private int maxDashes = 1;

    private int dashesLeft;

    private bool turnBasedOnLook;

    private bool isGrounded;

    public GameObject buttonObject;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        if (m_animator == null)
            Debug.Log("Animator could not be found");
        ground_animator = m_animator.runtimeAnimatorController;

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
        SwingSword();
        PushButton();
        
        //Rotate();
        //Dance();

		// Apply move vector
		//m_controller.Move(this.transform.forward * inputForward * Time.deltaTime * moveSpeed);

		//m_rigidbody.AddForce(move * Time.deltaTime);

        // send input and other state parameters to the animator
        UpdateAnimator();
    }

    // Calculate move
    private void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis
        h *= Mathf.Sqrt(1f - 0.5f * v * v);
        v *= Mathf.Sqrt(1f - 0.5f * h * h);
        if (isGrounded)
        {
            inputForward = Mathf.Clamp(Mathf.Lerp(inputForward, v,
                Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);
        }
        else if (GrapplingHook.swinging)
        {
            m_rigidbody.AddForce(v * mainCamera.transform.forward.normalized * swingingSpeedMult);
            //m_rigidbody.transform.Rotate(0, h * Time.deltaTime * turnInputFilter, 0, Space.Self);
        }
        else
        {
            m_rigidbody.AddForce(mainCamera.transform.forward.normalized * airbornSpeedMult);
            //m_rigidbody.AddForce(new Vector3(v, 0, -h).normalized * 5f);
        }
        if (isGrounded)
        {
            inputTurn = Mathf.Lerp(inputTurn, h, Time.deltaTime * turnInputFilter);

            float angle = Vector3.SignedAngle(transform.forward, mainCamera.transform.forward, Vector3.up); //Angle between player direction and camera direction.
            if (inputForward > 0.5 && (angle > 10 || angle < -10))
            {
                h = angle < 0 ? -1 : 1;

                h = h * Mathf.Sqrt(1f - 0.5f * v * v);

				// commented off because breaking things
                //inputTurn = Mathf.Lerp(inputTurn, h, Time.deltaTime * turnInputFilter);


            }
        }
        else if (!isGrounded && !GrapplingHook.swinging)
        {

        }
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

    private void PushButton()
    {
        if (Input.GetButtonDown("PushButton"))
        {
			m_animator.SetTrigger("BridgeButtonPressed");
        }
    }

    private void SwingSword()
    {
		if (Input.GetButtonDown("SwingSword"))
		{
			m_animator.SetTrigger("SwingSword");
		}
	}

    private bool isSwingingSword()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing");
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
			m_animator.SetTrigger("Jump");
        }
    }

    // makes character dash if press left alt
    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && dashesLeft > 0)
        {
            Debug.Log("Dash");

            dashDirection = mainCamera.transform.forward.normalized;
            m_rigidbody.AddForce(dashDirection * DashDistance, ForceMode.Impulse);
            dashesLeft--;
			//m_rigidbody.MovePosition(Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0,
			//(Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime))));
			m_animator.SetTrigger("Dash");

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

    private void UpdateAnimator()
    {
        m_animator.SetFloat("Turn", inputTurn);
        m_animator.SetFloat("Forward", inputForward);
        //m_animator.SetBool("Dance", m_dance);
        m_animator.SetBool("Sprint", m_sprint);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            m_animator.runtimeAnimatorController = ground_animator;
            m_animator.applyRootMotion = true;
            isGrounded = true;
            dashesLeft = maxDashes;
        }

		if (hitGroundAudio != null)
		{
			playerAudio.clip = hitGroundAudio;
			playerAudio.Play();
		}
		else
		{
			Debug.LogWarning("ground sound is probably broken");
		}
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground" && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
        {
            m_animator.runtimeAnimatorController = air_animator;
            m_animator.applyRootMotion = false;
            isGrounded = false;
        }
    }
}
