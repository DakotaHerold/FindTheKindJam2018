using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Idle,
    Run
}

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour {

    [SerializeField]
    private float maxMoveSpeed, acceleration, decelerationMultiplier, laneTime, laneHeight, rightBound, leftBound;
    private float moveSpeed = 0;
    private bool switchingLanes;
    [SerializeField]
    private int lane;
    private CharacterState currentState = CharacterState.Idle;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(currentState != CharacterState.Idle)
        {
            UpdateMovement();
        }

        anim.SetInteger("State", (int)currentState);
        anim.SetFloat("SpeedMultiplier", 1 + (moveSpeed / maxMoveSpeed));
    }

    private void UpdateMovement()
    {
        if (!switchingLanes)
        {
            Vector2 movement = InputHandler.Instance.MoveAxes;

            if(movement.x != 0)
            {
                if (movement.x > 0 && transform.position.x < rightBound)
                {
                    if (moveSpeed > 0)
                    {
                        moveSpeed += acceleration * Time.deltaTime;
                    }
                    else
                    {
                        moveSpeed += decelerationMultiplier * acceleration * Time.deltaTime;
                    }

                    if (moveSpeed > maxMoveSpeed)
                    {
                        moveSpeed = maxMoveSpeed;
                    }
                }
                else if (movement.x < 0 && transform.position.x > leftBound)
                {
                    if (moveSpeed < 0)
                    {
                        moveSpeed -= acceleration * Time.deltaTime;
                    }
                    else
                    {
                        moveSpeed -= decelerationMultiplier * acceleration * Time.deltaTime;
                    }

                    if (moveSpeed < maxMoveSpeed * -1)
                    {
                        moveSpeed = maxMoveSpeed * -1;
                    }
                }
                else if (transform.position.x > rightBound || transform.position.x < leftBound)
                {
                    if (moveSpeed > 0)
                    {
                        moveSpeed -= decelerationMultiplier * acceleration * Time.deltaTime;

                        if (moveSpeed < 0)
                        {
                            moveSpeed = 0;
                        }
                    }
                    else if (moveSpeed < 0)
                    {
                        moveSpeed += decelerationMultiplier * acceleration * Time.deltaTime;

                        if (moveSpeed > 0)
                        {
                            moveSpeed = 0;
                        }
                    }
                }
            }

            if (movement.y > 0)
            {
                if (lane > 0)
                {
                    switchingLanes = true;
                    lane--;
                }
            }
            if (movement.y < 0)
            {
                if (lane < 2)
                {
                    switchingLanes = true;
                    lane++;
                }
            }
        }
        else
        {
            float target = (-1 * (lane * laneHeight)) + laneHeight;

            if(transform.position.y > target)
            {
                transform.Translate(Vector3.down * (laneHeight / laneTime) * Time.deltaTime);

                if(transform.position.y <= target)
                {
                    transform.position = new Vector3(transform.position.x, target, -1);
                    switchingLanes = false;
                }
            }
            else if (transform.position.y <= target)
            {
                transform.Translate(Vector3.up * (laneHeight / laneTime) * Time.deltaTime);

                if (transform.position.y > target)
                {
                    transform.position = new Vector3(transform.position.x, target, -1);
                    switchingLanes = false;
                }
            }

            if (moveSpeed > 0 && transform.position.x >= rightBound)
            {
                moveSpeed -= decelerationMultiplier * acceleration * Time.deltaTime;

                if (moveSpeed < 0)
                {
                    moveSpeed = 0;
                }
            }
            else if (moveSpeed < 0 && transform.position.x <= leftBound)
            {
                moveSpeed += decelerationMultiplier * acceleration * Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = 0;
                }
            }
        }

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public CharacterState State
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }
}
