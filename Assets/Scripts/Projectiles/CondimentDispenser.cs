using UnityEngine;

public class CondimentDispenser : MonoBehaviour
{
    //Reference Variables

    //Configuration Parameters

    //State Variables

    //Internal Methods
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Debug.Log("Change");
            collision.GetComponent<CondimentShooter>().SwitchCondimentsRandomly();
        }
    }
}
