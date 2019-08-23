using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FirePlace.FX;

namespace FirePlace.Util
{

    public class ChangeBlizzardTrigger : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Player";
        [SerializeField] private float intensity = 1000;

        void OnTriggerEnter(Collider other) {

            if(other.tag == targetTag) 
            {
                Blizzard.instance.ChangeIntensity(intensity);
                Destroy(this);
            }

        }

    }

}
