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
	
	private int patternNumber;
	public int PatternNumber{
		get{return patternNumber;}
		set{patternNumber = value;}
	}

	public void Start(){
		firing = false;
		bulletSpeed = 6.3f;
		readyToFire = true;
	}

	public void BeginFire(bool reversed){
		if(!firing){
			fireCoroutine = StartCoroutine(Fire(reversed));
		}
	}

	public void EndFire(){
		if(firing){
			StartCoroutine(Cooldown());
			StopCoroutine(fireCoroutine);
			firing = false;
		}
	}

	private IEnumerator Cooldown(){
		readyToFire = false;
		yield return new WaitForSeconds(0.4f);
		readyToFire = true;
	}

	private IEnumerator Fire(bool reversed){
		float tau = 2*Mathf.PI;
		float delay = 0.2f;
		firing = true;
		while(!readyToFire){
			yield return new WaitForFixedUpdate();
		}
		while(firing){
			switch(patternNumber){
				default:
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
					break;
			}
			yield return new WaitForSeconds(delay);
		}
	}
}
