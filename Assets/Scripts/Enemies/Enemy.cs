using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Reference Variables
    private PlayerController player = null;

    //Configuration Parameters
    [SerializeField] int enemyHealth = 100;

    //State Variables
    private int currentHealth;

    //Internal Methods
    private void Awake() {
        FindPlayer();
    }

    private void FindPlayer() {
        player = FindObjectOfType<PlayerController>();
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
    }

    //Public Methods
    public void DamageEnemy(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            KillEnemy();
        }
    }
}
