using UnityEngine;
using System.Collections;

public class TransformFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Space offsetPositionSpace = Space.Self;
    [SerializeField] private bool lookAt = true;
    [SerializeField] private bool m_AutoTargetPlayer = true;  // Whether the rig should automatically target the player.

    protected virtual void Start()
    {
        // if auto targeting is used, find the object tagged "Player"
        // any class inheriting from this should call base.Start() to perform this action!
        if (m_AutoTargetPlayer)
        {
            FindAndTargetPlayer();
        }
    }

    private void LateUpdate()
    {
        Refresh();
        if (m_AutoTargetPlayer && (target == null || !target.gameObject.activeSelf))
        {
            FindAndTargetPlayer();
        }
    }

    public void FindAndTargetPlayer()
    {
        // auto target an object tagged player, if no target has been assigned
        var targetObj = GameObject.FindGameObjectWithTag("Player");
        if (targetObj)
        {
            SetTarget(targetObj.transform);
        }
    }

    public virtual void SetTarget(Transform newTransform)
    {
        target = newTransform;
    }

    public Transform Target
    {
        get { return target; }
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}