using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private float ghostDelay;
    private float ghostDelayTime;
    public GameObject ghost;
    public bool makeGhost;

    private void Start()
    {
        ghostDelayTime = ghostDelay;
    }

    private void FixedUpdate()
    {
        if (this.makeGhost)
        {
            if(this.ghostDelayTime > 0)
            {
                this.ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                this.ghostDelayTime = ghostDelay;
                Destroy(currentGhost, 0.3f);
            }
        }
    }
}
