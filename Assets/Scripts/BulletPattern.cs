using UnityEngine;
using System.Collections;

[System.Serializable]
public class BulletPattern : MonoBehaviour{
	[SerializeField]
	public Bullet bullet;

	private float bulletSpeed;
	public float BulletSpeed{
		get{return bulletSpeed;}
		set{bulletSpeed = value;}
	}

	private bool firing;
	private bool readyToFire;

	private float TAU;
	
	private int patternNumber;
	public int PatternNumber{
		get{return patternNumber;}
		set{patternNumber = value;}
	}

	private PatternVariation pattern;

	public void Start(){
		TAU = 2*Mathf.PI;
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
		patternNumber = 0;
		pattern = gameObject.AddComponent<SinPattern>();
		pattern.bulletSpeed = bulletSpeed;
		pattern.bullet = bullet;
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
		pattern.reversed = reversed;
		Coroutine patternCoroutine = null;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		switch(patternNumber){
			case 0:
				Debug.Log("sin");
				patternCoroutine = StartCoroutine(pattern.Fire());
				break;

			default:
				Debug.Log("spiral");
				patternCoroutine = StartCoroutine(BasicPattern(reversed));
				break;
		}
		while(firing){
			yield return new WaitForFixedUpdate();
		}
		StopCoroutine(patternCoroutine);
	}

	private IEnumerator BasicPattern(bool reversed){
		int nBullets = 8;
		float angleWidth = 1.0f;
		float mag = 1.0f;
		float offset = 0.075f;
		if(reversed){
			offset += 0.5f;
		}
		while(true){
			for(int i=0;i<nBullets;i++){
				float theta = (((float)i/nBullets) *
						TAU*angleWidth) +
					TAU*offset;
				Vector3 bulletPosition = new Vector3(
						Mathf.Sin(theta)*mag, Mathf.Cos(theta)*mag, 0);

				Bullet thisBullet = GameObject.Instantiate(
						bullet,
						(new Vector3(Mathf.Sin(theta)*mag, Mathf.Cos(theta)*mag, 0) +
						 transform.position),
						Quaternion.identity) as Bullet;
				thisBullet.velocity = bulletPosition.normalized * bulletSpeed;
			}
			offset += 0.01625f;
			yield return new WaitForSeconds(0.2f);
		}
	}
}
