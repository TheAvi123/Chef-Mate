using UnityEngine;

public class CondimentDispenser : MonoBehaviour
{
<<<<<<< HEAD
    //Reference Variables

    //Configuration Parameters

    //State Variables
=======
    //Configuration Parameters
    [SerializeField] Condiment.CondimentType condimentType = 0;
>>>>>>> dracoBranch

    //Internal Methods
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
<<<<<<< HEAD
            Debug.Log("Change");
            collision.GetComponent<CondimentShooter>().SwitchCondimentsRandomly();
=======
            collision.GetComponent<CondimentShooter>().SwitchCondiments((int) condimentType);
>>>>>>> dracoBranch
        }
    }
}
