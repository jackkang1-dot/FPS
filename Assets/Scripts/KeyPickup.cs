using UnityEngine;

// Lets the player pick up the custodian's key by walking into it.
// The key needs a collider with Is Trigger checked.
public class KeyPickup : MonoBehaviour
{
    // Optional sound that plays when the key is picked up.
    [SerializeField] AudioClip pickupSound;

    // Runs when something walks into the trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Only the player has a PlayerInventory, so anything else is ignored.
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) { return; }

        inventory.PickUpKey();

        // PlayClipAtPoint keeps playing even after the key is destroyed.
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        Destroy(gameObject);
    }
}
