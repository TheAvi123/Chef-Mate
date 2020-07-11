using UnityEngine;

public class Condiment : MonoBehaviour
{
    //Reference Variables
    private new Rigidbody2D rigidbody = null;

    //Configuration Parameters
    [SerializeField] GameObject condimentPrefab = null;
    [SerializeField] Color condimentColor = Color.white;
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Collisions")]
    [SerializeField] GameObject collisionVFX = null;

    //State Variables
    private Vector2 moveDirection = new Vector2(0, 0);

    //Internal Methods
    private void Awake() {
        FindRigidBody();
    }

    private void FindRigidBody() {
        rigidbody = GetComponent<Rigidbody2D>();
        if (!rigidbody) {
            Debug.LogWarning("No RigidBody Component Found On Condiment Projectile");
            enabled = false;
        }
    }

    private void FixedUpdate() {
        MoveCondimentProjectile();
    }

    private void MoveCondimentProjectile() {
        rigidbody.velocity = moveDirection * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        ManageTriggerCollisions(otherCollider);
    }

    private void ManageTriggerCollisions(Collider2D otherCollider) {
        if (otherCollider.tag == "Wall") {
            SpawnCollisionParticles();
            Destroy(gameObject);
        } else if (otherCollider.tag == "Enemy") {
            SpawnCollisionParticles();
            Destroy(gameObject);
        }
    }

    private void SpawnCollisionParticles() {
        GameObject particles = Instantiate(collisionVFX, transform.position, transform.rotation) as GameObject;
        ParticleSystem.MainModule particleSettings = particles.GetComponent<ParticleSystem>().main;
        particleSettings.startColor = condimentColor;
        Destroy(particles, 1f);
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
