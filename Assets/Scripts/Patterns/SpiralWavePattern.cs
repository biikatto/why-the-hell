using UnityEngine;
using System.Collections;

public class SpiralWavePattern : BulletPattern{
	
	private int nBullets = 4;
	private float mag = 1.5f;
	private float offset = 0.075f;
	private float angleWidth = 0.3f;
	private const float TAU = Mathf.PI * 2f;
	
	public override IEnumerator Fire(){
		if(reversed){
			offset += 0.5f;
		}
		while(true){
		    for(int i=0;i<4;i++){
			    for(int j=0;j<nBullets;j++){
				    float phaseA = (float)i/nBullets + 0.25f*(float)j/nBullets;
				    float thetaA = (phaseA * TAU * angleWidth) + TAU*offset;

				    float thetaB = thetaA + (0.5f*TAU);

				    Vector3 bulletPositionA = new Vector3(
						    Mathf.Sin(thetaA)*mag, Mathf.Cos(thetaA)*mag, 0);
					
				    Vector3 bulletPositionB = new Vector3(
						    Mathf.Sin(thetaB)*mag, Mathf.Cos(thetaB)*mag, 0);

				    Bullet thisBullet = GameObject.Instantiate(
						    bullet,
						    (bulletPositionA + transform.position),
						    Quaternion.identity) as Bullet;

				    Bullet thatBullet = GameObject.Instantiate(
						    bullet,
						    (bulletPositionB + transform.position),
						    Quaternion.identity) as Bullet;

				    thisBullet.velocity = bulletPositionA.normalized * bulletSpeed;
				    thatBullet.velocity = bulletPositionB.normalized * bulletSpeed;
			    }
			    offset += 0.01625f;
			}
			yield return new WaitForSeconds(0.15f);
			offset += 0.125f;
		}
	}
}
