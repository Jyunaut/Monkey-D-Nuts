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

    private void OnTriggerEnter2D(Collider2D other) {
	    var portalRendererBounds = portalOther.GetComponent<Renderer>();
	    float portalWidth = portalRendererBounds.bounds.size.x;
	    float portalHeight = portalRendererBounds.bounds.size.y;
	    var playerRendererBounds = player.GetComponent<Renderer>();
	    float playerWidth = playerRendererBounds.bounds.size.x;
	    float playerHeight = playerRendererBounds.bounds.size.y;
	    
	    if (other.gameObject.CompareTag("Player")) {
            Vector3 posDiff = new Vector3(0, 0, 0);
		    if (horizontalOrVertical == "horizontal") {
			    lastPlayerX = player.transform.position.x;
			    
			    // If player hits top boundary
			    if (player.transform.position.y <=
				    portalOther.transform.position.y) {
				    playerPos = new Vector3(lastPlayerX, portalOther.transform.position.y - portalHeight/2 - playerHeight, player.transform.position.z);
                    posDiff = player.transform.position - playerPos;
                    player.transform.position = playerPos;
			    }
			    
			    // If player hits bottom boundary
			    else if (player.transform.position.y >=
				    portalOther.transform.position.y) {
				    playerPos = new Vector3(lastPlayerX, portalOther.transform.position.y + portalHeight/2 + playerHeight + 2f, player.transform.position.z); //+2 to adjust for collider offset
                    posDiff = player.transform.position - playerPos;
                    player.transform.position = playerPos;
			    }
			    
			    
		    }
		    else if (horizontalOrVertical == "vertical") {
			    lastPlayerY = player.transform.position.y;

			    if (player.transform.position.x <=
				    portalOther.transform.position.x) {
				    playerPos = new Vector3(portalOther.transform.position.x - portalWidth/2 - playerWidth, lastPlayerY, player.transform.position.z);
                    posDiff = player.transform.position - playerPos;
                    player.transform.position = playerPos;
				    
			    }
			    else if (player.transform.position.x >=
				    portalOther.transform.position.x) {
				    playerPos = new Vector3(portalOther.transform.position.x + portalWidth/2 + playerWidth, lastPlayerY, player.transform.position.z);
                    posDiff = player.transform.position - playerPos;
                    player.transform.position = playerPos;
				    
			    }
			    
		    }

            //held item
            if (player.GetComponent<Player.Controller>().HeldItem != null)
            {
                player.GetComponent<Player.Controller>().HeldItem.transform.position -= posDiff;
            }
        }
    }
}
