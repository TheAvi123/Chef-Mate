using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] TextMeshProUGUI healthDisplay = null;
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
        UpdateHealthDisplay(false);
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

    private void UpdateHealthDisplay(bool dead) {
        if (healthDisplay) {
            if (dead) {
                healthDisplay.text = "HEALTH: NONE";
            } else {
                healthDisplay.text = "HEALTH: " + currentHealth.ToString();
            }
        }
    }

    //Public Methods
    public void DamagePlayer(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            KillPlayer();
            UpdateHealthDisplay(true);
        } else {
            UpdateHealthDisplay(false);
        }
    }
}
