using UnityEngine;
using System.Collections;

public abstract class PatternVariation : MonoBehaviour{
	public bool reversed;
	public float bulletSpeed;
	public Bullet bullet;

	public abstract IEnumerator Fire();
}
