using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class text : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = $"Current Account: {Secret.twitch_channel}";
    }
}
