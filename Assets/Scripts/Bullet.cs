using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour{
	public Vector3 velocity;

	public void Start(){
	}

	public void Update(){
		transform.position = transform.position + (velocity * Time.deltaTime);
	}

	public void OnTriggerEnter2D(Collider2D other){
	    Debug.Log(other.gameObject.layer);
		if(other.gameObject.layer == 8){		// Ships layer
			other.gameObject.GetComponent<Ship>().BulletImpact();
		}
		if(other.gameObject.layer == 10){       // Edges
		    Destroy(gameObject);
		}
	}
}
