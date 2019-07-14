using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private AudioSource source = null;

    void OnTriggerEnter(Collider other) {

        if(other.tag == targetTag) source.Play();

    }

}
