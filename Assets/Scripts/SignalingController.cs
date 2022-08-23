using UnityEngine;

public class SignalingController : MonoBehaviour
{
    [SerializeField] private Signaling _signaling;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ThiefMovement thiefMovement))
            _signaling.TurnOn();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ThiefMovement thiefMovement))
            _signaling.TurnOff();
    }
}
