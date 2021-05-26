using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject DoorClosed;

    bool lever01, lever02 = false;

    // Start is called before the first frame update
    void Start()
    {
        DoorClosed.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (lever01 && lever02) {
            DoorClosed.SetActive(false);
        }
    }

    public void setLever() {
        if (!lever01)
        {
            lever01 = true;

        }
        else {
            lever02 = true;
        }
    }

}
