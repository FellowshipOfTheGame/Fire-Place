using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

namespace FirePlace.Camera
{

    public class OnTriggerSwitch : MonoBehaviour
    {

        [SerializeField] private string toggleCamName = "";
        
        private void OnTriggerEnter(Collider other)
        {

            if(other.tag == "Player")
            {

                CinemachineVirtualCamera toggleCam = GameObject.Find(toggleCamName).GetComponent<CinemachineVirtualCamera>();

                if(toggleCam != null)
                    toggleCamName = CameraController.instance.SwichCameras(toggleCam).transform.name;
                else
                    Debug.Log("FirePlace.Camera.OnTriggerSwitch: Camera " + toggleCamName  + " not found!");

            }

        }

    }
}
