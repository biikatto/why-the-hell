using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon : MonoBehaviour{
	[SerializeField]
	private Bullet bullet;
	public Bullet Bullet{
		get{return bullet;}
		set{bullet = value;}
	}
	private BulletPattern selectedPattern;
	public List<BulletPattern> patternList = new List<BulletPattern>();

	private Control control;

	public void Start(){
		control = gameObject.GetComponent<Control>();
		selectedPattern = gameObject.AddComponent<BulletPattern>();
		selectedPattern.bullet = bullet;
		patternList.Add(selectedPattern);
	}

	public void BeginFire(){
		BeginFire(selectedPattern);
	}

	public void BeginFire(BulletPattern pattern){
		if(control.Player2){
			pattern.BeginFire(true);
		}else{
			pattern.BeginFire(false);
		}
	}

	public void EndFire(){
		foreach(BulletPattern pattern in patternList){
			pattern.EndFire();
		}
	}
}
