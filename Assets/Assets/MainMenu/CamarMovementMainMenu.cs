using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamarMovementMainMenu : MonoBehaviour
{

    public List<GameObject> path;
    public int waypoint = 0;
    public float dist;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(this.transform.position, path[waypoint].transform.position) < dist) {
            waypoint++;
            if (waypoint >= path.Count)
            {
                waypoint = 0;
            }
        }
        this.transform.position = Vector3.MoveTowards(transform.position, path[waypoint].transform.position, speed * Time.deltaTime);
    }
}
