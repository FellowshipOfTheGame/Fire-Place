using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDecay : MonoBehaviour
{

	private PLRDeathBehavior player = null;

	[SerializeField] private Image iceEffect = null;
	[SerializeField] private float maxIceAlpha = 0.9f;

	[SerializeField] private Image vignetteEffect = null;	
	[SerializeField] private float maxVignetteAlpha = 0.6f;

    // Start is called before the first frame update
    void Start()
    {

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PLRDeathBehavior>();    

    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
		if(iceEffect != null)
		{
			Color iceEffectColor = iceEffect.color;
			iceEffectColor.a = (1 - player.getHealth()/100) * maxIceAlpha;
			iceEffect.color = iceEffectColor;
		} else
			Debug.Log("HealthDecay.FixedUpdate: No ice effect!");

		if(iceEffect != null)
		{
			;
			vignetteEffect.color = new Color(0, 0, 0, (1 - player.getHealth()/100) * maxVignetteAlpha);
		} else
			Debug.Log("HealthDecay.FixedUpdate: No vignette effect!");

    }
}
