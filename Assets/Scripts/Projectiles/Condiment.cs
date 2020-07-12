using UnityEngine;

public class Condiment : MonoBehaviour
{
    enum CondimentType {Ketchup, HotSauce, Mustard, Mayonaisse, Relish};

    //Reference Variables
    private new Rigidbody2D rigidbody = null;

    //Configuration Parameters
    [Header("Condiment Parameters")]
    [SerializeField] GameObject condimentPrefab = null;
    [SerializeField] Color condimentColor = Color.white;
    [SerializeField] int condimentDamage = 25;

    [Header("Shooting")]
    [SerializeField] float shootDelay = 1f;
    [SerializeField] float projectileSpeed = 10f;
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

    private void SetProjectileRotation() {
        float lookAngle = (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) - 180;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
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
        if (otherCollider.tag == "Obstacle") {
            SpawnCollisionParticles();
            Destroy(gameObject);
        } else if (otherCollider.tag == "Enemy") {
            DamageEnemy(otherCollider.GetComponent<Enemy>());
            SpawnCollisionParticles();
            Destroy(gameObject);
        }
    }

    private void SpawnCollisionParticles() {
        GameObject particles = Instantiate(collisionVFX, transform.position, transform.rotation) as GameObject;
        particles.transform.SetParent(gameObject.transform.parent);
        ParticleSystem.MainModule particleSettings = particles.GetComponent<ParticleSystem>().main;
        particleSettings.startColor = condimentColor;
        Destroy(particles, 1f);
    }

    private void DamageEnemy(Enemy enemy) {
        enemy.DamageEnemy(condimentDamage);
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
        SetProjectileRotation();
    }
}
