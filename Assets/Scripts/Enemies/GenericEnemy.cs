using UnityEngine;

public class GenericEnemy : MonoBehaviour
{

    public int health;

    public GameObject player;
    public float speed;

    private float distance;
    public float sight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < sight)
        {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    public void onAttacked(int damage)
    {
        health = health-damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
