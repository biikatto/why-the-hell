using UnityEngine;
using System.Collections;

public abstract class BulletPattern : MonoBehaviour{
	public bool reversed;
	public float bulletSpeed;
	public Bullet bullet;

	public abstract IEnumerator Fire();
}
