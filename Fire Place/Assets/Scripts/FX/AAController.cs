using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace FirePlace.FX
{

    [RequireComponent(typeof(UnityEngine.Camera))]
    public class AAController : MonoBehaviour
    {
        
        public HDAdditionalCameraData cam;

        private bool useAA = true;
        public bool UseAA
        {
            get { return useAA; }
            set
            {

                AAController.UpdateAA(value);

            }
        }


        private void Start()
        {

            cam = GetComponent<HDAdditionalCameraData>();

        }

        public static void UpdateAA(bool enabled) 
        { 

            AAController[] aa_s = FindObjectsOfType<AAController>();

            foreach (AAController aa in aa_s)
            {
                
                aa.useAA = enabled;

                if(enabled)
                    aa.cam.antialiasing = HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing;
                else
                    aa.cam.antialiasing = HDAdditionalCameraData.AntialiasingMode.None;

            }

            

        }

    }

}
