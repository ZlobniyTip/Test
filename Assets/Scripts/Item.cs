using UnityEngine;

[RequireComponent(typeof(DropLayerDeterminer))]
public class Item : MonoBehaviour
{
    private DropLayerDeterminer _layerDetermine;

    private void Start()
    {
        _layerDetermine = GetComponent<DropLayerDeterminer>();
    }

    public void SwitchActivity(bool isActiv)
    {
        _layerDetermine.enabled = isActiv;
    }
}