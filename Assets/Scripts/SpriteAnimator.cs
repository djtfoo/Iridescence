using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SPRITE_DIRECTION
{
    DIR_DOWN = 0,
    DIR_DOWNLEFT,
    DIR_LEFT,
    DIR_UPLEFT,
    DIR_UP,
    DIR_UPRIGHT,
    DIR_RIGHT,
    DIR_DOWNRIGHT,

    DIR_TOTAL
}

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

    public float frameTime;

    private Dictionary<string, SpriteAnimation> animationsList;
    private SpriteAnimation currSprAnimation;   // current sprite animation
    private SpriteRenderer sr;  // a handle to this GameObject's SpriteRenderer
    private int currFrame = 0;  // this frame
    private int currDirection = (int)SPRITE_DIRECTION.DIR_DOWN;  // which direction currently facing
    private float timeElapsed = 0f;

    // for idle ONLY
    private bool isIdleAnimation = true;
    private bool isBlinking = false;
    private int direction = 1;

	// Use this for initialization
	void Start () {
        Random.seed = 0;

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
        sr.sprite = currSprAnimation.sprites[currFrame];    // frame = 0
    }
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;

        if (isIdleAnimation)
        {
            if (timeElapsed >= frameTime)
            {
                timeElapsed -= frameTime;
                if (isBlinking)
                {
                    currFrame += direction;
                    if (currFrame >= currSprAnimation.framesPerStrip * (1 + currDirection) - 1)
                    {
                        direction = -1;
                    }
                    else if (currFrame <= currSprAnimation.framesPerStrip * currDirection)
                    {
                        direction = 1;
                        isBlinking = false;
                    }
                }
                else
                {
                    ++currFrame;
                    if (currFrame >= currSprAnimation.framesPerStrip * currDirection + 2)
                    {
                        int rand = (int)Random.Range(0f, 2f);
                        if (rand == 0)
                        {   // normal idle
                            currFrame -= 2;
                        }
                        else
                        {   // blink loop
                            isBlinking = true;
                        }
                    }
                }

                sr.sprite = currSprAnimation.sprites[currFrame];
            }

            return;
        }

        if (timeElapsed >= frameTime)
        {
            timeElapsed -= frameTime;
            ++currFrame;
            if (currFrame >= currSprAnimation.framesPerStrip * (1 + currDirection))
                currFrame -= currSprAnimation.framesPerStrip;

            sr.sprite = currSprAnimation.sprites[currFrame];
        }
    }

    public void ChangeAnimation(string animationName)
    {
        currSprAnimation = animationsList[animationName];
        currFrame = currSprAnimation.framesPerStrip * currDirection;
        sr.sprite = currSprAnimation.sprites[currFrame];    // frame 0 of that strip

        isBlinking = false;

        if (animationName == "Idle")
            isIdleAnimation = true;
        else
            isIdleAnimation = false;
    }

    public void ChangeDirection(int dir)
    {
        currFrame = dir * currSprAnimation.framesPerStrip + currFrame % currSprAnimation.framesPerStrip;
        currDirection = dir;
        sr.sprite = currSprAnimation.sprites[currFrame];
    }
    public void ChangeDirection(SPRITE_DIRECTION dir)
    {
        ChangeDirection((int)dir);
    }

}
