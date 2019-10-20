using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Created by Sean Joplin
/// This will be the navigation manager for each agent outside of the 
/// actual movement calculation which are controlled by NavMeshAgent
/// </summary>
public class Navigation : MonoBehaviour
{
	/// <summary>
	/// Should we be doing patrolling behavior
	/// </summary>
	public bool patrol = true;

	/// <summary>
	/// If we should be patrolling, which points should we patrol between
	/// </summary>
	public List<GameObject> patrolSpots = new List<GameObject>();

	/// <summary>
	/// The player we want to find and destroy
	/// </summary>
	private GameObject player;

	/// <summary>
	/// Used for distance calculations
	/// </summary>
	private Transform myTransform;

	/// <summary>
	/// Used to move us to places
	/// </summary>
	private NavMeshAgent agent;

	/// <summary>
	/// Which patrol spot is next
	/// </summary>
	private int patrolIndex = 0;

	/// <summary>
	/// Where'd you go cotton eye joe
	/// </summary>
	private Transform curDest = null;

	/// <summary>
	/// Are we currently seeking a patrol point
	/// </summary>
	private bool seekingPatrol = false;

	/// <summary>
	/// Are we currently seeking a player
	/// </summary>
	private bool seekingPlayer = false;
	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		myTransform = GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		//If the player gets too close, attack!
		if (Vector3.Distance(myTransform.position, player.transform.position) < 10)
		{
			seekingPlayer = true;
			seekingPatrol = false;
			Seek(player.transform);
		}
		//if the player moved too far away, return to nearest patrol point
		else if (seekingPlayer && Vector3.Distance(myTransform.position, player.transform.position) >= 10)
		{
			seekingPlayer = false;
			float closestDist = 999999999;
			for (int i = 0; i < patrolSpots.Count; i++)
			{
				float curDist = Vector3.Distance(myTransform.position, patrolSpots[i].transform.position);
				if (curDist < closestDist)
				{
					closestDist = curDist;
					patrolIndex = i;
				}
			}
		}
		if (patrol)
		{
			if (!seekingPatrol && !seekingPlayer)
			{
				curDest = patrolSpots[patrolIndex % patrolSpots.Count].GetComponent<Transform>();
				patrolIndex++;
				Seek(curDest);
				seekingPatrol = true;
			}
			else if (Vector3.Distance(myTransform.position, curDest.position) < 4)
			{
				seekingPatrol = false;
			}
		}
	}

	/// <summary>
	/// Seek the specified destination.
	/// </summary>
	/// <param name="destination">Where should we go next</param>
	private void Seek(Transform destination)
	{
		agent.destination = destination.position;
	}
}
