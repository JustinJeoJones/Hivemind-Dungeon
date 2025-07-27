using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Unity;
using UnityEngine;

public static class ChatArmyController
{ 
    private static List<ChatCharacter> chatArmy = new List<ChatCharacter>();

    public static List<ChatCharacter> GetChatArmy()
    {
        return chatArmy;
    }

    public static void AddCharacter(ChatCharacter character)
    {
        if (!chatArmy.Exists(c => c.Id == character.Id))
        {
            chatArmy.Add(character);
            Debug.Log($"Character {character.Name} added to the chat army.");
            Debug.Log("Current chat army count: " + chatArmy.Count);
        }
        else
        {
            Debug.Log($"Character {character.Name} is already in the chat army.");
        }
    }

    public static void DropList()
    {
        chatArmy.Clear();
        Debug.Log("Chat army has been cleared.");
        Debug.Log("Current chat army count: " + chatArmy.Count);
    }

    public static void Win()
    {
        foreach (ChatCharacter character in chatArmy)
        {
            character.Status = ChatFightStatus.win;
        }
    }
}
