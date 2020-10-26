using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafBehavior : MonoBehaviour
{
    public Resource Health;
    public Image LeafImage;
    public Sprite[] LeafSprites;
    // Start is called before the first frame update
    void Start()
    {
        Health.EVT_OnResourceAdded.AddListener(UpdateLeafSprite);
        Health.EVT_OnResourceSubtracted.AddListener(UpdateLeafSprite);

    }

    private void UpdateLeafSprite()
    {
        if (Health.Value >= 50 && Health.Value <= 100)
        {
            LeafImage.sprite = LeafSprites[0];
        }
        if (Health.Value >=20 && Health.Value <=50)
        {
            LeafImage.sprite = LeafSprites[1];
        }
        if (Health.Value >= 0 && Health.Value <= 20)
        {
            LeafImage.sprite = LeafSprites[2];
        }
    }


}
