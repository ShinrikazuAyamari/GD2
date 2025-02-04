using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{

    public Rigidbody2D playerBody;

    Vector2 position;

    public float walkSpeed;

    Vector2 mousePosition;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        position.x = Input.GetAxisRaw("Horizontal");
        position.y = Input.GetAxisRaw("Vertical");

        position.Normalize();

        playerBody.linearVelocity = position * walkSpeed;

        // Rotation
        mousePosition = mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}



