using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private static int startHealth; // Static so it keeps its value when a scene is loaded


    public int maxHealth;
    public int health;
    public Slider healthSlider;

    public Animator animator;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        health = startHealth;
        healthSlider.value = health;

        animator.SetInteger("Infection", health);
    }

    public void ReduceHealth(int increaseAmount)
    {
        health += increaseAmount;
        healthSlider.value = health;
        animator.SetInteger("Infection", health);

        if (health >= maxHealth)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // To always load the current scene when the player dies.
            startHealth += 20;

            if (startHealth >= maxHealth)
            {
                // Put game over code here. 
            }
        }
    }
}
