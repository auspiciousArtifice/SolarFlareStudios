using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;
    public GameObject hand; // Used to keep track of the "right palm" area of the player with independent movement
    public GameObject player;

    public float hookTravelSpeed; // speed the hook flies
    public float playerTravelSpeed; // speed which it pulls the player
    public float maxDistance;

    [HideInInspector] public static bool fired;
    [HideInInspector] public bool swinging;
    [HideInInspector] public bool hooked;
    [HideInInspector] public GameObject hookedObj;
    [HideInInspector] public Vector3 pullVector;
    [HideInInspector] public LineRenderer rope;

    private Rigidbody playerRB;

    private bool rappelling;
    private bool rappelDown;
    private float currentDistance;
    private float distanceToHook;
    private Camera mainCamera;

    private Vector3 HookTrajectory;

    private Transform originalParent;

    private MouseAimCamera AimCamera;

    private void Start()
    {
        originalParent = hookHolder.transform.parent;
        playerRB = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        distanceToHook = float.MaxValue;
        AimCamera = mainCamera.GetComponent<MouseAimCamera>();
        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera");
        }
    }

    private void Update()
    {
        // firing the hook
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            hookHolder.transform.parent = null; // Makes the hookHolder's position independent of any other game object.
            fired = true;
            rope = hook.GetComponent<LineRenderer>();
            rope.positionCount = 2;
            rope.SetPosition(0, hand.transform.position);
            rope.SetPosition(1, hook.transform.position);

            HookTrajectory = Vector3.Normalize(mainCamera.transform.forward);
            if (AimCamera.hit.collider != null)
            {
                HookTrajectory = Vector3.Normalize(AimCamera.hit.collider.transform.gameObject.transform.position - hookHolder.transform.position);
            } else
            {
                HookTrajectory = Vector3.Normalize(mainCamera.transform.forward);
            }
        }
        else if ((Input.GetMouseButtonDown(0) || distanceToHook < 3.5f) && fired && hooked && !swinging)
        {
            hookHolder.transform.parent = originalParent;
            swinging = true;
            playerRB.useGravity = true;
            hookedObj.GetComponent<ConfigurableJoint>().transform.position = hookedObj.transform.position;
            hookedObj.GetComponent<ConfigurableJoint>().connectedBody = playerRB;
            playerRB.freezeRotation = false;
        }
        else if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("Dash") || Input.GetButtonDown("Jump")) && (swinging || !hooked))
        {
            if (swinging)
            {
                hookedObj.GetComponent<ConfigurableJoint>().connectedBody = null;
                playerRB.freezeRotation = true;
            }
            ReturnHook();
        }
        else if (Input.GetButtonDown("RappelDown") && swinging)
        {
            rappelling = true;
            rappelDown = true;
        }
        else if (Input.GetButtonDown("RappelUp") && swinging)
        {
            rappelling = true;
            rappelDown = false;
        }
        else if ((Input.GetButtonUp("RappelDown") || Input.GetButtonUp("RappelUp")) && swinging)
        {
            rappelling = false;
            hookedObj.GetComponent<ConfigurableJoint>().connectedBody = null;
            hookedObj.GetComponent<ConfigurableJoint>().connectedBody = playerRB;
        }
    }

    private void FixedUpdate()
    {
        if (!fired && !swinging)
        {
            hookHolder.transform.position = hand.transform.position;
        }
        else if (fired || swinging)
        {
            rope.positionCount = 2;
            rope.SetPosition(0, hand.transform.position);
            if (swinging)
            {
                hook.GetComponent<MeshRenderer>().enabled = false;
                rope.SetPosition(1, hookedObj.transform.position);
            }
            else
            {
                rope.SetPosition(1, hook.transform.position);
            }
        }

        if (fired && !hooked)
        {
            hook.transform.position = hook.transform.position + HookTrajectory * hookTravelSpeed * Time.deltaTime;
            currentDistance = Vector3.Distance(hand.transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
                ReturnHook();
        }

        if (hooked && fired && !swinging)
        {
            //hook.transform.parent = hookedObj.transform;
            //playerRB.AddForce(Vector3.MoveTowards(playerRB.transform.position, hook.transform.position, playerTravelSpeed));
            playerRB.MovePosition(Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed));
            //playerRB.MoveRotation(Quaternion.LookRotation(playerRB.transform.forward, hook.transform.position - playerRB.transform.position));
            distanceToHook = Mathf.Abs(Vector3.Distance(hand.transform.position, hook.transform.position));
            Quaternion q = Quaternion.FromToRotation(playerRB.transform.up, HookTrajectory) * playerRB.transform.rotation;
            playerRB.transform.rotation = Quaternion.Slerp(playerRB.transform.rotation, q, Time.deltaTime * 15f);

            playerRB.useGravity = false;
        }

        if (swinging && rappelling)
        {
            hookedObj.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Free;
            if (!rappelDown)
            {
                Vector3 toHook = (hookedObj.transform.position - playerRB.transform.position).normalized;
                playerRB.AddForce(toHook * (toHook.y > 0 ? Physics.gravity.magnitude * 2 / toHook.y : 0), ForceMode.Acceleration);
            }
        }
        else if (swinging && !rappelling)
        {
            hookedObj.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
        }

        if (!hooked && !fired)
        {
            hook.transform.parent = hookHolder.transform;
            hook.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            hook.transform.rotation = hookHolder.transform.rotation;
            hook.transform.position = hookHolder.transform.position;
            Quaternion q = Quaternion.FromToRotation(playerRB.transform.up, Vector3.up) * playerRB.transform.rotation;
            playerRB.transform.rotation = Quaternion.Slerp(playerRB.transform.rotation, q, Time.deltaTime * 5f);
        }
    }

    private void ReturnHook()
    {
        fired = false;
        hooked = false;
        swinging = false;
        distanceToHook = float.MaxValue;
        hook.GetComponent<MeshRenderer>().enabled = true;

        rope.positionCount = 0;
    }
}
