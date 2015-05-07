using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HPManager : MonoBehaviour{

	private List<GameObject> hpSprites;

	[SerializeField]
	private bool player2;
	public bool Player2{
		get{
			return player2;
		}
		set{
			player2 = value;
			FindSprites();
		}
	}

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
			if(0 <= hp && hp < 3){
				for(int i=0;i<hpSprites.Count;i++){
					hpSprites[i].SetActive(i<hp);
				}
			}
		}
	}

	private void FindSprites(){
		hpSprites = new List<GameObject>();
		string searchString = "P1_GUI";
		if(player2){
			searchString = "P2_GUI";
		}
		foreach(Transform candidate in GameObject.Find(searchString).transform){
			if(candidate.name.StartsWith("Life")){
				hpSprites.Add(candidate.gameObject);
			}
		}
	}

	public void Start(){
		hp = maxHP;
		FindSprites();
	}
}
