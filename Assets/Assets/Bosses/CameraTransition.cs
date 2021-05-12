using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    public GameObject FirstCeraBound, SecondCameraBound;

   
    
    bool first = true;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "MainCharacter") {

            Debug.Log("ChangingCamera");
            FirstCeraBound.SetActive(false);
            SecondCameraBound.SetActive(false);

            if (first) {
                SecondCameraBound.SetActive(true);
            }
            else {
                FirstCeraBound.SetActive(true);
            }

            first = !first;
        }
    }
}
