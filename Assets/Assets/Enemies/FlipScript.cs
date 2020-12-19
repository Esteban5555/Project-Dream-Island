using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class FlipScript : MonoBehaviour
{
    public AIPath aipath;
    bool facingRight = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (aipath.desiredVelocity.x > 0 && facingRight == false)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            facingRight = !facingRight;
        }
        else { if (aipath.desiredVelocity.x < 0 && facingRight) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                facingRight = !facingRight;
            } 
        }
    }
}
