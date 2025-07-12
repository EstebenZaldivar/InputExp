using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
	private bool m_isFlying;
    private Animator m_animator;
    private Rigidbody m_rigidbody;

	public float WalkSpeed = 5;
	public float RotateSpeed = 2.5f;
	public float BoostAmt = 15;
	public float MoveSpeed = 5;
	public float FlySpeed = 2.5f;
	public float GamepadRotateSpeed = 25f;
	public GameObject[] Flames;
	public GameObject Player1Camera;

	private bool m_grounded;
	private float m_groundedRadius = 0.28f;
	private float m_groundedOffset = -0.14f;
	public LayerMask GroundLayers;

	void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
		CheckForListener();
		//Adjust rotation speeds for gamepads
		if (GetComponent<PlayerInput>().currentControlScheme == "Gamepad")
			RotateSpeed = GamepadRotateSpeed;
		//Switches off the flames of the rocket pack when the game begins
		for (int i = 0; i < Flames.Length; i++)
		{
			Flames[i].SetActive(false);
		}
	}








	//Start flying by Setting the flying bools to true and switches on the rocket pack flames
	private void StartFlying()
	{
		m_isFlying = true;
		m_animator.SetBool("IsFlying", true);
		m_animator.SetBool("Grounded", false);
		for (int i = 0; i < Flames.Length; i++)
		{
			Flames[i].SetActive(true);
		}
	}

	//Stops flying by setting the bools to false and switching off the rocket pack flames
	private void StopFlying()
	{
		m_isFlying = false;
		m_animator.SetBool("IsFlying", false);
		for (int i = 0; i < Flames.Length; i++)
		{
			Flames[i].SetActive(false);
		}
	}

	private void FixedUpdate()
	{
        Walking();
        Rotating();
		Flying();
		GroundedCheck();
	}

	//Handles the movement logic while walking or flying
	private void Walking()
	{
		m_animator.SetFloat("Speed", m_moveAmt.y);
		if (!m_isFlying)
		{
			m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime);
		}
		else
		{
			m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * FlySpeed * Time.deltaTime);
		}
	}

	//Handles the rotation look logic
	private void Rotating()
	{
		// Calculate the rotation amount based on look input
		float rotationAmount = m_lookAmt.x * RotateSpeed * Time.deltaTime;

		// Apply the rotation to the rigidbody
		Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
		m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation);
	}

	//Handles the flying logic by adding force on the Y position to boost the player into the air
	private void Flying()
	{
		if (m_isFlying)
		{
			m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime);
			m_rigidbody.AddForceAtPosition(new Vector3(0, BoostAmt * Time.deltaTime, 0), Vector3.up, ForceMode.Impulse);
		}
	}

	//A custom function to switch on the player camera for local multiplayer co-op games
	public void SwitchOnCamera()
	{
		Player1Camera.SetActive(true);
	}

	//Checks when the player lands on the ground to reset the flying animation
	private void GroundedCheck()
	{
		// set sphere position, with offset
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - m_groundedOffset,
			transform.position.z);
		m_grounded = Physics.CheckSphere(spherePosition, m_groundedRadius, GroundLayers,
			QueryTriggerInteraction.Ignore);
		m_animator.SetBool("Grounded", m_grounded);

	}

	//Destroys the Audio Listener on the camera when there are multiple players in the scene
	private void CheckForListener()
	{
		AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
		if (listeners.Length > 1)
		{
			Destroy(listeners[1]);
		}
		
	}

}
