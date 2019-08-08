using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalCutSceneScript : MonoBehaviour
{
	public enum State { None, Middle, Final };

	public GameObject player, sceneStart, sceneEnd;
	private State state;
	private Rigidbody playerRgbd;
	private float playerAcceleration, playerMaxVelocity;

	public GameObject blackPlanel;

	public GameObject[] finalTexts;

	private float totalDist, dist, alpha, secondsBetweenTexts = 5;
	public float tolDistToDest = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
		state = State.None;

		playerRgbd = player.GetComponent<Rigidbody>();
		playerAcceleration = 2.0f;
		playerMaxVelocity = player.GetComponent<PlayerBehaviour>().velocity;
	}

    // Update is called once per frame
    void Update()
    {
		if (sceneStart == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");

			sceneStart = GameObject.FindGameObjectWithTag("StartTrigger");
			sceneEnd = GameObject.FindGameObjectWithTag("EndTrigger");

			totalDist = Vector3.Magnitude(sceneStart.transform.position - sceneEnd.transform.position);
			totalDist *= 1.1f;
		}

		if (state == State.Middle)
		{
			dist = Vector3.Magnitude(player.transform.position - sceneEnd.transform.position);

			alpha = 1 - dist / totalDist;

			blackPlanel.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
		}
    }

	public void SetStart()
	{
		state = State.Middle;

		player.GetComponent<PlayerBehaviour>().setControllable(false);
		player.GetComponent<PlayerBehaviour>().Sit(sceneEnd.transform.position, 0);
		//StartCoroutine(WalkToEnd(sceneEnd.transform.position));

	}

	public void SetEnd()
	{
		state = State.Final;
		StartCoroutine(FinalCredits());
	}

	private IEnumerator FinalCredits()
	{
		player.GetComponent<PlayerBehaviour>().enabled = false;

		blackPlanel.GetComponent<Image>().color = new Color(0, 0, 0, 1);

		for (int i = 0; i < finalTexts.Length; i++)
		{
			StartCoroutine(ChangeAlpha(finalTexts[i], 1, 0.5f));

			yield return new WaitForSeconds(secondsBetweenTexts+1);

			StartCoroutine(ChangeAlpha(finalTexts[i], 0, 0.5f));

		}

		//return null;
	}

	private IEnumerator ChangeAlpha(GameObject target, float finalAlpha, float time)
	{
		float alphaTol = 0.05f;
		float iniAlpha = target.GetComponent<Text>().color.a;
		float totalAlpha = Mathf.Abs(iniAlpha - finalAlpha);

		if(finalAlpha > iniAlpha)
		{
			while (finalAlpha - target.GetComponent<Text>().color.a > alphaTol)
			{
				Debug.Log("Loop called");
				target.GetComponent<Text>().color += new Color(0, 0, 0, (totalAlpha * Time.deltaTime) / time);
				Debug.Log("Target name = " + target.name);
				yield return new WaitForFixedUpdate();
			}
		}
		else
		{
			while (target.GetComponent<Text>().color.a - finalAlpha > alphaTol)
			{
				target.GetComponent<Text>().color -= new Color(0, 0, 0, (totalAlpha * Time.deltaTime) / time);
				yield return new WaitForFixedUpdate();
			}
		}

		target.GetComponent<Text>().color = new Color(target.GetComponent<Text>().color.r, target.GetComponent<Text>().color.g, target.GetComponent<Text>().color.b, finalAlpha);
	}

	/*private IEnumerator WalkToEnd(Vector3 keyPos)
	{
		Vector3 distance = keyPos - transform.position;             //calcula o vetor distancia
		distance = distance.normalized;                             //normaliza
		distance = new Vector3(distance.x, 0, distance.z);          //projeta no plano

		while ((transform.position - keyPos).magnitude > tolDistToDest)             //enquanto a distancia até o destino for maior que a tolerancia
		{
			if (Vector3.Magnitude(playerRgbd.velocity) <= playerMaxVelocity)            //aplica uma força no player para andar
			{
				playerRgbd.AddForce(distance * playerAcceleration, ForceMode.Force);
			}
			yield return null;
		}
	}*/
}
