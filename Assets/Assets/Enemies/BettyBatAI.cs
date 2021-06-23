using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BettyBatAI : MonoBehaviour
{
    private enum enemyBettyBatStates
    {
        Hiding,
        Follow,
    }

    private enemyBettyBatStates state;

    public Transform target;

    public float FollowSpeed = 200f;
    public float speed = 5f;
    public float nextWaypointDistance = 3f;
    public float AwarnessRange = 5f;

    Seeker seeker;
    Rigidbody2D rb;
    Animator anim;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    bool returning = false;

    Vector3 roamPosition;

    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        state = enemyBettyBatStates.Hiding;

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            switch (state)
            {
                default:
                case enemyBettyBatStates.Hiding:
                    //seeker.StartPath(rb.position, roamPosition, OnPathComplete);
                    anim.SetBool("Pursuing", false);
                    //speed = RoamSpeed;
                    break;

                case enemyBettyBatStates.Follow:

                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                    returning = false;
                    speed = FollowSpeed;
                    break;
            }

        }

        if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
            facingRight = false;
        }
        else
        {
            if (!facingRight && rb.velocity.x >= 0)
            {
                Flip();
                facingRight = true;
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

        if (Vector3.Distance(transform.position, target.position) < AwarnessRange)
        {
            FindObjectOfType<AudioManager>().Play("BatSceaching");
            anim.SetBool("Pursuing", true);
            state = enemyBettyBatStates.Follow;
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

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0);
    }
}
