using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bossPrefab; // Assign the boss prefab in the Inspector
    public GameObject bossInstance;
    public int bossHealth = 100; // Initial health of the boss
    public SphereCollider trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<SphereCollider>();
        bossInstance = Instantiate(bossPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Logic to handle player entering the boss's trigger area
            Debug.Log("Player has entered the boss's trigger area.");
            other.GetComponent<ChatController>().chatInfo.Status = ChatFightStatus.attack; // Change status to attack
            // You can add more logic here, like starting a battle or displaying UI
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
