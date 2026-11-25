using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ends the level at the control tower's gate controls, once the generator is running.
// Goes on the controls (or an empty object next to them) with a Box Collider that has Is Trigger checked.
public class LevelComplete : MonoBehaviour
{
    // The generator that has to be running before the controls work.
    [SerializeField] Generator generator;
    // The canvas with the Level Complete message and buttons. It starts hidden.
    [SerializeField] Canvas levelCompleteCanvas;
    // On-screen text used to tell the player the controls have no power.
    [SerializeField] TextMeshProUGUI messageText;

    // Runs once when the game starts.
    void Start()
    {
        levelCompleteCanvas.enabled = false;
    }

    // Runs when something walks into the trigger at the controls.
    private void OnTriggerEnter(Collider other)
    {
        // Only the player has a PlayerInventory, so anything else is ignored.
        if (other.GetComponent<PlayerInventory>() == null) { return; }

        if (generator != null && !generator.IsRunning())
        {
            ShowMessage("The controls have no power.");
            return;
        }

        ShowMessage("");
        levelCompleteCanvas.enabled = true;

        // Freeze the game so the raptors stop moving.
        Time.timeScale = 0;

        // Unlock and show the mouse so the player can click the buttons.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Runs when something walks back out of the trigger. Clears the message.
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() == null) { return; }
        ShowMessage("");
    }

    // Hook this to a Restart button's On Click ().
    // The scene must be in File > Build Profiles > Scene List.
    public void ReloadLevel()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Hook this to a Quit button's On Click (). Only works in a built game.
    public void QuitGame()
    {
        Application.Quit();
    }

    // Changes the on-screen message. Does nothing if no text is assigned.
    private void ShowMessage(string message)
    {
        if (messageText != null) { messageText.text = message; }
    }
}
