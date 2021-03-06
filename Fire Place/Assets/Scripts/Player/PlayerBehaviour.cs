﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace FirePlace
{	
		
	public class PlayerBehaviour : MonoBehaviour
	{

		public static PlayerBehaviour instance;

		public enum States { Default, Lendo, Fogueira, Pausado, Cutscene, End };

		private States state;
		public void setState(States state) { this.state = state; }
		public States getState() { return state; }

		private States previousState = States.Default;

		[Header("Movement")]
		public float velocity = 8.5f;
		public float turningSpeed = 0.5f;
		public float gravityScale = 1;

		private bool gotDir = false;
		private Vector2 previousInput = Vector2.zero;
		private Vector3 previousPos = Vector2.zero;

		private Transform mainCamera;
		public Transform cameraFollowPoint = null;

		private Vector3 cameraForward;
		private Vector3 cameraRight;

		// Health too.
		private float health = 100;
		public float getHealth() { return health; }

		[Header("Health")]
		public bool allowDeath = true;
		public float iceDamageMultiplier = 1;

		private bool takingDamage = true;
		public bool TakingDamage
		{ 
			get { return takingDamage; }
		}
		
		// UI &HUD too.
		[System.Serializable]
		public class Paused
		{

			public GameObject menu;
			public Button initialSelection;
			public EventSystem eventSystem;

		}
		[Header("Pause")]
		public Paused paused = new Paused();

		[System.Serializable]
		public class HUD
		{

			public PlayerBehaviour parent;

			public GameObject notesObject = null;
			public Image noteImage = null;

			public AudioSource noteSound = null;

			public Image interactIcon = null;

			public void UpdateIconPosition(GameObject target, Vector2 iconOffset) 
			{

				Vector3 pos = UnityEngine.Camera.main.WorldToScreenPoint(target.transform.position);
				pos = new Vector3(pos.x + iconOffset.x, pos.y + iconOffset.y, 0);

				interactIcon.gameObject.transform.position = pos;

			}

		}
		public HUD hud = new HUD();

		[System.Serializable]
		public class IceEffect
		{

			public PlayerBehaviour parent;

			public Image ice = null;
			public float maxIceAlpha = 0.9f;

			public Image vignette = null;	
			public float maxVignetteAlpha = 0.6f;

			public void UpdateEffect() 
			{

				if(ice != null)
				{

					Color iceEffectColor = ice.color;
					iceEffectColor.a = (1 - parent.getHealth()/100) * maxIceAlpha;
					ice.color = iceEffectColor;

				} else
					Debug.Log("HealthDecay.FixedUpdate: No ice effect!");

				if(ice != null)
					vignette.color = new Color(0, 0, 0, (1 - parent.getHealth()/100) * maxVignetteAlpha);
				else
					Debug.Log("HealthDecay.FixedUpdate: No vignette effect!");

			}

		}
		[SerializeField] private IceEffect iceEffect = new IceEffect();

		[Header("Physics")]
		public Rigidbody _rigidbody;
		public Collider _collider;

		[Header("Animation")]
		public Animator _animator;
		public float standUpTime = 2.0f;
		
		[Header("Navigation")]
		public NavMeshAgent _navigation;
		public float distanceToDestinationTolerance = 0.2f;

		void Awake()
		{

			if(instance != null)
				Debug.Log("PlayerBehaviour.Awake: More than one PlayerBehaviout instance found!");
			else
				instance = this;


			mainCamera = UnityEngine.Camera.main.transform;

			_collider.enabled = false;

			_navigation.enabled = false;

			state = States.Cutscene;
			
		}

		void Update()
		{

			switch (state)
			{
				case States.Default:

					// PAUSE ---------------------------------------------------------------------------
					if (Input.GetKeyDown(KeyCode.Escape))
					{
						Pause();
						return;
					}

					// COLD ----------------------------------------------------------------------------------
					if (takingDamage)
					{
						if (health > 40) // Does more damage at start.
							health -= 3 * iceDamageMultiplier * Time.deltaTime;
						else if(health > 0) // Does less damage when health is ending.
							health -= iceDamageMultiplier * Time.deltaTime;
						else if(health < 0) // Clamps the life if it's lesser than 0.
							health = 0;

					} else { // Restores Player health.

						if (health < 100) // Quickly restores health.
							health += 100 * iceDamageMultiplier * Time.deltaTime;
						else if(health > 100) // Clamps the life if it's larger than 100.
							health = 100;

					}

					// Allows the interact icon to appear.
					hud.interactIcon.gameObject.SetActive(true);

					if (health == 0 && allowDeath)
						Debug.Log("Dead");

					break;

				case States.Lendo:

					// Disallows the interact icon to appear.
					hud.interactIcon.gameObject.SetActive(false);

					// Ensures the Player is not walking.
					_animator.SetBool("isWalking", false);

					break;

				case States.Fogueira:

					// PAUSE ---------------------------------------------------------------------------
					if (Input.GetKeyDown(KeyCode.Escape))
					{
						Pause();
						return;
					}

					// Disallows the interact icon to appear.
					hud.interactIcon.gameObject.SetActive(false);

					if (health < 100) // Quickly restores health.
						health += 200 * iceDamageMultiplier * Time.deltaTime;
					else if(health > 100) // Clamps the life if it's larger than 100.
						health = 100;

					break;

				case States.Pausado:

					// UNPAUSE ---------------------------------------------------------------------------
					if (Input.GetKeyDown(KeyCode.Escape))
					{
						Unpause();
						return;
					}

					// Disallows the interact icon to appear.
					hud.interactIcon.gameObject.SetActive(false);

					break;

				case States.Cutscene:

					// Disallows the interact icon to appear.
					hud.interactIcon.gameObject.SetActive(false);

					break;

				case States.End:

					// Ensures the Player is not walking.
					_animator.SetBool("isWalking", false);

					break;

			}

			iceEffect.UpdateEffect();

		}

		void FixedUpdate()
		{
			switch (state)
			{
				case States.Default:

					// MOVEMENT ---------------------------------------------------------------------------

					// Plays the animation at the start of the fram eso any physics calculations that may change the velocity from the previous fram have already been performed.
					if(new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.z).magnitude >= 0.5f) _animator.SetBool("isWalking", true);
					else _animator.SetBool("isWalking", false);

					Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

					float yVel = _rigidbody.velocity.y;

					if(!gotDir) {

						cameraRight = mainCamera.right;
						cameraForward = mainCamera.forward;

						gotDir = true;

					} if(inputAxis != previousInput) gotDir = false;

					if(inputAxis != Vector2.zero)
					{
						Vector3 dir = ((cameraRight * inputAxis.x) + (cameraForward * inputAxis.y)).normalized;

						_rigidbody.velocity = new Vector3(dir.x  * velocity, yVel, dir.z * velocity);

						if(_rigidbody.useGravity)
							_rigidbody.velocity += Physics.gravity * Time.deltaTime;

						previousInput = inputAxis;

						// ROTATION ----------------------------------------------------------------------------------
						Vector3 projection = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
						float yAngle = Vector3.SignedAngle(Vector3.forward, projection, Vector3.up);

						if (_rigidbody.velocity.magnitude > 0.5f)
							transform.eulerAngles = new Vector3(0, yAngle, 0);

					}

					break;

				case States.Fogueira:

					if(Vector3.Distance(transform.position, previousPos) / Time.fixedDeltaTime > 0.1f) _animator.SetBool("isWalking", true);
					else _animator.SetBool("isWalking", false);

					break;

				case States.Cutscene:

					if(Vector3.Distance(transform.position, previousPos) / Time.fixedDeltaTime > 0.1f) _animator.SetBool("isWalking", true);
					else _animator.SetBool("isWalking", false);

					break;

			}

			previousPos = transform.position;

		}

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Fire")
				takingDamage = false;
		}

		void OnTriggerExit(Collider other)
		{
			if (other.tag == "Fire")
				takingDamage = true;
		}

		public void Sit(Vector3 targetPos, float facingY)
		{
			if (state != States.Fogueira)
			{
				state = States.Fogueira;

				_collider.enabled = false;
				_navigation.enabled = true;

				_rigidbody.velocity = Vector3.zero;
				StartCoroutine(SitState(targetPos, facingY));

			}
		}

		private IEnumerator SitState(Vector3 targetPos, float facingY)
		{

			_navigation.SetDestination(targetPos);

			while(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPos.x, 0, targetPos.z)) > distanceToDestinationTolerance)
				yield return new WaitForFixedUpdate();

			_collider.enabled = true;
			_navigation.enabled = false;

			Quaternion initialRotation = transform.rotation;
			Quaternion finalRotation = Quaternion.Euler(new Vector3(0, facingY, 0));
			float time = 0;

			while(time < 1)
			{

				time += Time.deltaTime * turningSpeed;
				transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, time);
				
				yield return new WaitForEndOfFrame();

			}

			_animator.SetBool("isSeated", true);

			while(state == States.Fogueira || state == States.Pausado)
			{

				if(state == States.Fogueira)
				{
					if (Input.GetButtonDown("Use") || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
					{
						_animator.SetBool("isSeated", false);

						time = 0;
						while(time < standUpTime)
						{
							time += Time.deltaTime;	
							yield return new WaitForEndOfFrame();
						}

						state = States.Default;

					}
				}

				yield return new WaitForEndOfFrame();

			}

		}

		public void Cutscene(Vector3 targetPos,  bool endCutscene)
		{
			if (state != States.Cutscene)
			{
				state = States.Cutscene;

				_collider.enabled = false;
				_navigation.enabled = true;

				_rigidbody.velocity = Vector3.zero;
				StartCoroutine(CutsceneState(targetPos, endCutscene));

			}
		}

		private IEnumerator CutsceneState(Vector3 targetPos, bool endCutscene)
		{

			_navigation.SetDestination(targetPos);
			while(Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPos.x, 0, targetPos.z)) > distanceToDestinationTolerance)
				yield return new WaitForFixedUpdate();

			_navigation.isStopped = true;
 			_navigation.ResetPath();
			 _rigidbody.velocity = Vector3.zero;

			if(endCutscene)
				state = States.End;

		}

		public void Pause()
		{
			previousState = state;
			state = States.Pausado;
			Time.timeScale = 0;

			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			paused.menu.SetActive(true);
			paused.eventSystem.SetSelectedGameObject(paused.initialSelection.gameObject);
			paused.initialSelection.OnSelect(null);

		}

		public void Unpause()
		{
			state = previousState;
			Time.timeScale = 1;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			paused.menu.SetActive(false);

		}

	}

}
