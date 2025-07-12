using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Walk : MonoBehaviour
{
	public InputActionAsset InputActions;

	private InputAction m_moveAction;
	private InputAction m_lookAction;
	private InputAction m_jumpAction;

	private Vector2 m_moveAmt;
	private Vector2 m_lookAmt;
	private Animator m_animator;
	private Rigidbody m_rigidbody;

	public float WalkSpeed = 5;
	public float RotateSpeed = 5;
	public float JumpSpeed = 5;

	private void OnEnable()
	{
		InputActions.FindActionMap("Player").Enable();		
	}

	private void OnDisable()
	{
		InputActions.FindActionMap("Player").Disable();
	}

	private void Awake()
	{
		m_moveAction = InputActions.FindAction("Move");
		m_lookAction = InputActions.FindAction("Look");
		m_jumpAction = InputActions.FindAction("Jump");


		m_animator = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		m_moveAmt = m_moveAction.ReadValue<Vector2>();
		m_lookAmt = m_lookAction.ReadValue<Vector2>();

		if (m_jumpAction.WasPressedThisFrame())
		{
			Jump();
		}
	}

	private void Jump()
	{	
		//Add force to the rigidbody to push it up into the air
		m_rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
		//Play the jump animation in the animator
		m_animator.SetTrigger("Jump");
	}

	private void FixedUpdate()
	{
		Walking();
		Rotating();
	}

	private void Walking()
	{
		//Adjust the speed in the animator to match the move amount input
		m_animator.SetFloat("Speed", m_moveAmt.y);
		//Move the player forward or back using the move amount input
		m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * WalkSpeed * Time.deltaTime);
	}

	private void Rotating()
	{
		// Only rotate if there is movement input
		if (m_moveAmt.y != 0)
		{
			// Calculate the rotation amount based on look input
			float rotationAmount = m_lookAmt.x * RotateSpeed * Time.deltaTime;

			// Apply the rotation to the rigidbody
			Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
			m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation);
		}
	}

}
