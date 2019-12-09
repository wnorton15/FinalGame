using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject head = null;
    [SerializeField] GameObject body = null;

    //walking vs crouched speed
    [SerializeField] float walkingSpeed = 5f;
    [SerializeField] float crouchedSpeed = 2f;

    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    public void MoveToCursor(bool crouched)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            //slows player when crouched 
            if (crouched)
            {
                navMeshAgent.speed = crouchedSpeed;
            } else
            {
                navMeshAgent.speed = walkingSpeed;
            }
            navMeshAgent.destination = hit.point;
            head.transform.rotation = gameObject.transform.rotation;
        }
    }

    public void Crouch()
    {
        head.transform.position += Vector3.down;
        body.transform.position += Vector3.down;
    }

    public void Uncrouch()
    {
        head.transform.position += Vector3.up;
        body.transform.position += Vector3.up;
    }
}
