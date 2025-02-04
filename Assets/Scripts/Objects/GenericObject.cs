using UnityEngine;

public class GenericObject : MonoBehaviour
{
    public int damage;  

    public Color objectColor; // This is temporary and purely for showing when it's hurtbox is active. 
    private SpriteRenderer sprite;

    public float speed;

    public float activeSpeed;   //  speed to check when the hurtbox should be active

    public Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectColor = Color.white;
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = objectColor;
    }

    // Update is called once per frame
    void Update()
    {
        speed = body.linearVelocity.magnitude;

        sprite.color = objectColor;

        if(speed > activeSpeed)
        {
            objectColor = Color.red;
        }
        else
        {
            objectColor = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (speed > activeSpeed)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject enemy = collision.gameObject;
                enemy.GetComponent<GenericEnemy>().onAttacked(damage);
            }
        }
    }
}
