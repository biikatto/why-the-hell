using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon : MonoBehaviour{
	[SerializeField]
	private Bullet bullet;
	public Bullet Bullet{
		get{return bullet;}
		set{bullet = value;}
	}
	private BulletPattern selectedPattern;
	public BulletPattern[] patternList;

	private Control control;

	public void Start(){
		control = gameObject.GetComponent<Control>();
		selectedPattern = gameObject.AddComponent<BulletPattern>();
		selectedPattern.bullet = bullet;
	}

	public void Fire(){
		Fire(selectedPattern);
	}

	public void Fire(BulletPattern pattern){
		if(control.Player2){
			pattern.Fire(true);
		}else{
			pattern.Fire(false);
		}
	}
}
