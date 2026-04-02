using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float height = 15f;

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, height, player.position.z);
    }
}
