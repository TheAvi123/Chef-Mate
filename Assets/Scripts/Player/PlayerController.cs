using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Reference Variables
    private Rigidbody2D rigidBody = null;

    //Configuration Parameters
    [Header("Movement and Rotation")]
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float rotationSpeed = 720f;
    [SerializeField] Vector2 startPosition = new Vector2(0f, 0f);

    [Header("Movement and Rotation")]
    [SerializeField] float dashCooldown = 5f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashSpeed = 50f;

    //Rotation State Variables
    private Vector2 mouseDirection;
    private float lookAngle = 0;

    //Dash State Variables
    private Coroutine dashCoroutine = null;
    private Vector2 dashDirection;
    private float dashTimer = 0f;
    private bool dashing = false;

    //Internal Methods
    private void Awake() {
        FindRigidBody();
    }

    private void FindRigidBody() {
        rigidBody = GetComponent<Rigidbody2D>();
        if (!rigidBody) {
            Debug.LogWarning("No RigidBody Component Found On Player");
            enabled = false;
        }
    }

    private void Start() {
        MoveToStartPosition();
    }

    private void MoveToStartPosition() {
        rigidBody.MovePosition(startPosition);
    }

    private void FixedUpdate()
    {
        UpdateLookAngle();
        RotateTowardsMouse();
        UpdatePlayerVelocity();
        TriggerDashOnInput();
    }

    private void UpdateLookAngle() {
        float mouseXPosition = Input.mousePosition.x;
        float mouseYPosition = Input.mousePosition.y;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(new Vector2(mouseXPosition, mouseYPosition));
        mouseDirection = (mousePosition - (Vector2)transform.position).normalized;
        lookAngle = (Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg) - 90;
    }

    private void RotateTowardsMouse() {
        rigidBody.angularVelocity = 0f;
        float maxRotationStep = rotationSpeed * Time.fixedDeltaTime;
        Quaternion newRotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, maxRotationStep);
    }

    private void UpdatePlayerVelocity() {
        if (dashing) {
            rigidBody.velocity = dashDirection * dashSpeed;
        } else {
            float deltaX = Input.GetAxisRaw("Horizontal");
            float deltaY = Input.GetAxisRaw("Vertical");
            Vector2 newVelocity = new Vector2(deltaX, deltaY) * Time.fixedDeltaTime;
            newVelocity.Normalize();
            rigidBody.velocity = newVelocity * moveSpeed;
        }
    }

    private void TriggerDashOnInput() {
        if (dashTimer > 0f) {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0f) {
                StopCoroutine(dashCoroutine);
                dashCoroutine = null;
            }
        } else {
            if (Input.GetAxisRaw("Dash") == 1 && dashCoroutine == null) {
                dashCoroutine = StartCoroutine(Dash());
                dashTimer = dashCooldown;
            }
        }
    }

    private IEnumerator Dash() {
        dashing = true;
        dashDirection = mouseDirection;
        yield return new WaitForSeconds(dashDuration);
        dashing = false;
    }

    //Public Methods
    public Vector2 GetMouseDirection() {
        return mouseDirection;
    }
}
