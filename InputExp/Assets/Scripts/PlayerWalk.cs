using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
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
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

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

    public void Jump()
    {
        m_rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
        m_animator.SetTrigger("Jump");
    }


    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }

    private void Walking()
    {
        // Move only forward/backward relative to the player's facing direction
        Vector3 moveDirection = transform.forward * m_moveAmt.y;

        // Move the player
        m_rigidbody.MovePosition(m_rigidbody.position + moveDirection * WalkSpeed * Time.deltaTime);

        // Set speed in animator based only on forward/backward movement
        m_animator.SetFloat("Speed", Mathf.Abs(m_moveAmt.y));
    }


    private void Rotating()
    {
        if (Mathf.Abs(m_lookAmt.x) > 0.01f) // Optional: Only rotate if there's actual look input
        {
            float rotationAmount = m_lookAmt.x * RotateSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation);
        }
    }


}



