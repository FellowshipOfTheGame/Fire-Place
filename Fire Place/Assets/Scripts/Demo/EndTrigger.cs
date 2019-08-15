using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FirePlace.Demo
{

    public class EndTrigger : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Player";

        void OnTriggerEnter(Collider other) {

            if(other.tag == targetTag) EndDemo.instance.End();

        }

}

}
