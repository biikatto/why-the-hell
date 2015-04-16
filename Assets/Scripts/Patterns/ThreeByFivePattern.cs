using UnityEngine;
using System.Collections;

public class ThreeByFivePattern : BulletPattern{
	
	private int nGroups = 5;
	private int bulletsPerGroup = 3;
	private float mag = 1.0f;
	private float groupAngleWidth = 1.0f;
	private float groupAngleOffset = 1f/32;
	private const float TAU = Mathf.PI * 2f;
	private float offset = 0.25f;
	
	public override IEnumerator Fire(){
	    if(reversed){
	        offset += 0.5f;
	    }
		while(true){
			for(int i=0;i<nGroups;i++){
				float groupPhase = (float)i/nGroups;
			    for(int j=0;j<bulletsPerGroup;j++){
			        float phaseOffset = (j - (bulletsPerGroup/2)) * groupAngleOffset * TAU;
				    float theta = (groupPhase * TAU * groupAngleWidth) +
				        phaseOffset + (offset * TAU);
				    Vector3 bulletPosition = new Vector3(
						    Mathf.Sin(theta)*mag, Mathf.Cos(theta)*mag, 0);

				    Bullet thisBullet = GameObject.Instantiate(
						    bullet,
						    (bulletPosition + transform.position),
						    Quaternion.identity) as Bullet;

				    thisBullet.velocity = bulletPosition.normalized * bulletSpeed;
				}
			}
			yield return new WaitForSeconds(0.2f);
		}
	}
}
