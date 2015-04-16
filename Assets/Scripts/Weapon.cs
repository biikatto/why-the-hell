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

	private Control control;

	private float bulletSpeed;
	public float BulletSpeed{
		get{return bulletSpeed;}
		set{bulletSpeed = value;}
	}

	private bool firing;
	private bool readyToFire;
	
	private int patternNumber;
	public int PatternNumber{
		get{return patternNumber;}
		set{patternNumber = value;}
	}

	private BulletPattern sinPattern;
	private BulletPattern spiralPattern;
	private BulletPattern eightLinePattern;
	private BulletPattern threeByFivePattern;

	public void Start(){
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
		patternNumber = 2;

		sinPattern = gameObject.AddComponent<SinPattern>();
		sinPattern.bulletSpeed = bulletSpeed;
		sinPattern.bullet = bullet;

		spiralPattern = gameObject.AddComponent<SpiralPattern>();
		spiralPattern.bulletSpeed = bulletSpeed;
		spiralPattern.bullet = bullet;

		eightLinePattern = gameObject.AddComponent<EightLinePattern>();
		eightLinePattern.bulletSpeed = bulletSpeed;
		eightLinePattern.bullet = bullet;

		threeByFivePattern = gameObject.AddComponent<ThreeByFivePattern>();
		threeByFivePattern.bulletSpeed = bulletSpeed;
		threeByFivePattern.bullet = bullet;
	}

	public void BeginFire(){
		firing = true;
		bool reversed = true;
		StartCoroutine(Fire(reversed));
	}

	public void EndFire(){
		if(firing){
			firing = false;
			StartCoroutine(Cooldown());
		}
	}

	private IEnumerator Cooldown(){
		readyToFire = false;
		yield return new WaitForSeconds(0.4f);
		readyToFire = true;
	}

	private IEnumerator Fire(bool reversed){
		sinPattern.reversed = reversed;
		spiralPattern.reversed = reversed;
		Coroutine patternCoroutine = null;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		switch(patternNumber){
			case 0:
				patternCoroutine = StartCoroutine(sinPattern.Fire());
				break;

			case 1:
			    patternCoroutine = StartCoroutine(eightLinePattern.Fire());
			    break;

			case 2:
			    patternCoroutine = StartCoroutine(threeByFivePattern.Fire());
			    break;

			default:
				patternCoroutine = StartCoroutine(spiralPattern.Fire());
				break;
		}
		while(firing){
			yield return new WaitForFixedUpdate();
		}
		StopCoroutine(patternCoroutine);
	}
}
