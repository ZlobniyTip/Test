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
            //если предмет подобран при повтором нажати ны мышь предмет упадет
            if (_isTaken)
            {
                DropItem();
            }
            //если предмет не подобран рука его возьмет
            else
            {
                CheckForItemCollision();
            }
        }
    }

    private void CheckForItemCollision()
    {
        //метод провер€ет все коллайдеры попавшие в радиус оверлап—фер
        Collider2D[] items = Physics2D.OverlapCircleAll(_hand.position, 0.2f);

        if (items.Length > 0)
        {
            for (int i = 0; i < items.Length; i++)
            {
                //если на геймќбжекте колайдера есть компонент Item, то мы берем его в руку
                if (items[i].TryGetComponent(out Item item))
                {
                    PickupItem(item);

                    //включаетс€ компонент который использует рейкаст предмета дл€ определени€ сло€ на который он может упасть, который мы вз€ли в руку
                    item.SwitchActivity(true);
                }
            }
        }
    }

    private void PickupItem(Item item)
    {
        //мен€ем спрайт вз€вшего предмет в руку
        _handSpriteRenderer.sprite = _handSprites[1];
        //бул поле котора€ говорит о том что мы держим предмет в руке
        _isTaken = true;
        //получаем риджитбади текущего айтема и отключаем ему физические свойства(гравитацию)
        _rbCurrentItem = item.GetComponent<Rigidbody2D>();
        _rbCurrentItem.simulated = false;
        //в поле добавл€ем текущий айтем,устанавливаем ему родительский обьект "руку",чтоб он следовал за ним и обнул€ем локальные координаты
        _currentItem = item;
        _currentItem.transform.SetParent(_hand);
        _currentItem.transform.localPosition = Vector3.zero;
    }

    private void DropItem()
    {
        //мен€ем спрайт руки на дефолтный
        _handSpriteRenderer.sprite = _handSprites[0];
        //выключаем компонент который определ€ет слои под предметом
        _currentItem.SwitchActivity(false);
        //включаем симул€цию физики айтема и убираем его из дочерних обьектов "руки"
        _rbCurrentItem.simulated = true;
        _currentItem.transform.parent = null;
        //бул поле устанавливаем в фолс (не держит предмет) и обнул€ем поле текущего предмета
        _isTaken = false;
        _currentItem = null;
    }
}