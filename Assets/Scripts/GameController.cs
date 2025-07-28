using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerSpawn;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        spawnPlayersAll();
    }

    // Update is called once per frame
    void Update()
    {
        if(ChatArmyController.GetChatArmy().Where(c => c.IsAlive == false).Count() > 0)
        {
            spawnPlayersDead();
        }
    }

    void spawnPlayersDead()
    {
        Vector3 spawnPosition = playerSpawn.transform.position;
        Quaternion spawnRotation = Quaternion.identity; // No rotation
        foreach (ChatCharacter chatCharacter in ChatArmyController.GetChatArmy().Where(c => c.IsAlive == false))
        {
            Debug.Log(chatCharacter.Name);
            GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, spawnRotation);

            Debug.Log($"Spawning player: {chatCharacter.Name} at position: {spawnPosition}");
            newPlayer.GetComponentInChildren<PlayerInfo>().updatePlayer(chatCharacter);
            newPlayer.GetComponent<ChatController>().chatInfo = chatCharacter;
            newPlayer.GetComponent<ChatController>().target = target;
            newPlayer.GetComponent<ChatController>().spawn = spawnPosition;
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
            newPlayer.GetComponent<ChatController>().chatInfo = chatCharacter;
            newPlayer.GetComponent<ChatController>().target = target;
            newPlayer.GetComponent<ChatController>().spawn = spawnPosition;
        }
    }
}
