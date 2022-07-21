using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitFlash
{
    // FIELDS
    private float defaultDuration = 0.18f;
    private float duration;

    private MonoBehaviour entity;
    private SpriteRenderer sprite;
    private Image image;

    private Shader whiteShader = Shader.Find("GUI/Text Shader");
    private Shader normalShader = Shader.Find("Sprites/Default");

    // CONSTRUCTORS
    public HitFlash(MonoBehaviour _entity, SpriteRenderer _sprite, float _duration)
    {
        sprite = _sprite;
        duration = _duration;
        entity = _entity;
    }
    public HitFlash(MonoBehaviour _entity, SpriteRenderer _sprite)
    {
        entity = _entity;
        sprite = _sprite;
        duration = defaultDuration;
    }
    public HitFlash(MonoBehaviour _entity, Image _image, float _duration)
    {
        image = _image;
        duration = _duration;
        entity = _entity;
    }
    public HitFlash(MonoBehaviour _entity, Image _image)
    {
        entity = _entity;
        image = _image;
        duration = defaultDuration;
    }

    // METHODS
    public void Flash()
    {
        entity.StartCoroutine(CO_Flash());
    }
    IEnumerator CO_Flash()
    {
        WhiteSprite();
        yield return new WaitForSeconds(duration);
        NormalSprite();
    }

    void WhiteSprite()
    {
        if (sprite != null)
        {
            sprite.material.shader = whiteShader;
            sprite.color = Color.white;
        }

        if (image != null)
        {
            image.material.shader = whiteShader;
            image.color = Color.white;
        }
    }
    void NormalSprite()
    {
        if (sprite != null)
        {
            sprite.material.shader = normalShader;
            sprite.color = Color.white;
        }
            
        if (image != null)
        {
            image.material.shader = normalShader;
            image.color = Color.white;
        }
    }
}
