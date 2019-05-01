using System;
using Fireplace.Util;
using UnityEngine;
using UnityEngine.Experimental.VFX;

namespace Fireplace.VFX 
{
    public class Blizzard : MonoBehaviour
    {
        [SerializeField] private string cameraScene = "Player";
        [SerializeField] private string cameraTag = "MainCamera";
        
        private VisualEffect blizzard;
        private GameObject cameraObject;
        
        public float Intensity
        {
            get => blizzard.GetFloat(nameof(Intensity));
            set
            {
                if (value < 0f || value > 1f) throw new ArgumentOutOfRangeException();
                
                blizzard.SetFloat(nameof(Intensity), value);
            }
        }
        private void Awake()
        {
            blizzard = GetComponent<VisualEffect>();
            cameraObject = Finder.FindRootObject(cameraScene, cameraTag);
        }

        private void Update()
        {
            if (transform) transform.position = cameraObject.transform.position;
        }
    }
}