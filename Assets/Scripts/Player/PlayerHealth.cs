using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] int playerHealth = 100;
    [SerializeField] GameObject deathVFX = null;

    //State Variables
    private int currentHealth;

    //Internal Methods
    private void Start() {
        InitializeHealth();
    }

    private void InitializeHealth() {
        currentHealth = playerHealth;
    }

    private void KillPlayer() {
        Destroy(gameObject);
        SpawnDeathParticles();
        GameStateManager.sharedInstance.GameOver(2f);
    }

    private void SpawnDeathParticles() {
        GameObject particles = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(particles, 1f);
    }

    //Public Methods
    public void DamagePlayer(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            KillPlayer();
        }
    }
}
