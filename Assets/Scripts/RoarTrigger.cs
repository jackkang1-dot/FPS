using UnityEngine;

// Plays the T-Rex roar and shakes the camera the first time the player walks through.
// Goes on an empty object with a Box Collider that has Is Trigger checked.
public class RoarTrigger : MonoBehaviour
{
    // The Audio Source behind the pen wall with the roar clip on it.
    [SerializeField] AudioSource roarSource;
    // Player > PlayerCameraRoot. Shaking it shakes the view.
    [SerializeField] Transform cameraRoot;
    // How long the shake lasts, in seconds.
    [SerializeField] float shakeDuration = 2.5f;
    // How hard the camera shakes. Raise it if the shake is too subtle.
    [SerializeField] float shakeStrength = 0.08f;

    // True once the roar has played, so it only happens once.
    private bool hasRoared = false;
    // Seconds of shaking left. Zero means no shake.
    private float shakeTimeLeft = 0f;
    // Where the camera sat before the shake, so it can go back exactly.
    private Vector3 cameraStartPosition;

    // Runs when something walks into the trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (hasRoared) { return; }

        // Only the player has a PlayerInventory, so anything else is ignored.
        if (other.GetComponent<PlayerInventory>() == null) { return; }

        hasRoared = true;

        if (roarSource != null)
        {
            roarSource.Play();
        }

        if (cameraRoot != null)
        {
            cameraStartPosition = cameraRoot.localPosition;
            shakeTimeLeft = shakeDuration;
        }
    }

    // Runs once per frame. Moves the camera to a random nearby spot while the shake lasts.
    void Update()
    {
        if (shakeTimeLeft <= 0f) { return; }

        shakeTimeLeft -= Time.deltaTime;

        if (shakeTimeLeft > 0f)
        {
            // fade goes from 1 down to 0, so the shake dies off instead of stopping suddenly.
            float fade = shakeTimeLeft / shakeDuration;
            cameraRoot.localPosition = cameraStartPosition + Random.insideUnitSphere * shakeStrength * fade;
        }
        else
        {
            cameraRoot.localPosition = cameraStartPosition;
        }
    }
}
