using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporttoStart : MonoBehaviour {
	public GameObject portalOther, player;
	public String horizontalOrVertical;
	private float lastPlayerX, lastPlayerY;
	private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
	    var portalRendererBounds = portalOther.GetComponent<Renderer>();
	    float portalWidth = portalRendererBounds.bounds.size.x;
	    float portalHeight = portalRendererBounds.bounds.size.y;
	    var playerRendererBounds = player.GetComponent<Renderer>();
	    float playerWidth = playerRendererBounds.bounds.size.x;
	    float playerHeight = playerRendererBounds.bounds.size.y;
	    
	    if (other.gameObject.CompareTag("Player")) {
		    if (horizontalOrVertical == "horizontal") {
			    lastPlayerX = player.transform.position.x;
			    
			    // If player hits top boundary
			    if (player.transform.position.y + playerHeight / 2 <=
				    portalOther.transform.position.y - portalHeight / 2) {
				    playerPos = new Vector3(lastPlayerX, portalOther.transform.position.y + portalHeight/2 + playerHeight/2, player.transform.position.z);
				    player.transform.position = playerPos;
			    }
			    
			    // If player hits bottom boundary
			    else if (player.transform.position.y - playerHeight / 2 >=
				    portalOther.transform.position.y + portalHeight / 2) {
				    playerPos = new Vector3(lastPlayerX, portalOther.transform.position.y - portalHeight/2 - playerHeight/2, player.transform.position.z);
				    player.transform.position = playerPos;
			    }
			    
			    
		    }
		    else if (horizontalOrVertical == "vertical") {
			    lastPlayerY = player.transform.position.y;

			    if (player.transform.position.x + playerWidth / 2 <=
				    portalOther.transform.position.x - portalWidth / 2) {
				    playerPos = new Vector3(portalOther.transform.position.x + portalWidth/2 + playerWidth/2, lastPlayerY, player.transform.position.z);
				    player.transform.position = playerPos;
				    
			    }
			    else if (player.transform.position.x - playerWidth / 2 <=
				    portalOther.transform.position.x + portalWidth / 2) {
				    playerPos = new Vector3(portalOther.transform.position.x - portalWidth/2 - playerWidth/2, lastPlayerY, player.transform.position.z);
				    player.transform.position = playerPos;
				    
			    }
			    
		    }

		    
		    
		    
		    
		    

	    }
    }
}
