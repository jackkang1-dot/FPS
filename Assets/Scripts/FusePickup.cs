using UnityEngine;

// Lets the player pick up the fuse by walking into it.
// The fuse needs a collider with Is Trigger checked.
public class FusePickup : MonoBehaviour
{
    // Optional sound that plays when the fuse is picked up.
    [SerializeField] AudioClip pickupSound;

    // Runs when something walks into the trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Only the player has a PlayerInventory, so anything else is ignored.
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) { return; }

        inventory.PickUpFuse();

        // PlayClipAtPoint keeps playing even after the fuse is destroyed.
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        Destroy(gameObject);
    }
}
