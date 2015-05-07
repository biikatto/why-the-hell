using UnityEngine;
using System.Collections;

public class EightLinePattern : BulletPattern{
	
	private int nBullets = 8;
	private float mag = 1.0f;
	private float angleWidth = 1.0f;
	private const float TAU = Mathf.PI * 2f;
	
	public override IEnumerator Fire(){
		while(true){
			for(int i=0;i<nBullets;i++){
				float phase = (float)i/nBullets;
				float theta = (phase * TAU * angleWidth);
				Vector3 bulletPosition = new Vector3(
						Mathf.Sin(theta)*mag, Mathf.Cos(theta)*mag, 0);

				Bullet thisBullet = GameObject.Instantiate(
						bullet,
						(bulletPosition + transform.position),
						Quaternion.identity) as Bullet;

				thisBullet.velocity = bulletPosition.normalized * bulletSpeed;
			}
			yield return new WaitForSeconds(0.2f);
		}
	}
}
