#pragma strict

var newtexture : Texture;
var isTriggered = false;
var rendleft : GameObject;
var rendright : GameObject;

 function OnTriggerEnter (col : Collider) {
    isTriggered = true;
}

 function OnTriggerExit (col : Collider) {
    isTriggered = false;
    }

 function Update () {
    if(isTriggered)
    {
        rendleft =  GameObject.Find("LeftTrackWall");
        rendleft.renderer.material.mainTexture = newtexture; 
        rendright =  GameObject.Find("RightTrackWall");
        rendright.renderer.material.mainTexture = newtexture;
        isTriggered = false;
    }
}