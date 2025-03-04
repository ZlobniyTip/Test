using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private List<Sprite> _handSprites;

    private SpriteRenderer _handSpriteRenderer;
    private Rigidbody2D _rbCurrentItem;
    private Item _currentItem;
    private bool _isTaken = false;

    private void Awake()
    {
        _handSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isTaken)
            {
                DropItem();
            }
            else
            {
                CheckForItemCollision();
            }
        }
    }

    private void CheckForItemCollision()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(_hand.position, 0.2f);

        if (items.Length > 0)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].TryGetComponent(out Item item))
                {
                    PickupItem(item);
                    item.SwitchActivity(true);
                }
            }
        }
    }

    private void PickupItem(Item item)
    {
        _handSpriteRenderer.sprite = _handSprites[1];
        _isTaken = true;
        _rbCurrentItem = item.GetComponent<Rigidbody2D>();
        _rbCurrentItem.simulated = false;
        _currentItem = item;
        _currentItem.transform.SetParent(_hand);
        _currentItem.transform.localPosition = Vector3.zero;
    }

    private void DropItem()
    {
        _handSpriteRenderer.sprite = _handSprites[0];
        _currentItem.SwitchActivity(false);
        _rbCurrentItem.simulated = true;
        _currentItem.transform.parent = null;
        _isTaken = false;
        _currentItem = null;
    }
}