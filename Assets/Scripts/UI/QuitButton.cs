using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame() {
        GameStateManager.sharedInstance.QuitGame();
    }
}
