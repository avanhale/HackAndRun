using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerColumn : MonoBehaviour
{

    public Transform iceColumnT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InstallIce(Card_Ice card)
	{
        card.ParentCardTo(iceColumnT);
        card.transform.localEulerAngles = Vector3.forward * 180;
	}



}
