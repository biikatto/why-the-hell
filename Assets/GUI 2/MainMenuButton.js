#pragma strict

function Start () {

}

function Update () {

}

function StartGame() {
	
	Debug.Log("going to Game");

	Application.LoadLevel("Game");

}

function loadMenu() {
	
	Debug.Log("Loading Menu");

	Application.LoadLevel("Menus");
}