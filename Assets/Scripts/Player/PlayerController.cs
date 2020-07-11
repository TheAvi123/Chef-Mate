using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Reference Variables

    //Configuration Parameters
    [SerializeField] float playerSpeed = 15f;
    [SerializeField] Vector2 startPosition = new Vector2(0, 0);

    //State Variables
    private Vector2 position;

    //Internal Methods
    private void Start() {
        MoveToStartPosition();
    }

    private void MoveToStartPosition() {
        position = startPosition;
        transform.position = position;
    }

    private void FixedUpdate()
    {
        UpdatePlayerPosition();
        MovePlayer();
    }

    private void UpdatePlayerPosition() {
        float xDisplacement = Input.GetAxisRaw("Horizontal");
        float yDisplacement = Input.GetAxisRaw("Vertical");
        Vector2 displacement = new Vector2(xDisplacement, yDisplacement).normalized;
        displacement *= playerSpeed * Time.deltaTime;
        position += displacement;
    }

    private void MovePlayer() {
        transform.position = position;
    }
}
