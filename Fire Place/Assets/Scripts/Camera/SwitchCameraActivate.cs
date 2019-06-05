using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCameraActivate : MonoBehaviour
{
    
    [SerializeField] private float switchDelay = 1.5f;

    [SerializeField] private bool disableOld = true;

    [SerializeField] private CinemachineVirtualCamera oldCamera = null;
    [SerializeField] private CinemachineVirtualCamera newCamera = null;

    public void ActivateSwitch () 
    {

        StartCoroutine(DelaySwitch());

    }   

    private IEnumerator DelaySwitch () 
    {

        float time = 0;

        while(time < switchDelay) 
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        if(!disableOld)
            oldCamera.enabled = false;
        else
            oldCamera.gameObject.SetActive(false);

        newCamera.enabled = true;

    }

}
