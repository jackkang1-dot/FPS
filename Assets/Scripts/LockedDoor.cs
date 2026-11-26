using TMPro;
using UnityEngine;

// A door that swings open when the player walks up with the key.
// Goes on the door's hinge: the door model itself if its pivot is on the hinge edge,
// or an empty object placed on the hinge with the door as its child.
// Needs a collider with Is Trigger checked, bigger than the door, so the player can walk into it.
public class LockedDoor : MonoBehaviour
{
    // How far the door swings open, in degrees. Use a negative number to swing the other way.
    [SerializeField] float openAngle = 90f;
    // How fast the door swings, in degrees per second.
    [SerializeField] float openSpeed = 90f;
    // On-screen text used to tell the player the door is locked.
    [SerializeField] TextMeshProUGUI messageText;
    // Optional sounds for unlocking and for trying the door without the key.
    [SerializeField] AudioClip unlockSound;
    [SerializeField] AudioClip lockedSound;

    // Which way the door faces when it's fully open.
    private Quaternion openRotation;
    // True once the door has been unlocked.
    private bool isOpen = false;

    // Runs once when the game starts.
    void Start()
    {
        // Swing around the world's up direction, so it works even if the model was built lying on its side.
        openRotation = Quaternion.AngleAxis(openAngle, Vector3.up) * transform.rotation;
    }

    // Runs once per frame. RotateTowards swings the door a little further each frame.
    void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, openRotation, openSpeed * Time.deltaTime);
        }
    }

    // Runs when something walks into the trigger around the door.
    private void OnTriggerEnter(Collider other)
    {
        if (isOpen) { return; }

        // Only the player has a PlayerInventory, so anything else is ignored.
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) { return; }

        if (inventory.HasKey())
        {
            isOpen = true;
            ShowMessage("");

            if (unlockSound != null)
            {
                AudioSource.PlayClipAtPoint(unlockSound, transform.position);
            }
        }
        else
        {
            ShowMessage("It's locked. The head custodian must have a key.");

            if (lockedSound != null)
            {
                AudioSource.PlayClipAtPoint(lockedSound, transform.position);
            }
        }
    }

    // Runs when something walks back out of the trigger. Clears the message.
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() == null) { return; }
        ShowMessage("");
    }

    // Changes the on-screen message. Does nothing if no text is assigned.
    private void ShowMessage(string message)
    {
        if (messageText != null) { messageText.text = message; }
    }
}
