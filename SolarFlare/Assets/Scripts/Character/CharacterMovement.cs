using System.Collections;
using System.Collections.Generic;
using GameManager;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float DashDistance;
	[SerializeField] private Vector3 Drag;
	[SerializeField] private AudioClip hitGroundAudio;
	private AudioSource playerAudio;

    private Camera mainCamera;

    private Rigidbody m_rigidbody;
    private Collider m_collider;
    private Animator m_animator;
    private RuntimeAnimatorController ground_animator;
	[SerializeField] private RuntimeAnimatorController air_animator;

	private GameObject[] Sword;
	private GameObject Hook;
	private GrapplingHook grapplingHook;

    private Vector3 dashDirection;

    // Animator variables
    //private bool m_dance;
	private bool m_sprint;
    private float inputForward = 0f;
    private float inputTurn = 0f;

    public float forwardInputFilter;
    public float turnInputFilter;
    private float forwardSpeedLimit = 5f;
    public float airbornSpeedMult;
    public float swingingSpeedMult;

    private int maxDashes = 1;

    private int dashesLeft;

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

		Sword = GameObject.FindGameObjectsWithTag("Sword");
		if (Sword.Length == 0)
		{
			Debug.LogWarning("No sword");
		}
		else
		{
			foreach (GameObject s in Sword)
			{
				s.SetActive(false);
			}
		}
		Hook = GameObject.FindGameObjectWithTag("Hook");
		if (!Hook)
		{
			Debug.LogWarning("No hook found");
		}
		grapplingHook = GetComponent<GrapplingHook>();
		if (!grapplingHook)
		{
			Debug.LogWarning("No script grappling found");
		}

		mainCamera = Camera.main;
		if (mainCamera == null)
		{
			Debug.LogWarning("No main camera");
		}
	}

    void FixedUpdate()
    {
        Move();
        Jump();
        Dash();
		Sprint();
        SwingSword();
        PushButton();
		SwitchWeapon();
        //Dance();

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
        else if (grapplingHook.swinging)
        {
            m_rigidbody.AddForce(v * mainCamera.transform.forward.normalized * swingingSpeedMult);
            //m_rigidbody.transform.Rotate(0, h * Time.deltaTime * turnInputFilter, 0, Space.Self);
        }
        else
        {
            m_rigidbody.AddForce(mainCamera.transform.forward.normalized * airbornSpeedMult * v);
            Vector3 perp = Vector3.Cross(mainCamera.transform.forward.normalized, Vector3.up);
            m_rigidbody.AddForce(perp * airbornSpeedMult * (-h));
            //m_rigidbody.AddForce(new Vector3(v, 0, -h).normalized * 5f);
        }
        if (isGrounded)
        {
            inputTurn = Mathf.Lerp(inputTurn, h, Time.deltaTime * turnInputFilter);
			Rotate();

        }
        else if (!isGrounded && !grapplingHook.swinging)
        {

        }

		if (grapplingHook.swinging && !isGrounded && m_animator.runtimeAnimatorController != air_animator)
		{
			m_animator.runtimeAnimatorController = air_animator;
			m_animator.applyRootMotion = false;
			isGrounded = false;
		}
    }

	private void SwitchWeapon()
	{
		if (Input.GetKeyDown("1"))
		{
			foreach (GameObject s in Sword)
			{
				s.SetActive(!s.activeSelf);
			}
			grapplingHook.enabled = !Hook.activeSelf;
			Hook.SetActive(!Hook.activeSelf);
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
		if (Input.GetMouseButtonDown(0) && Sword[0].activeSelf)
		{
			m_animator.SetTrigger("SwingSword");
		}
	}

    public bool isSwingingSword()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing");
    }

    // Apply rotation
    private void Rotate()
    {
		Quaternion rotateCamera = mainCamera.transform.localRotation;
		transform.rotation = new Quaternion(0, rotateCamera.y, 0, rotateCamera.w);
    }

    // makes character jump if on ground and press space
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
			m_animator.SetTrigger("Jump");
			isGrounded = false;
		}
    }

    // makes character dash if press left alt
    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && dashesLeft > 0)
        {
			if (!isGrounded && m_animator.runtimeAnimatorController != air_animator)
			{
				m_animator.runtimeAnimatorController = air_animator;
				m_animator.applyRootMotion = false;
				isGrounded = false;
			}
			Debug.Log("Dash");

            dashDirection = mainCamera.transform.forward.normalized;
            m_rigidbody.AddForce(dashDirection * DashDistance, ForceMode.Impulse);
            dashesLeft--;
			m_animator.SetTrigger("Dash");

        }
    }

    public void AddDash()
    {
        if (dashesLeft + 1 > maxDashes)
        {
            dashesLeft = maxDashes;
        }
        else
        {
            dashesLeft++;
        }
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
		if (m_animator.runtimeAnimatorController == ground_animator)
		{
			m_animator.SetFloat("Turn", inputTurn);
			m_animator.SetFloat("Forward", inputForward);
			//m_animator.SetBool("Dance", m_dance);
			m_animator.SetBool("Sprint", m_sprint);
			m_animator.SetBool("Grounded", isGrounded);
		}
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_rigidbody.angularVelocity = new Vector3(0,0,0);
        if (collision.gameObject.tag == "ground" && !isGrounded && !grapplingHook.swinging)
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
			Debug.LogWarning("ground sound is broken");
		}
    }

    private void OnCollisionStay(Collision collision)
    {
        if (grapplingHook.hooked && grapplingHook.swinging)
        {
            m_rigidbody.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground" && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing") && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !GetComponent<PlayerHealth>().isDead)
        {
            m_animator.runtimeAnimatorController = air_animator;
            m_animator.applyRootMotion = false;
            isGrounded = false;
        }
	}
}
