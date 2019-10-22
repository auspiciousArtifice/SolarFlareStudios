using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;
    public GameObject hand; // Used to keep track of the "right palm" area of the player with independent movement

    public float hookTravelSpeed = 25f; // speed the hook flies
    public float playerTravelSpeed = 15f; // speed which it pulls the player
    public float maxDistance = 20f;

    [HideInInspector] public static bool fired;
    [HideInInspector] public static bool swinging;
    [HideInInspector] public bool hooked;
    [HideInInspector] public GameObject hookedObj;

    //private CharacterController m_controller;

    private Rigidbody playerRB;

    private float currentDistance;
    private float distanceToHook;
    private Camera mainCamera;

    private LineRenderer rope;

    private Vector3 HookTrajectory;
    private Vector3 futurePos;
    private Vector3 swingingVelocity;

    private Transform originalParent;

    private void Start()
    {
        originalParent = hookHolder.transform.parent;
        playerRB = GetComponent<Rigidbody>();
        rope = hook.GetComponent<LineRenderer>();
        //m_controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera");
        }
    }

    private void Update()
    {
        futurePos = transform.position + playerRB.velocity * Time.deltaTime;
        // firing the hook
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            hookHolder.transform.parent = null; // Makes the hookHolder's position independent of any other game object.
            fired = true;
            HookTrajectory = Vector3.Normalize(mainCamera.transform.forward);
        }
        else if (Input.GetMouseButtonDown(0) && fired && hooked && !swinging)
        {
            hookHolder.transform.parent = originalParent;
            swinging = true;
            playerRB.useGravity = true;
        }
        else if (Input.GetMouseButtonDown(0) && (swinging || !hooked))
        {
            if (swinging)
            {
                playerRB.AddForce(swingingVelocity, ForceMode.VelocityChange);
            }
            ReturnHook();
        }

        if (fired || swinging)
        {
            rope.positionCount = 2;
            rope.SetPosition(0, hand.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }
        else
        {
            hookHolder.transform.position = hand.transform.position;
        }

        if (fired && !hooked)
        {
            //hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            hook.transform.position = hook.transform.position + HookTrajectory * hookTravelSpeed * Time.deltaTime;
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
                ReturnHook();
        }

        if (hooked && fired && !swinging)
        {
            hook.transform.parent = hookedObj.transform;
            playerRB.MovePosition(Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed));
            distanceToHook = Vector3.Distance(hand.transform.position, hook.transform.position);

            playerRB.useGravity = false;


            if (distanceToHook < 2f)
            {
                //if (!grounded)
                //{
                //    //m_controller.Move(Vector3.forward * Time.deltaTime * 13f);
                //    //m_controller.Move(Vector3.up * Time.deltaTime * 18f);
                //    this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                //    this.transform.Translate(Vector3.up * Time.deltaTime * 18f);
                //}

                //StartCoroutine("Climb");
                ReturnHook();
            }
        }

        if (hooked && fired && swinging)
        {
            if ((futurePos - hook.transform.position).magnitude > distanceToHook)
            {
                playerRB.MovePosition(hook.transform.position + (futurePos - hook.transform.position).normalized * distanceToHook);
            }
            else
            {
                playerRB.MovePosition(futurePos);
            }

            Vector3 upVector = playerRB.position - hook.transform.position;
            playerRB.MoveRotation(Quaternion.LookRotation(upVector, Vector3.up));
            swingingVelocity = playerRB.velocity;
            //transform.rotation = Quaternion.LookRotation(playerRB.velocity, upVector);
        }

        if (!hooked && !fired)
        {
            hook.transform.parent = hookHolder.transform;
            hook.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            hook.transform.rotation = hookHolder.transform.rotation;
            hook.transform.position = hookHolder.transform.position;
        }

    }

    //IEnumerator Climb()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    ReturnHook();
    //}

    private void ReturnHook()
    {
        fired = false;
        hooked = false;
        swinging = false;

        rope.positionCount = 0;
    }


    //void CheckIfGrounded()
    //{
    //    RaycastHit hit;
    //    float distance = 1f;
    //    Vector3 dir = new Vector3(0, -1);

    //    if (Physics.Raycast(transform.position, dir, out hit, distance))
    //    {
    //        grounded = true;
    //    }
    //    else
    //    {
    //        grounded = false;
    //    }
    //}

}
