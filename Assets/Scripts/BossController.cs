using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public GameObject bossPrefab; // Assign the boss prefab in the Inspector
    public GameObject bossInstance;
    public int bossHealth = 100 * ChatArmyController.GetChatArmy().Count; // Initial health of the boss
    public SphereCollider trigger;
    public Animator animatior;
    public string CurrentAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<SphereCollider>();
        
        bossInstance = Instantiate(bossPrefab, transform.position, Quaternion.identity);
        animatior = bossInstance.GetComponentInChildren<Animator>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic to handle player entering the boss's trigger area
            Debug.Log("Player has left the boss's trigger area.");
            other.GetComponent<ChatController>().chatInfo.Status = ChatFightStatus.walk; // Change status to attack
            // You can add more logic here, like starting a battle or displaying UI
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic to handle player entering the boss's trigger area
            Debug.Log("Player has entered the boss's trigger area.");
            other.GetComponent<ChatController>().chatInfo.Status = ChatFightStatus.attack; // Change status to attack
            // You can add more logic here, like starting a battle or displaying UI
            if(CurrentAnimation != "Punch")
            {
                CurrentAnimation = "Punch";
                animatior.CrossFade("Punch", 0.2f);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            returnBack();
        }
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        Debug.Log("Boss took damage: " + damage + ". Remaining health: " + bossHealth);
        if (bossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(bossInstance);
        trigger.enabled = false;
        ChatArmyController.Win();

        Invoke("returnBack", 5.0f);
    }

    void returnBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
