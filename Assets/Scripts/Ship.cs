using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ship : MonoBehaviour{
	private HPManager hpManager;
	private Movement movement;
	private Control control;
	private Weapon weapon;

	public new Rigidbody2D rigidbody;

	public HPManager HPManager{
		get{
			if(gameObject.GetComponent<HPManager>() == null){
				hpManager = gameObject.AddComponent<HPManager>() as HPManager;
			}else{
				hpManager = gameObject.GetComponent<HPManager>() as HPManager;
			}
			hpManager.hideFlags = HideFlags.HideInInspector;
			hpManager.Player2 = Control.Player2;
			return hpManager;
		}
	}

	public Movement Movement{
		get{
			if(gameObject.GetComponent<Movement>() == null){
				movement = gameObject.AddComponent<Movement>() as Movement;
			}else{
				movement = gameObject.GetComponent<Movement>() as Movement;
			}
			movement.hideFlags = HideFlags.HideInInspector;
			return movement;
		}
	}

	public Control Control{
		get{
			if(gameObject.GetComponent<Control>() == null){
				control = gameObject.AddComponent<Control>() as Control;
			}else{
				control = gameObject.GetComponent<Control>() as Control;
			}
			control.hideFlags = HideFlags.HideInInspector;
			return control;
		}
	}

	public Weapon Weapon{
		get{
			if(gameObject.GetComponent<Weapon>() == null){
				weapon = gameObject.AddComponent<Weapon>() as Weapon;
			}else{
				weapon = gameObject.GetComponent<Weapon>() as Weapon;
			}
			weapon.hideFlags = HideFlags.HideInInspector;
			return weapon;
		}
	}

	public Rigidbody2D Rigidbody2D{
	    get{
			if(gameObject.GetComponent<Rigidbody2D>() == null){
				rigidbody = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
			}else{
				rigidbody = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
			}
		    rigidbody.gravityScale = 0;
			rigidbody.hideFlags = HideFlags.HideInInspector;
			return rigidbody;
		}
	}

	public void Start(){
	}

	public void BulletImpact(){
		HPManager.HP -= 1;
		if(HPManager.HP <= 0){
			Explode();
		}else{
			Debug.Log("Ding!");
		}
	}

	public void Explode(){
		StartCoroutine("ResetLevel");
		transform.position = new Vector3(1000,1000,0);
	}

	private IEnumerator ResetLevel(){
		Debug.Log("Resetting...");
		yield return new WaitForSeconds(3);
		Debug.Log("Reset");
		Application.LoadLevel("Game");
	}

	public void PowerupPattern(BulletPattern pattern){
	    Weapon.PowerupPattern(pattern);
	}

	public void PowerupOn(){
	    Weapon.PowerupOn();
	}

	public void PowerupOff(){
	    Weapon.PowerupOff();
	}
}
