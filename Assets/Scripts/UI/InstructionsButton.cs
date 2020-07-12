using UnityEngine;

public class InstructionsButton : MonoBehaviour
{
    public void LoadInstructions() {
        GameStateManager.sharedInstance.LoadInstructions();
    }
}
