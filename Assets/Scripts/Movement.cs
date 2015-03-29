using UnityEngine;
using System.Collections;

[System.Serializable]
public class Movement : MonoBehaviour{
	public bool player2;
	
	[SerializeField]
	private float movementSpeed = 1.0f;
	public float MovementSpeed{
		get{return movementSpeed;}
		set{movementSpeed = value;}
	}

    public void Start(){
    }

    public void Move(Vector3 movementVector){
    	movementVector = transform.position +
    		(movementVector * movementSpeed * Time.deltaTime);
    	transform.position = movementVector;
    }
}
