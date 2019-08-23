using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FirePlace;

namespace FirePlace.Util
{

    public class CutsceneTrigger : MonoBehaviour
    {
        
        [SerializeField] private Transform dest = null;
        [SerializeField] private bool isEndCutscene = false;

        void OnTriggerEnter(Collider other)
        {

            if(other.tag == "Player")
            {
                PlayerBehaviour.instance.Cutscene(dest.position, isEndCutscene);
                PlayerBehaviour.instance.hud.interactIcon.enabled = false;

                Destroy(this);

            }
        }
    }

}