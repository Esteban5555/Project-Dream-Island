using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VcamsScript : MonoBehaviour
{
    private GameObject VcamObject;
    private List<GameObject> Vcams;

    // Start is called before the first frame update
    void Start()
    {
        VcamObject = GameObject.Find("VCams");
        Vcams = new List<GameObject>();

        for (int i = 0; i < VcamObject.transform.childCount; i++) {
            Vcams.Add(VcamObject.transform.GetChild(i).gameObject);
        }


    }

    public void DeactivateAllVcams() {
        for (int i = 0; i < Vcams.Count; i++)
        {
            Vcams[i].SetActive(false);
        }
    }
}
