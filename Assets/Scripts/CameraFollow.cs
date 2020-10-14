using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - _player.position;

    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.position.z + offset.z);
    }
}
