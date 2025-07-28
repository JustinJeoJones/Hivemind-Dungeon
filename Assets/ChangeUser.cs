using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class ChangeUser : MonoBehaviour
{
    public TMP_InputField userInputBox;
    public TMP_InputField goblinInputBox;
    public GameObject allUsers;
    private TextMeshProUGUI textMeshPro;
    private int tempId = 0;

    private void Start()
    {
        textMeshPro = allUsers.GetComponent<TextMeshProUGUI>();
    }
    public void OnButtonClick()
     {
        Secret.twitch_channel = userInputBox.text;
     }

    public void StartButton()
    {
        //SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene("Dungeon1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AddGoblin()
    {
        ChatCharacter character = new ChatCharacter()
        {
            Name = goblinInputBox.text,
            Id = $"id:{tempId++}"
        };
        ChatArmyController.AddCharacter(character);
    }

    private void Update()
    {
        if (ChatArmyController.GetChatArmy().Count > 0 && textMeshPro != null)
        {
            textMeshPro.text = $"";
            foreach (ChatCharacter c in ChatArmyController.GetChatArmy())
            {
                textMeshPro.text += $"{c.Name}\n";
            }
        }
        
    }

}
