using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour
{
    // offset to make sure that sprite is already spawned before we reach exactly at the edge
    public int offsetX = 2;

    //these are used to decide if we need to spawn the sprite
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    //used to decide if object is not tilable
    public bool reverseScale = false;

    private float _spriteWidth = 0f; //width of the sprite
    private Camera _cam;
    private Transform _myTransform;

    [SerializeField]
    float edgeVisiblePositionRight;
    [SerializeField]
    float edgeVisiblePositionLeft;

    private void Awake()
    {
        _cam = Camera.main;
        _myTransform = transform;
    }

    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        _spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //check if buddy need to be spawned
        if(hasARightBuddy == false || hasALeftBuddy == false)
        {
            //Horizontal size of viewing volume depends on the aspect ratio
            //orthographic size if half the size of vertical viewing volume
            float camHorizontalExtend = _cam.orthographicSize * Screen.width / Screen.height;

            //calculate the x position where camera can see the edge of the sprite
            
            edgeVisiblePositionRight = (_myTransform.position.x + _spriteWidth / 2) - camHorizontalExtend;
            edgeVisiblePositionLeft = (_myTransform.position.x - _spriteWidth / 2) + camHorizontalExtend;

            //checking if we can see the edge of the sprite
            if(_cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeANewBuddy(1);
                hasARightBuddy = true;
            }
            else if(_cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
            {
                MakeANewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    void MakeANewBuddy(int rightOrLeft)
    {
        //calculating the new position for our buddy
        Vector3 newPos = new Vector3(_myTransform.position.x + _spriteWidth * rightOrLeft,
                                 _myTransform.position.y,
                                 _myTransform.position.z);

        //Instantiating new buddy
        Transform newBuddy = Instantiate(_myTransform, newPos, _myTransform.rotation) as Transform;

        //if not tilable then reverse the scale
        if(reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1,
                                              newBuddy.localScale.y,
                                              newBuddy.localScale.z);
        }

        newBuddy.parent = _myTransform.parent;
        if(rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }
}
