<<<<<<< HEAD
﻿using UnityEngine;
=======
﻿using TMPro;
using UnityEngine;
>>>>>>> dracoBranch

public class PlayerHealth : MonoBehaviour
{
    //Configuration Parameters
<<<<<<< HEAD
=======
    [SerializeField] TextMeshProUGUI healthDisplay = null;
>>>>>>> dracoBranch
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
<<<<<<< HEAD
=======
        UpdateHealthDisplay(false);
>>>>>>> dracoBranch
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

<<<<<<< HEAD
=======
    private void UpdateHealthDisplay(bool dead) {
        if (healthDisplay) {
            if (dead) {
                healthDisplay.text = "HEALTH: NONE";
            } else {
                healthDisplay.text = "HEALTH: " + currentHealth.ToString();
            }
        }
    }

>>>>>>> dracoBranch
    //Public Methods
    public void DamagePlayer(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            KillPlayer();
<<<<<<< HEAD
=======
            UpdateHealthDisplay(true);
        } else {
            UpdateHealthDisplay(false);
>>>>>>> dracoBranch
        }
    }
}
