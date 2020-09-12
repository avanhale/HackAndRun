using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gogo : MonoBehaviour
{
    public string text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        text = GetComponent<TextMeshProUGUI>().text;
    }
}
