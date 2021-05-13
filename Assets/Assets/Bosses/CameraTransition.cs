using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    public GameObject NewVcam;

    VcamsScript VcamManager;
    
    bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        VcamManager = GameObject.Find("VCams").GetComponent<VcamsScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "MainCharacter") {
            VcamManager.DeactivateAllVcams();
            NewVcam.SetActive(true);
        }
    }
}
