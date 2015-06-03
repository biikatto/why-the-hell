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

	private Control control;

	private float bulletSpeed;
	public float BulletSpeed{
		get{return bulletSpeed;}
		set{bulletSpeed = value;}
	}

	private bool firing;
	public bool Firing{
		get{return firing;}
	}
	private bool readyToFire;
	private int buttonsDown;

	private int patternNumber;
	public int PatternNumber{
		get{return patternNumber;}
		set{patternNumber = value;}
	}

	private BulletPattern sinPattern;
	private BulletPattern spiralPattern;
	private BulletPattern eightLinePattern;
	private BulletPattern threeByFivePattern;

	private BulletPattern powerupPattern;
	private Coroutine powerupCoroutine;

	private List<BulletPattern> patterns;

	private List<Coroutine> patternCoroutines;
	private Coroutine patternCoroutine;

	public void Start(){
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
		buttonsDown = 0;
		patternNumber = 2;

		patterns = new List<BulletPattern>();
		patternCoroutines = new List<Coroutine>();

		patterns.Add(gameObject.AddComponent<SinPattern>());
		patterns[0].bulletSpeed = bulletSpeed;
		patterns[0].bullet = bullet;

		patterns.Add(gameObject.AddComponent<SpiralPattern>());
		patterns[1].bulletSpeed = bulletSpeed;
		patterns[1].bullet = bullet;

		patterns.Add(gameObject.AddComponent<RingPattern>());
		patterns[2].bulletSpeed = bulletSpeed;
		patterns[2].bullet = bullet;

		patterns.Add(gameObject.AddComponent<ThreeByFivePattern>());
		patterns[3].bulletSpeed = bulletSpeed;
		patterns[3].bullet = bullet;
	}

	public void BeginFire(int which, bool reversed){
		buttonsDown++;
		if(firing){
			_EndFire();
		}
		firing = true;
		StartCoroutine(Fire(which, reversed));
	}

	public void EndFire(){
		Debug.Log("EndFire()");
		buttonsDown--;
		if((buttonsDown == 0)/* && firing*/){
			firing = false;
			_EndFire();
		}
	}

	private void _EndFire(){
		for(int i=patternCoroutines.Count-1;i>=0;i--){
			StopCoroutine(patternCoroutines[i]);
			patternCoroutines.Remove(patternCoroutines[i]);
		}
		Debug.Log("_EndFire() "+ patternCoroutines.Count.ToString());
		StartCoroutine(Cooldown());
	}

	private IEnumerator Cooldown(){
		readyToFire = false;
		yield return new WaitForSeconds(0.4f);
		readyToFire = true;
	}

	private IEnumerator Fire(int which, bool reversed){
		BulletPattern pattern = patterns[which];
		pattern.reversed = reversed;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		// fix this to not be hardcoded later
		if(patternCoroutines.Count == 0){
			patternCoroutines.Add(StartCoroutine(pattern.Fire()));
		}
		while(firing){
			yield return new WaitForFixedUpdate();
		}
		if(patternCoroutines.Count > 0){
			StopCoroutine(patternCoroutines[patternCoroutines.Count-1]);
			patternCoroutines.Remove(patternCoroutines[patternCoroutines.Count-1]);
		}
	}

	public void Update(){
	}

	public void PowerupPattern(BulletPattern pattern){
        powerupPattern = pattern;
        powerupPattern.transform.parent = transform;
        powerupPattern.bulletSpeed = bulletSpeed * 1.3f;
        powerupPattern.bullet = bullet;
	}

	public void PowerupOn(){
	    _EndFire();
	    _EndFire();
	    _EndFire();
	    powerupCoroutine = StartCoroutine(powerupPattern.Fire());
	    readyToFire = false;
	}

	public void PowerupOff(){
	    StopCoroutine(powerupCoroutine);
	    _EndFire();
	    _EndFire();
	    _EndFire();
	    readyToFire = true;
	}
}
