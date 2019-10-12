using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed = 15f; // speed the hook flies
    public float playerTravelSpeed = 20f; // speed which it pulls the player
    public float maxDistance = 20f;

    [HideInInspector] public static bool fired;
    [HideInInspector] public bool hooked;
    [HideInInspector] public GameObject hookedObj;

    //private CharacterController m_controller;

    private float currentDistance;
    private Camera mainCamera;

    private bool grounded;
    private LineRenderer rope;

    private void Start()
    {
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
        // firing the hook
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            fired = true;
        }
        else if (Input.GetMouseButtonDown(0) && fired && !hooked)
        {
            fired = false;
            ReturnHook();
        }


        if (fired)
        {
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if (fired && !hooked)
        {
            //hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            hook.transform.position = hook.transform.position + Vector3.Normalize(mainCamera.transform.forward) * hookTravelSpeed * Time.deltaTime;
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
                ReturnHook();
        }

        if (hooked && fired)
        {
            hook.transform.parent = hookedObj.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            //m_controller.Move(hook.transform.position * Time.deltaTime);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            this.GetComponent<Rigidbody>().useGravity = false;


            if (distanceToHook < 2f)
            {
                if (!grounded)
                {
                    //m_controller.Move(Vector3.forward * Time.deltaTime * 13f);
                    //m_controller.Move(Vector3.up * Time.deltaTime * 18f);
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 13f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 18f);
                }

                StartCoroutine("Climb");
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
            this.GetComponent<Rigidbody>().useGravity = true;
        }

    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    private void ReturnHook()
    {
        hook.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;

        rope.positionCount = 0;
    }


    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

}
