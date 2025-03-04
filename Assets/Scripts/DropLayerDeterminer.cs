using UnityEngine;

public class DropLayerDeterminer : MonoBehaviour
{
    [SerializeField] private LayerMask _background;
    [SerializeField] private LayerMask _midground;
    [SerializeField] private float _lengthRaycastBackground = 0.5f;
    [SerializeField] private float _lengthRaycastMidground = 2;

    private void Update()
    {
        //рейкасты для каждого слоя с индивидуальной длинной луча
        RaycastHit2D hitMidground = Physics2D.Raycast(transform.position, Vector2.down, _lengthRaycastMidground, _midground);
        RaycastHit2D hitBackground = Physics2D.Raycast(transform.position, Vector2.down, _lengthRaycastBackground, _background);

        //если нижний слой виден рейкастом, то он будет в приоритете
        if (hitBackground)
        {
            if (hitBackground.collider.gameObject.layer != gameObject.layer)
            {
                gameObject.layer = hitBackground.collider.gameObject.layer;
            }
        }
        //если нет то буду выбираться обьекты на среднем слое (полки, столы) 
        else if (hitMidground)
        {
            if (hitMidground.collider.gameObject.layer != gameObject.layer)
            {
                gameObject.layer = hitMidground.collider.gameObject.layer;
            }
        }
        //оначе остается на текущем слое
        else
        {
            gameObject.layer = gameObject.layer;
        }
    }
}