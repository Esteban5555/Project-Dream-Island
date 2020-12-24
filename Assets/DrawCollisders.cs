using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCollisders : MonoBehaviour
{
    Collider2D[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponents<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (colliders == null) { return; }
        Gizmos.color = Color.red;
        for (int i = 0; i < colliders.Length; i++)
        {
            Gizmos.DrawWireCube(colliders[i].bounds.center, new Vector2(colliders[i].bounds.size.x, colliders[i].bounds.size.y));
        }
    }
}
