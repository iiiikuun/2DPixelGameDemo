using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float xParallaxEffect;
    [SerializeField] private float yParallaxEffect;

    private float xPosition;
    private float yPosition;
    private float length;

    private void Awake()
    {
        xPosition = transform.position.x;
        yPosition = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate()
    {
        float xDistanceToMove = player.transform.position.x * xParallaxEffect;
        float yDistanceToMove = player.transform.position.y * yParallaxEffect;
        float distanceMoved = player.transform.position.x * (1 - xParallaxEffect);

        transform.position = new Vector3(xPosition + xDistanceToMove, yPosition + yDistanceToMove);

        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}


