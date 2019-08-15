using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Cinemachine;

namespace FirePlace.Camera
{

    public class CameraController : MonoBehaviour
    {

        public static CameraController instance;

        private CinemachineVirtualCamera curCam;

        private void Awake()
        {
            
            if(instance == null) instance = this;
            else Destroy(this);

        }

        public void SetCamera(CinemachineVirtualCamera newCam)
        {
            SwichCameras(newCam);
        }

        public CinemachineVirtualCamera SwichCameras(CinemachineVirtualCamera newCam)
        {

            newCam.enabled = true;
            if(curCam != null)
                curCam.enabled = false;

            CinemachineVirtualCamera prevCam = curCam;
            curCam = newCam;

            return prevCam;

        }

    }

}
