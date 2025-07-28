using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using System.Threading.Tasks;
using TwitchLib.Client.Events;
using System;
using Assets.Scripts;

public class TwitchClient : MonoBehaviour
{
    //Comeback and adjust this 
    public Client client;
    private string channelName = Secret.twitch_channel;
    private int tempId = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Always run in background
        Application.runInBackground = true;
        //tells bot which channel to join
        ConnectionCredentials credentials = new ConnectionCredentials("hivemindgamebot", Secret.bot_access_token);
        client = new Client();
        client.Initialize(credentials, channelName);

        //Event subscribing
        client.OnMessageReceived += MessageRecieve;

        //connect our bot to the channel
        client.ConnectAsync();
    }

    private async Task MessageRecieve(object sender, OnMessageReceivedArgs e)
    {
        Debug.Log("The bot just read a message in chat");
        Debug.Log($"{e.ChatMessage.Username}: {e.ChatMessage.Message}");
        if(e.ChatMessage.Message == "!join")
        {
            //Lets us create a character out of a chat message
            ChatCharacter unit = new ChatCharacter
            {
                Name = e.ChatMessage.Username,
                Id = e.ChatMessage.UserId
            };
            ChatArmyController.AddCharacter(unit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            client.SendMessage(client.JoinedChannels[0], "Hello World!");
        }
        //debugging purposes
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ChatCharacter unit = new ChatCharacter
            {
                Name = $"dummi{tempId}",
                Id = $"Temp:{tempId}"
            };
            tempId++;
            ChatArmyController.AddCharacter(unit);
        }
    }
   


}
