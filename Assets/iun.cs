using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MonoBehaviour m = this;
        if (m.GetType() == typeof(MonoBehaviour))
		{
			print("MonoBehaviour");
		}

        if (m)
		{
            print("mm");
        }


        //Card_Hardware c = (Card_Hardware) this;
        ouinad o = (ouinad) this;
        if (o.GetType() == typeof(ouinad))
		{
            print("ouinad");
		}

        if (o)
		{
            print("o");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
