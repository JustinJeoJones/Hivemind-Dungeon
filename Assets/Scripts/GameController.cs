using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerSpawn;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnPlayersAll();
        }
    }

    void spawnPlayersAll()
    {
        Vector3 spawnPosition = playerSpawn.transform.position;
        Quaternion spawnRotation = Quaternion.identity; // No rotation
        foreach (ChatCharacter chatCharacter in ChatArmyController.GetChatArmy())
        {
            Debug.Log(chatCharacter.Name);
            GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, spawnRotation);
            Debug.Log($"Spawning player: {chatCharacter.Name} at position: {spawnPosition}");
            newPlayer.GetComponentInChildren<PlayerInfo>().updatePlayer(chatCharacter);
            newPlayer.GetComponent<ChatMover>().target = target.transform;
            newPlayer.GetComponent<ChatMover>().spawn = spawnPosition;
        }
    }
}
