using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BushCrabAI : MonoBehaviour
{
    private enum enemyBushCrabStates
    {
        Hiding,
        BackToHide,
        Follow,
    }

    private enemyBushCrabStates state;

    public Transform target;
    public Transform HidingSpot;

    public float FollowSpeed = 200f;
    public float speed = 5f;
    public float nextWaypointDistance = 3f;
    public float AwarnessRanfge = 5f;

    bool inHidingSpot = true;

    Seeker seeker;
    Rigidbody2D rb;
    Animator anim;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Vector3 roamPosition;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        state = enemyBushCrabStates.Hiding;

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            switch (state)
            {
                default:
                case enemyBushCrabStates.Hiding:
                    //seeker.StartPath(rb.position, roamPosition, OnPathComplete);
                    //speed = RoamSpeed;
                    break;
                case enemyBushCrabStates.BackToHide:
                    seeker.StartPath(rb.position, HidingSpot.position, OnPathComplete);
                    speed = FollowSpeed;
                    break;
                case enemyBushCrabStates.Follow:
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                    speed = FollowSpeed;
                    break;
            }

        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < AwarnessRanfge)
        {
            anim.SetBool("persuing", true);
            state = enemyBushCrabStates.Follow;
        }
        else
        {
            state = enemyBushCrabStates.BackToHide;
            
            if (inHidingSpot) {
                anim.SetBool("persuing", false);
                state = enemyBushCrabStates.Hiding;
            }
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (path == null)
        {
            return;
        }

        //Move Enemy throuhout the path

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "HidingSpot") {
            inHidingSpot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "HidingSpot") {
            inHidingSpot = false;
        }
    }
}
