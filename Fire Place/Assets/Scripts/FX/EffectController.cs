using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace FirePlace.FX
{

    [RequireComponent(typeof(Volume))]
    public class EffectController : MonoBehaviour
    {
        
        private Volume volume;

        public VolumeProfile defaultProfile;
        public VolumeProfile noAOProfile;
        public VolumeProfile noBloomProfile;
        public VolumeProfile noBloomAOProfile;

        private bool useAO = true;
        private bool useBloom = true;

        public bool UseAO
        {
            get { return useAO; }
            set
            {
                useAO = value;
                UpdateVisualEffects();

            }
        }

        public bool UseBloom
        {
            get { return useBloom; }
            set
            {
                useBloom = value;
                UpdateVisualEffects();

            }
        }

        private void Start()
        {

            volume = GetComponent<Volume>();

        }

        public static void UpdateAO(bool enabled) 
        { 

            EffectController[] effects = FindObjectsOfType<EffectController>();

            foreach (EffectController effect in effects)
            {
                
                effect.UseAO = enabled;

            }

        }

        public static void UpdateBloom(bool enabled) 
        { 
            EffectController[] effects = FindObjectsOfType<EffectController>();

            foreach (EffectController effect in effects)
            {
                
                effect.UseBloom = enabled;

            }
        }

        private void UpdateVisualEffects()
        {

            if(!useBloom && useAO)
                volume.profile = noBloomProfile;
            else if(useBloom && !useAO)
                volume.profile = noAOProfile;
            else if(!useBloom && !useAO)
                volume.profile = noBloomAOProfile;
            else
                volume.profile = defaultProfile;

        }

    }

}
