using UnityEngine;
using UnityEngine.SceneManagement;

// Shows the Game Over screen when the player dies. It goes on the Player, next to PlayerHealth.
public class DeathHandler : MonoBehaviour
{
    // The canvas with the Game Over message and buttons. It starts hidden.
    [SerializeField] Canvas gameOverCanvas;

    // Runs once when the game starts.
    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    // Called by PlayerHealth when the player's hit points reach zero.
    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;

        // Freeze the game so the raptors stop moving.
        Time.timeScale = 0;

        // Turn off the gun so clicking the buttons doesn't fire it.
        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.enabled = false;
        }

        // Unlock and show the mouse so the player can click the buttons.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
}
