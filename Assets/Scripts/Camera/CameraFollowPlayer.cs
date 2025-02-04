using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;

    Vector3 position;
    void Start()
    {
        position.z = -20;
    }

    // Update is called once per frame
    void Update()
    {
        position.x = player.position.x;
        position.y = player.position.y;
        this.transform.position = position;
    }
}
