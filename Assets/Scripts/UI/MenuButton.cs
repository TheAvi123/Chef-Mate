using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void LoadMenu() {
        GameStateManager.sharedInstance.LoadStartMenu();
    }
}
