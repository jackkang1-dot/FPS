using UnityEngine;

// Keeps track of what the player is carrying. It goes on the Player.
public class PlayerInventory : MonoBehaviour
{
    // True once the player has picked up the fuse.
    private bool hasFuse = false;

    // Lets other scripts ask whether the player is carrying the fuse.
    public bool HasFuse()
    {
        return hasFuse;
    }

    // Called by FusePickup when the player walks into the fuse.
    public void PickUpFuse()
    {
        hasFuse = true;
    }

    // Called by FuseBox when the fuse gets installed.
    public void UseFuse()
    {
        hasFuse = false;
    }
}
