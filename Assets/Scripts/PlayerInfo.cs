using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public ChatCharacter chatInfo;
    public TextMeshPro textNameBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updatePlayer(ChatCharacter c)
    {
        chatInfo = c;
        
        Debug.LogError($" hi {textNameBox.text}");
        
        textNameBox.text = chatInfo.Name;
    }
}
