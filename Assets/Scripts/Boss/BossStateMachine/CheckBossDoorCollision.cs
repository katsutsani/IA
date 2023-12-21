
using UnityEngine;

public class CheckBossDoorCollision : MonoBehaviour
{
    public bool PlayerPassDoor = false;
    public GameObject playerGameObject;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            PlayerPassDoor = true;
            playerGameObject = other.gameObject;
        }
    }
}
