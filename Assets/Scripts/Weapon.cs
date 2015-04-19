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
		set{patternNumber = (patternList.Count + value) % patternList.Count;}
	}

	private List<BulletPattern> patternList;

	private List<BulletPattern> patterns;

	private Coroutine patternCoroutine;

	public void Start(){
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
		buttonsDown = 0;
		patternNumber = 2;

<<<<<<< HEAD
		patterns = new List<BulletPattern>();

		patterns.Add(gameObject.AddComponent<SinPattern>());
		patterns[0].bulletSpeed = bulletSpeed;
		patterns[0].bullet = bullet;

		patterns.Add(gameObject.AddComponent<SpiralPattern>());
		patterns[1].bulletSpeed = bulletSpeed;
		patterns[1].bullet = bullet;

		patterns.Add(gameObject.AddComponent<EightLinePattern>());
		patterns[2].bulletSpeed = bulletSpeed;
		patterns[2].bullet = bullet;

		patterns.Add(gameObject.AddComponent<ThreeByFivePattern>());
		patterns[3].bulletSpeed = bulletSpeed;
		patterns[3].bullet = bullet;
	}

	public void BeginFire(int which, bool reversed){
		Debug.Log(reversed);
		buttonsDown++;
		if(firing){
			_EndFire();
		}
		firing = true;
		StartCoroutine(Fire(which, reversed));
=======
		patternList = new List<BulletPattern>();

		patternList.Add(gameObject.AddComponent<SinPattern>());
		patternList[0].bulletSpeed = bulletSpeed;
		patternList[0].bullet = bullet;

		patternList.Add(gameObject.AddComponent<SpiralPattern>());
		patternList[1].bulletSpeed = bulletSpeed;
		patternList[1].bullet = bullet;

		patternList.Add(gameObject.AddComponent<EightLinePattern>());
		patternList[2].bulletSpeed = bulletSpeed;
		patternList[2].bullet = bullet;

		patternList.Add(gameObject.AddComponent<ThreeByFivePattern>());
		patternList[3].bulletSpeed = bulletSpeed;
		patternList[3].bullet = bullet;
	}

	public void BeginFire(bool reversed){
		firing = true;
		StartCoroutine(Fire(reversed));
>>>>>>> df8bd13949797a7e27681b98b90c1e0a15d41dd5
	}

	public void EndFire(){
		buttonsDown--;
		if((buttonsDown == 0) && firing){
			_EndFire();
		}
	}

	private void _EndFire(){
		firing = false;
		StopCoroutine(patternCoroutine);
		StartCoroutine(Cooldown());
	}

	private IEnumerator Cooldown(){
		readyToFire = false;
		yield return new WaitForSeconds(0.4f);
		readyToFire = true;
	}

<<<<<<< HEAD
	private IEnumerator Fire(int which, bool reversed){
		BulletPattern pattern = patterns[which];
		pattern.reversed = reversed;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		patternCoroutine = StartCoroutine(pattern.Fire());
=======
	private IEnumerator Fire(bool reversed){
	    for(int i=0;i<patternList.Count;i++){
	        patternList[i].reversed = reversed;
	    }
		Coroutine patternCoroutine = null;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		if(patternNumber <= patternList.Count){
		    patternCoroutine = StartCoroutine(patternList[patternNumber].Fire());
		}
>>>>>>> df8bd13949797a7e27681b98b90c1e0a15d41dd5
		while(firing){
			yield return new WaitForFixedUpdate();
		}
		StopCoroutine(patternCoroutine);
	}
}
