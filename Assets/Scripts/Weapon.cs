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

	private List<BulletPattern> patterns;

	private Coroutine patternCoroutine;

	public void Start(){
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
		buttonsDown = 0;
		patternNumber = 2;

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

	public void BeginFire(int which){
		buttonsDown++;
		if(firing){
			_EndFire();
		}
		firing = true;
		bool reversed = true;
		StartCoroutine(Fire(which, reversed));
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

	private IEnumerator Fire(int which, bool reversed){
		BulletPattern pattern = patterns[which];
		pattern.reversed = reversed;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		patternCoroutine = StartCoroutine(pattern.Fire());
		while(firing){
			yield return new WaitForFixedUpdate();
		}
		StopCoroutine(patternCoroutine);
	}
}
