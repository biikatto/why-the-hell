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

	public void Start(){
		rigidbody = gameObject.AddComponent<Rigidbody2D>();
		rigidbody.gravityScale = 0;
		rigidbody.hideFlags = HideFlags.HideInInspector;
	}

	public void BulletImpact(){
		Debug.Log("impact");
		HPManager.HP -= 1;
		if(HPManager.HP <= 0){
			Explode();
		}else{
			Debug.Log("Ding!");
		}
	}

	public void Explode(){
		Debug.Log("Boom!");
		Destroy(gameObject);
	}
}
