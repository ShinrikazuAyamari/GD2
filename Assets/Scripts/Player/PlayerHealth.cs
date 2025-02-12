using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
    }

    public void ReduceHealth(int increaseAmount)
    {
        health += increaseAmount;
        healthSlider.value = health;
        
        if (health >= maxHealth)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // To always load the current scene when the player dies. 
        }
    }
}
