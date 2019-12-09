using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer : MonoBehaviour
{
    [SerializeField] float timeBeforeDestroy = 2;
    [SerializeField] float waypointTolerance = 1f;

    //could make deer with multiple sensitivities
    [SerializeField] float walkingDetectionDistance = 60f;
    [SerializeField] float crouchDetectionDistance = 15f;
    float currentDetectionDistance;

    //walking speed 
    float walkingSpeed = 4f;
    //run speed 
    float runSpeed = 12f;
    //time spent fleeing 
    float fleeTime = 6f;
    //time since spooked 
    float timeSinceSpooked = Mathf.Infinity;

    int currentWaypointIndex;
    bool dead = false;

    DeerSpawn deerSpawn;
    NavMeshAgent navMeshAgent;
    Waypoints waypoints;
    Controller player;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkingDetectionDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, crouchDetectionDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        waypoints = FindObjectOfType<Waypoints>();
        currentWaypointIndex = waypoints.GetRandomWaypoint();
        player = FindObjectOfType<Controller>(); 
        navMeshAgent = GetComponent<NavMeshAgent>();
        deerSpawn = FindObjectOfType<DeerSpawn>();
        //start off walking 
        currentDetectionDistance = walkingDetectionDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {

        } else //not dead
        {
            if (player.IsCrouched())
            {
                currentDetectionDistance = crouchDetectionDistance;
            } else
            {
                currentDetectionDistance = walkingDetectionDistance;
            }

            if (InDetectionRange(currentDetectionDistance))
            {
                Flee();
            }
            if (timeSinceSpooked > fleeTime)
            {
                NormalSpeed();
            }
            //wander
            Wander();

            timeSinceSpooked += Time.deltaTime;
        }
        
    }

    private void NormalSpeed()
    {
        navMeshAgent.speed = walkingSpeed;
    }

    private void Flee()
    {
        navMeshAgent.speed = runSpeed;
        timeSinceSpooked = 0;
    }

    private bool InDetectionRange(float detectionRange)
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distance < detectionRange)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Die()
    {
        //change dead variable to true so movement stops 
        dead = true;
        //disable colliders
        foreach (CapsuleCollider collider in GetComponents<CapsuleCollider>())
        {
            collider.enabled = false;
        }
        //stop navmeshagent
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        //tip over
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, 90);
        //change deer count 
        deerSpawn.deadDeer();
        //destroy
        Destroy(gameObject, timeBeforeDestroy);
    }

    //taken from rpg game 
    private void Wander()
    {
        Vector3 nextPosition = transform.position;
        if (waypoints != null)
        {
            if (AtWaypoint())
            {
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }
        //walk towards next waypoint, speed = 3 
        MoveTo(nextPosition);//should change to variable 
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return waypoints.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = waypoints.GetRandomWaypoint();
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }

    public void ChangeDetectionDistance(bool playerCrouched)
    {
        if (playerCrouched)
        {
            currentDetectionDistance = crouchDetectionDistance;
        } else
        {
            currentDetectionDistance = walkingDetectionDistance;
        }
    }
}
