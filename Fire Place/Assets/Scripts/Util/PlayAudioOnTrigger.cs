using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FirePlace.Util
{

    public class PlayAudioOnTrigger : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Player";
        [SerializeField] private AudioSource source = null;
        [SerializeField] private bool repeat = false;

        void OnTriggerEnter(Collider other) {

            if(other.tag == targetTag) source.Play();

            if(!repeat) Destroy(this);

        }

    }

}
