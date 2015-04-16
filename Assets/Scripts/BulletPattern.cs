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
	private Coroutine fireCoroutine;
	private Coroutine cooldownCoroutine;

	private float cooldownTime = 0f;
	private float cooldownDuration = 0.4f;

	private float tau;
	
	private int patternNumber;
	public int PatternNumber{
		get{return patternNumber;}
		set{patternNumber = value;}
	}

	public void Start(){
		tau = 2*Mathf.PI;
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
		patternNumber = 0;
	}

	public void BeginFire(bool reversed){
		if(Time.time >= cooldownTime){
		// if(!firing){
		 	firing = true;
		 	fireCoroutine = StartCoroutine(Fire(reversed));
		// 	if(cooldownCoroutine != null){
		// 		StopCoroutine(cooldownCoroutine);
		// 	}
		}
	}

	public void EndFire(){
		if(firing){
			StopCoroutine(fireCoroutine);
			cooldownTime = Time.time + cooldownDuration;
			firing = false;
		//	cooldownCoroutine = StartCoroutine(Cooldown());
		}
	}

	private IEnumerator Cooldown(){
		readyToFire = false;
		yield return new WaitForSeconds(0.4f);
		readyToFire = true;
		firing = false;
	}

	private IEnumerator Fire(bool reversed){
		float delay = 0.2f;
		Coroutine patternCoroutine = null;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		switch(patternNumber){
			case 0:
				Debug.Log("sin");
				patternCoroutine = StartCoroutine(SinPattern(reversed));
				break;

			default:
				Debug.Log("spiral");
				patternCoroutine = StartCoroutine(BasicPattern(reversed));
				break;
		}
		yield return new WaitForSeconds(delay);
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
		while(firing){
			for(int i=0;i<nBullets;i++){
				float theta = (((float)i/nBullets) *
						tau*angleWidth) +
					tau*offset;
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

	private IEnumerator SinPattern(bool reversed){
		int nBullets = 8;
		float angleWidth = 1.0f;
		float mag = 1.0f;
		float offset = 0.075f;
		float sinWidth = 0.25f;
		float sinFreq = 2f;
		float rotationSpeed = 1f;
		if(reversed){
			offset += 0.5f;
		}
		while(firing){
			for(int i=0;i<nBullets;i++){
				// float theta = (((float)i/nBullets) *
				// 		tau*angleWidth) +
				// 		(Mathf.Sin(tau*offset) * sinWidth);
				// float theta = (((float)i/nBullets) *
				// 		tau*angleWidth) +
				// 		tau*offset;
				float theta = ((float)i/nBullets) * tau;
				Vector3 bulletPosition = new Vector3(
						Mathf.Sin(theta)*mag, Mathf.Cos(theta)*mag, 0);
				Bullet thisBullet = GameObject.Instantiate(
						bullet,
						(bulletPosition + transform.position),
						Quaternion.identity) as Bullet;
				thisBullet.velocity = bulletPosition.normalized * bulletSpeed;
				thisBullet.sinPattern = true;
				thisBullet.sinWidth = sinWidth;
				thisBullet.sinFreq = sinFreq;
			}
			offset += 0.0325f * rotationSpeed;
			yield return new WaitForSeconds(0.2f);
		}
	}
}
