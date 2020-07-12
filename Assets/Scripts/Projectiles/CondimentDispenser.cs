using UnityEngine;

public class CondimentDispenser : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] Condiment.CondimentType condimentType;

    //Internal Methods
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<CondimentShooter>().SwitchCondiments((int) condimentType);
        }
    }
}
