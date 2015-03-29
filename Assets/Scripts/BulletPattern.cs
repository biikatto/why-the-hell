using UnityEngine;
using System.Collections;

[System.Serializable]
public class BulletPattern : MonoBehaviour{
	[SerializeField]
	public Bullet bullet;

	private bool firing;
	
	private int patternNumber;
	public int PatternNumber{
		get{return patternNumber;}
		set{patternNumber = value;}
	}

	public void Start(){
		firing = false;
	}

	public void BeginFire(bool reversed){
		StartCoroutine(Fire(reversed));
	}

	public void EndFire(){
		firing = false;
	}

	private IEnumerator Fire(bool reversed){
		float tau = 2*Mathf.PI;
		float delay = 0.2f;
		firing = true;
		while(firing){
			switch(patternNumber){
				default:
					Debug.Log("Default pattern");
					int nBullets = 8;
					float offset = 0.075f;
					if(reversed){
						offset += 0.5f;
					}
					float angleWidth = 0.4f;
					float mag = 1.0f;
					float bulletSpeed = 4.0f;
					for(int i=0;i<nBullets;i++){
						float theta = (((float)i/nBullets) *
											tau*angleWidth) +
												tau*offset;
						Debug.Log(theta);
						Vector3 bulletPosition = new Vector3(
								Mathf.Sin(theta)*mag, Mathf.Cos(theta)*mag, 0);
						Bullet thisBullet = GameObject.Instantiate(
							bullet,
							(new Vector3(Mathf.Sin(theta)*mag, Mathf.Cos(theta)*mag, 0) +
								transform.position),
							Quaternion.identity) as Bullet;
						thisBullet.velocity = bulletPosition.normalized * bulletSpeed;
					}
					break;
			}
			yield return new WaitForSeconds(delay);
		}
	}
}
