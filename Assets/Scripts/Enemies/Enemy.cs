using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Reference Variables
    private PlayerHealth player = null;

    //Configuration Parameters
    [SerializeField] int enemyHealth = 100;
    [SerializeField] int enemyDamage = 100;
    [SerializeField] int enemyScore = 100;
    [SerializeField] GameObject deathVFX = null;

    //State Variables
    private int currentHealth;

    //Internal Methods
    private void Awake() {
        FindPlayer();
    }

    private void FindPlayer() {
        player = FindObjectOfType<PlayerHealth>();
        if (!player) {
            Debug.LogWarning("No Player Found To Follow In Scene");
            enabled = false;
        }
    }

    private void Start() {
        InitializeHealth();
    }

    private void InitializeHealth() {
        currentHealth = enemyHealth;
    }

    private void KillEnemy() {
        Destroy(gameObject);
        SpawnDeathParticles();
        StatsManager.sharedInstance.AddEnemyKill();
        StatsManager.sharedInstance.AddScore(enemyScore);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            DamagePlayer();
            Destroy(gameObject);
        }
    }

    private void SpawnDeathParticles() {
        GameObject particles = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(particles, 1f);
    }

    private void DamagePlayer() {
        player.DamagePlayer(enemyDamage);
    }

    //Public Methods
    public void DamageEnemy(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            KillEnemy();
        }
    }
}
