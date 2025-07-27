using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bossPrefab; // Assign the boss prefab in the Inspector
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bossPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
