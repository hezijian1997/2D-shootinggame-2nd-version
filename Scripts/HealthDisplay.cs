using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI HealthText;
    Player HealthValue;
    // Use this for initialization
    void Start ()
    {
        HealthText = GetComponent<TextMeshProUGUI>();
        HealthValue = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HealthText.text = "Health: " + HealthValue.GetHealth().ToString();
	}
}
