using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    
    [SerializeField] private Transform follow = null;
    private Vector3 relativeDistance = Vector3.zero;

    private void Start () {

        relativeDistance = transform.position - follow.position;

    }

    private void Update() {

        transform.position = follow.position + relativeDistance;

    }

}
