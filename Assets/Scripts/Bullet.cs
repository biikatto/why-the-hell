using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour{
	public Vector3 velocity;
	public bool sinPattern;
	public float sinWidth;
	public float sinFreq;

	private float phase;
	private Vector3 sinVector;
	private Vector3 centerPosition;

	private float distanceTraveled;

	public void Start(){
		phase = 0f;
		sinVector = new Vector3();

		centerPosition = transform.position;
		distanceTraveled = 0f;
	}

	public void Update(){
		if(sinPattern){
			sinVector.x = velocity.y * -1;
			sinVector.y = velocity.x;
			Vector3.Normalize(sinVector);
			sinVector *= Mathf.Sin(phase) * sinWidth;

			centerPosition = centerPosition + (velocity * Time.deltaTime);
			distanceTraveled += (velocity.magnitude * Time.deltaTime);
			transform.position = centerPosition + sinVector;
			phase += Time.deltaTime * sinFreq;

			sinWidth += distanceTraveled * 0.00005f;
			sinFreq += distanceTraveled * 0.0015f;

		}else{
			transform.position = transform.position + (velocity * Time.deltaTime);
		}
	}

	public void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.layer == 8){		// Ships layer
			other.gameObject.GetComponent<Ship>().BulletImpact();
		}
		if(other.gameObject.layer == 10){	// Screen
			Destroy(gameObject);
		}
	}
}
