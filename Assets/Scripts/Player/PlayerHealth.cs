using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int health;

    public void ReduceHealth(int increaseAmount)
    {
        health += increaseAmount;
        
        if (health >= 100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // To always load the current scene when the player dies. 
        }
    }
}
