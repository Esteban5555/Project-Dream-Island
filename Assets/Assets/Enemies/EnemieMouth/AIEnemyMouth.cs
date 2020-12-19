using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIEnemyMouth : MonoBehaviour
{
    private enum enemyMouthStates { 
        Roaming,
        Follow,
    }

    private enemyMouthStates state;

    public Transform target;

    public float RoamSpeed = 100f;
    public float FollowSpeed = 200f;
    public float speed = 100f;
    public float nextWaypointDistance = 3f;
    public float AwarnessRanfge = 5f;

    Seeker seeker;
    Rigidbody2D rb;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Vector3 roamPosition;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        roamPosition = GetRoamingPosition();

        state = enemyMouthStates.Roaming;
        
    }

    Vector3 GetRoamingPosition() {
        Vector3 startingPosition = new Vector3(rb.position.x, rb.position.y);
        return startingPosition + GetRandomDirection() * Random.Range(3f, 3f);
    }

    Vector3 GetRandomDirection() {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    void UpdatePath() {

        if (seeker.IsDone()) {
            switch (state) {
                default:
                case enemyMouthStates.Roaming:
                    seeker.StartPath(rb.position, roamPosition, OnPathComplete);
                    speed = RoamSpeed;
                    break;
                case enemyMouthStates.Follow:
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                    speed = FollowSpeed;
                    break;
            }
            
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }  
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < AwarnessRanfge)
        {
            state = enemyMouthStates.Follow;
        }
        else {
            state = enemyMouthStates.Roaming;
        }

        if (reachedEndOfPath && state == enemyMouthStates.Roaming) {
            roamPosition = GetRoamingPosition();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPath();
    }

    void FollowPath() {
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
}
