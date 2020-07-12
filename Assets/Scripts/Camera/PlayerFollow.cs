using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    //Reference Variables
    private PlayerController player = null;

    //Configuration Parameters
    [SerializeField] int cameraOffset = -50;
    
    //Internal Methods
    private void Awake()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>();
        if (!player) {
            Debug.Log("No Player Found To Follow In Scene");
            enabled = false;
        }
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer() {
        Vector3 playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, cameraOffset);
    }
}
