using TMPro;
using UnityEngine;

// Step two of getting the power back. Once the fuse is in, walking up to the generator
// restarts it and brings the power back.
// Needs a collider with Is Trigger checked, bigger than the generator.
public class Generator : MonoBehaviour
{
    // The fuse box that has to have its fuse in before the generator will start.
    [SerializeField] FuseBox fuseBox;
    // Lights that turn on when the generator starts. The script turns them off when the game starts.
    [SerializeField] GameObject[] powerLights;
    // Lights that turn off when the generator starts, such as red emergency lights.
    [SerializeField] GameObject[] emergencyLights;
    // On-screen text used to tell the player what to do.
    [SerializeField] TextMeshProUGUI messageText;
    // Optional Audio Source on the generator for its running hum. Turn on Loop, turn off Play On Awake.
    [SerializeField] AudioSource generatorSound;

    // True once the generator is running.
    private bool isRunning = false;

    // Runs once when the game starts.
    void Start()
    {
        SetLights(powerLights, false);
        ShowMessage("");
    }

    // Lets the control tower ask whether the power is back.
    public bool IsRunning()
    {
        return isRunning;
    }

    // Runs when something walks into the trigger around the generator.
    private void OnTriggerEnter(Collider other)
    {
        if (isRunning) { return; }

        // Only the player has a PlayerInventory, so anything else is ignored.
        if (other.GetComponent<PlayerInventory>() == null) { return; }

        if (fuseBox != null && fuseBox.IsFuseInstalled())
        {
            StartGenerator();
        }
        else
        {
            ShowMessage("It won't start. The fuse box is missing a fuse.");
        }
    }

    // Runs when something walks back out of the trigger. Clears the message.
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() == null) { return; }
        ShowMessage("");
    }

    // Everything that happens when the generator starts.
    private void StartGenerator()
    {
        isRunning = true;
        ShowMessage("Power restored. Head to the control tower.");

        SetLights(powerLights, true);
        SetLights(emergencyLights, false);

        if (generatorSound != null)
        {
            generatorSound.Play();
        }
    }

    // Turns a whole group of lights on or off.
    private void SetLights(GameObject[] lights, bool isOn)
    {
        foreach (GameObject lightObject in lights)
        {
            if (lightObject != null) { lightObject.SetActive(isOn); }
        }
    }

    // Changes the on-screen message. Does nothing if no text is assigned.
    private void ShowMessage(string message)
    {
        if (messageText != null) { messageText.text = message; }
    }
}
