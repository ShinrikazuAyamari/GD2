using UnityEngine;

public class GenericEnemy : MonoBehaviour
{

    public int health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
