using UnityEditor;

[CustomEditor(typeof(Ship))]
public class ShipEditor : Editor{
	public override void OnInspectorGUI(){
		Ship ship = (Ship)target;
		EditorGUILayout.LabelField("HP", ship.HPManager.HP.ToString());
		ship.HPManager.MaxHP = EditorGUILayout.IntField("Max HP", ship.HPManager.MaxHP);
		ship.Movement.MovementSpeed = EditorGUILayout.Slider(
			"Movement speed", ship.Movement.MovementSpeed, 1.0f, 10.0f);
		ship.Control.Player2 = EditorGUILayout.Toggle("Player 2", ship.Control.Player2);

		ship.Weapon.Bullet = EditorGUILayout.ObjectField("Bullet prefab", ship.Weapon.Bullet, typeof(Bullet), false) as Bullet;
	}
}
