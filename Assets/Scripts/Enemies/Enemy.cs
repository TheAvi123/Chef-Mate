using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Reference Variables
    private PlayerController player = null;

    //Configuration Parameters


    //State Variables


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

    private void Update()
    {
        
    }
}
