using UnityEngine;

public class Condiment : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] GameObject condimentPrefab = null;
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileSpeed = 10f;

    //State Variables
    private Vector2 moveDirection = new Vector2(0, 0);
    
    //Internal Methods
    private void FixedUpdate()
    {
        MoveCondimentProjectile();
    }

    private void MoveCondimentProjectile() {
        Vector2 displacement = moveDirection * projectileSpeed * Time.fixedDeltaTime;
        transform.position = (Vector2) transform.position + displacement;
    }

    //Public Methods
    public GameObject GetCondimentPrefab() {
        return condimentPrefab;
    }

    public float GetShootDelay() {
        return shootDelay;
    }

    public void SetMoveDirection(Vector2 direction) {
        moveDirection = direction;
    }
}
