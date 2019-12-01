using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;

    private GrapplingHook grapplingHook;

    private void Start()
    {
        grapplingHook = player.GetComponent<GrapplingHook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hookable"))
        {
            grapplingHook.hooked = true;
            grapplingHook.hookedObj = other.gameObject;
            player.GetComponent<CharacterMovement>().AddDash();
        }
    }
}
