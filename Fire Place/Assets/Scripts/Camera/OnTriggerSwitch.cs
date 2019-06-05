using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OnTriggerSwitch : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera toggleCam = null;
    
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {

            //cam.enabled = false;
            toggleCam.enabled = true;

        }

    }

}
