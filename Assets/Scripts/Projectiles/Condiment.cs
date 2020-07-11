using UnityEngine;

public class Condiment : MonoBehaviour
{
    //Reference Variables

    //Configuration Parameters
    [SerializeField] GameObject condimentPrefab = null;
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileSpeed = 10f;

    //State Variables
    private Vector2 moveDirection = new Vector2(0, 0);
    
    //Internal Methods
    private void Update()
    {
        MoveCondimentProjectile();
    }

    private void MoveCondimentProjectile() {
        Vector2 displacement = moveDirection * projectileSpeed * Time.deltaTime;
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
