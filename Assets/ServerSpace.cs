using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSpace : MonoBehaviour
{
    public static ServerSpace instance;

    public ServerColumn targetServer;

	private void Awake()
	{
        instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InstallCard(IInstallable installableCard)
	{
        Card_Ice iceCard = installableCard as Card_Ice;
        targetServer.InstallIce(iceCard);

    }




}
