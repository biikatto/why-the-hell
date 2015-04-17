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
	private bool readyToFire;
	
	private int patternNumber;
	public int PatternNumber{
		get{return patternNumber;}
		set{patternNumber = (patternList.Count + value) % patternList.Count;}
	}

	private List<BulletPattern> patternList;

	public void Start(){
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
		patternNumber = 2;

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
		while(firing){
			yield return new WaitForFixedUpdate();
		}
		StopCoroutine(patternCoroutine);
	}
}
