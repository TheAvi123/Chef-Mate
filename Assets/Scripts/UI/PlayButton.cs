using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void PlayGame() {
        GameStateManager.sharedInstance.PlayGame();
    }
}
