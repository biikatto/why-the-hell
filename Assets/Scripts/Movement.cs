using UnityEngine;
using System.Collections;

[System.Serializable]
public class Movement : MonoBehaviour{
	public bool player2;
	public bool firing;
	
	[SerializeField]
	private float movementSpeed = 1.0f;
	public float MovementSpeed{
		get{return movementSpeed;}
		set{movementSpeed = value;}
	}

	[SerializeField]
	private float slowFactor = 0.5f;
	public float SlowFactor{
		get{return slowFactor;}
		set{slowFactor = value;}
	}

    public void Start(){
        firing = false;
    }

    public void Firing(bool newFiring){
        firing = newFiring;
    }

    public void Move(Vector3 movementVector){
    	if(firing){
    	movementVector = transform.position +
    		(movementVector * movementSpeed * slowFactor * Time.deltaTime);
    	}else{
    	movementVector = transform.position +
    		(movementVector * movementSpeed * Time.deltaTime);
    	}
    	transform.position = movementVector;
    }
}
