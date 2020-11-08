using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSetter : MonoBehaviour
{
    public SpriteRenderer WordSpriteRenderer;
    public SpriteRenderer NumberSpriteRenderer;
  public void SetSprites(Sprite WordSprite, Sprite NumberSprite)
    {
        WordSpriteRenderer.sprite = WordSprite;
        NumberSpriteRenderer.sprite = NumberSprite;
    }

  
}
