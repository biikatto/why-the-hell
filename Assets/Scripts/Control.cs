using UnityEngine;
using System.Collections;

[System.Serializable]
public class Control : MonoBehaviour{
	[SerializeField]
	private bool player2;
	public bool Player2{
		get{
			return player2;
		}
		set{
			player2 = value;
		}
	}

	private Movement movement;
	private Weapon weapon;

    private Hashtable inputList;

    public void Start(){
    	movement = gameObject.GetComponent<Ship>().Movement;
    	weapon = gameObject.GetComponent<Ship>().Weapon;

    	inputList = new Hashtable();
    	inputList.Add("Horizontal", "Horizontal");
    	inputList.Add("Vertical", "Vertical");
    	inputList.Add("Fire", "Fire");
    	foreach(string key in ((Hashtable)inputList.Clone()).Keys){
    		if(player2){
    			inputList[key] = inputList[key] + " P2";
    		}else{
    			inputList[key] = inputList[key] + " P1";
    		}
    	}
    }

	public void Update(){
		Vector2 movementVector = new Vector2();
		if(Input.GetAxis(inputList["Horizontal"] as string) > 0){
			// move right
			movementVector.x = 1;
		}else if (Input.GetAxis(inputList["Horizontal"] as string) < 0){
			// move left
			movementVector.x = -1;
		}

		if(Input.GetAxis(inputList["Vertical"] as string) > 0){
			// move up
			movementVector.y = 1;
		}else if(Input.GetAxis(inputList["Vertical"] as string) < 0){
			// move down
			movementVector.y = -1;
		}
		movement.Move(movementVector.normalized);

		if(Input.GetButtonDown(inputList["Fire"] as string)){
			// begin fire
			weapon.BeginFire();
			movement.Firing(true);
		}
		if(Input.GetButtonUp(inputList["Fire"] as string)){
			// end fire
			weapon.EndFire();
			movement.Firing(false);
		}
	}
}
