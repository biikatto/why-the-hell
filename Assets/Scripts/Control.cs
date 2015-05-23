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
    	for(int i=0;i<4;i++){
    		inputList.Add("Fire "+(i+1), "Fire "+(i+1));
    	}
    	foreach(string key in ((Hashtable)inputList.Clone()).Keys){
    		if(player2){
    			inputList[key] = inputList[key] + " P2";
    		}else{
    			inputList[key] = inputList[key] + " P1";
    		}
    	}
    }

	public void LateUpdate(){
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

		int buttonsDownThisFrame = 0;
		for(int i=0;i<4;i++){
			if(Input.GetButtonDown(inputList["Fire "+(i+1)] as string)){
				// begin fire
				weapon.BeginFire(i);
				movement.Firing(weapon.Firing);
				buttonsDownThisFrame++;
				continue;
			}
		}
		if(buttonsDownThisFrame > 1){
			Debug.Log(buttonsDownThisFrame);
		}
		for(int i=0;i<4;i++){
			if(Input.GetButtonUp(inputList["Fire "+(i+1)] as string)){
				// end fire
				weapon.EndFire();
				movement.Firing(weapon.Firing);
			}
		}
	}
}
