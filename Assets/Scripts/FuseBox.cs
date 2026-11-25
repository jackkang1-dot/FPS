using TMPro;
using UnityEngine;

// Step one of getting the power back. When the player walks up with the fuse, it goes in,
// and then the Generator can be restarted.
// Needs a second collider with Is Trigger checked, bigger than the box.
public class FuseBox : MonoBehaviour
{
    // Lights that turn on once the fuse is in, such as dim emergency lights in the electrical room.
    // The script turns them off when the game starts.
    [SerializeField] GameObject[] fuseLights;
    // On-screen text used to tell the player what to do.
    [SerializeField] TextMeshProUGUI messageText;
    // Optional sound that plays when the fuse goes in.
    [SerializeField] AudioClip fuseSound;

    // True once the fuse is in, so it can only be installed once.
    private bool isFuseInstalled = false;

    // Runs once when the game starts.
    void Start()
    {
        SetLights(fuseLights, false);
        ShowMessage("");
    }

    // Lets the Generator ask whether the fuse is in yet.
    public bool IsFuseInstalled()
    {
        return isFuseInstalled;
    }

    // Runs when something walks into the trigger around the box.
    private void OnTriggerEnter(Collider other)
    {
        if (isFuseInstalled) { return; }

        // Only the player has a PlayerInventory, so anything else is ignored.
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) { return; }

        if (inventory.HasFuse())
        {
            inventory.UseFuse();
            InstallFuse();
        }
        else
        {
            ShowMessage("The fuse is missing.");
        }
    }

    // Runs when something walks back out of the trigger. Clears the message.
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() == null) { return; }
        ShowMessage("");
    }

    // Everything that happens when the fuse goes in.
    private void InstallFuse()
    {
        isFuseInstalled = true;
        ShowMessage("Fuse installed. Restart the generator.");

        SetLights(fuseLights, true);

        if (fuseSound != null)
        {
            AudioSource.PlayClipAtPoint(fuseSound, transform.position);
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
