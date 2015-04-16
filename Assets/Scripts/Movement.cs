using UnityEngine;
using System.Collections;

[System.Serializable]
public class Movement : MonoBehaviour{
	public bool player2;
	public bool firing;

	private bool constrainNorth;
	private bool constrainSouth;
	private bool constrainEast;
	private bool constrainWest;

	private new Rigidbody2D rigidbody;
	
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
        rigidbody = gameObject.GetComponent<Ship>().Rigidbody2D;
    }

    public void Firing(bool newFiring){
        firing = newFiring;
    }

    public void Move(Vector2 movementVector){
        movementVector = ConstrainDirections(movementVector);
    	if(firing){
    	movementVector = rigidbody.position +
    		(movementVector * movementSpeed * slowFactor * Time.deltaTime);
    	}else{
    	movementVector = rigidbody.position +
    		(movementVector * movementSpeed * Time.deltaTime);
    	}
    	transform.position = movementVector;
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == 10){
            if(collision.gameObject.name == "North"){
                constrainNorth = true;
            }
            if(collision.gameObject.name == "South"){
                constrainSouth = true;
            }
            if(collision.gameObject.name == "East"){
                constrainEast = true;
            }
            if(collision.gameObject.name == "West"){
                constrainWest = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 10){
            if(collision.gameObject.name == "North"){
                constrainNorth = false;
            }
            if(collision.gameObject.name == "South"){
                constrainSouth = false;
            }
            if(collision.gameObject.name == "East"){
                constrainEast = false;
            }
            if(collision.gameObject.name == "West"){
                constrainWest = false;
            }
        }
    }

    private Vector2 ConstrainDirections(Vector2 vector){
        if(constrainNorth){
            vector.y = Mathf.Min(vector.y, 0);
        }
        if(constrainSouth){
            vector.y = Mathf.Max(vector.y, 0);
        }
        if(constrainWest){
            vector.x = Mathf.Max(vector.x, 0);
        }
        if(constrainEast){
            vector.x = Mathf.Min(vector.x, 0);
        }
        return vector;
    }
}
