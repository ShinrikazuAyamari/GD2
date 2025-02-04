using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health;

    public void ReduceHealth(int decreaseAmount)
    {
        health -= decreaseAmount;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
