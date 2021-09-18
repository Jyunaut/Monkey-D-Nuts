using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporttoStart : MonoBehaviour {
	public GameObject portalOne, player;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
	    if (other.gameObject.CompareTag("Player")) {
		    var rendererBounds = portalOne.GetComponent<Renderer>();
		    float width = rendererBounds.bounds.size.x;
		    Vector3 playerPos;
		    playerPos = new Vector3(player.transform.position.x+width/2, player.transform.position.y, player.transform.position.z);
		    player.transform.position = playerPos;

	    }
    }
}
