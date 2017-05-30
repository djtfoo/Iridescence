using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SerialiseSpriteAnimation
{
    public string name;
    public Texture2D texture;   // file name of sprite sheet
    //public Sprite spritesheet;
    public int framesPerStrip;  // number of frames per animation strip
    public bool multiDirectional;   // whether it's 8-directional or not
}

public struct SpriteAnimation
{
    public bool loop;
    public Sprite[] sprites;
    public int framesPerStrip;  // number of frames per animation strip
    public bool multiDirectional;   // whether it's 8-directional or not
}

public class SpriteAnimator : MonoBehaviour {

    public SerialiseSpriteAnimation[] initAnimationsList; // FOR INITIALISING ONLY

    public float frameTime = 0.5f;

    private Dictionary<string, SpriteAnimation> animationsList;
    private SpriteAnimation currSprAnimation;   // current sprite animation
    private SpriteRenderer sr;  // a handle to this GameObject's SpriteRenderer
    private int frame = 0;  // this frame
    private int currDirection = 0;  // which direction currently facing
    private float timeElapsed = 0f;

	// Use this for initialization
	void Start () {
        animationsList = new Dictionary<string, SpriteAnimation>();
        sr = this.GetComponent<SpriteRenderer>();

        for (int i = 0; i < initAnimationsList.Length; ++i) {
            SpriteAnimation temp;
            temp.loop = true;
            temp.sprites = Resources.LoadAll<Sprite>("Sprites/" + initAnimationsList[i].texture.name);
            temp.framesPerStrip = initAnimationsList[i].framesPerStrip;
            temp.multiDirectional = initAnimationsList[i].multiDirectional;

            animationsList.Add(initAnimationsList[i].name, temp);

            if (i == 0)
                currSprAnimation = temp;
        }
        //currSprAnimation = initAnimationsList[0];
        sr.sprite = currSprAnimation.sprites[frame];    // frame = 0
    }
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;

        while (timeElapsed >= frameTime)
        {
            timeElapsed -= frameTime;
            ++frame;
            if (frame >= currSprAnimation.framesPerStrip * (1 + currDirection))
                frame -= currSprAnimation.framesPerStrip;

            sr.sprite = currSprAnimation.sprites[frame];
        }

    }

    public void ChangeAnimation(string animationName)
    {

    }

}
