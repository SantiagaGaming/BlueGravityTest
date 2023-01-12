using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothes : MonoBehaviour
{
    [SerializeField] private Sprite _yellow;
    [SerializeField] private Sprite _green;
    [SerializeField] private Sprite _noCloth;
    [SerializeField]private SpriteRenderer _sprite;

    public void SetCloth(ItemColor color)
    {
        if (color == ItemColor.Green)
            _sprite.sprite = _green;
        else if (color == ItemColor.Yellow)
            _sprite.sprite = _yellow;
        else _sprite.sprite = _noCloth;
    }
    public void SetCloth()
    {
       _sprite.sprite = _noCloth;
    }

}
