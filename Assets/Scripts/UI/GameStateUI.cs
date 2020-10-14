using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Time: " + GameState.instance.GetTimeLeftTillDayEnds().ToString("F0") + "\tPoints: " + GameState.instance.customersServed;
    }
}
