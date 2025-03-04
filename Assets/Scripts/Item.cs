using UnityEngine;

[RequireComponent(typeof(DropLayerDeterminer))]
public class Item : MonoBehaviour
{
    private DropLayerDeterminer _layerDetermine;

    private void Start()
    {
        //хэшируем компонент, чтоб в дальнейшем управлять им
        _layerDetermine = GetComponent<DropLayerDeterminer>();
    }

    public void SwitchActivity(bool isActiv)
    {
        //переключаем активность компонента в зависимости от полученого параметра
        _layerDetermine.enabled = isActiv;
    }
}