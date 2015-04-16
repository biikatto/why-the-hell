using UnityEngine;

[System.Serializable]
public class HPManager : MonoBehaviour{
	[SerializeField]
	private int maxHP;
	public int MaxHP{
		get{
			return maxHP;
		}
		set{
			maxHP = value;
		}
	}

	[SerializeField]
	private int hp;
	public int HP{
		get{
			return hp;
		}
		set{
			hp = value;
		}
	}

	public void Start(){
		hp = maxHP;
	}
}
