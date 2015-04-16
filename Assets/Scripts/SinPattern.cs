using UnityEngine;
using System.Collections;

public class SinPattern : PatternVariation{
	
	private int nBullets = 8;
	private float mag = 1.0f;
	private float offset = 0.075f;
	private float sinWidth = 0.25f;
	private float sinFreq = 2f;
	private const float TAU = Mathf.PI * 2f;
	
	public override IEnumerator Fire(){
		Debug.Log("firing");
		if(reversed){
			offset += 0.5f;
		}
		while(true){
			for(int i=0;i<nBullets;i++){
				float theta = ((float)i/nBullets) * TAU;
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
			offset += 0.01625f;
			yield return new WaitForSeconds(0.2f);
		}
	}
}
