using UnityEngine;

public class DropLayerDeterminer : MonoBehaviour
{
    [SerializeField] private LayerMask _background;
    [SerializeField] private LayerMask _midground;
    [SerializeField] private float _lengthRaycastBackground = 0.5f;
    [SerializeField] private float _lengthRaycastMidground = 2;

    private void Update()
    {
        RaycastHit2D hitMidground = Physics2D.Raycast(transform.position, Vector2.down, _lengthRaycastMidground, _midground);
        RaycastHit2D hitBackground = Physics2D.Raycast(transform.position, Vector2.down, _lengthRaycastBackground, _background);

        if (hitBackground)
        {
            if (hitBackground.collider.gameObject.layer != gameObject.layer)
            {
                gameObject.layer = hitBackground.collider.gameObject.layer;
            }
        }
        else if (hitMidground)
        {
            if (hitMidground.collider.gameObject.layer != gameObject.layer)
            {
                gameObject.layer = hitMidground.collider.gameObject.layer;
            }
        }
        else
        {
            gameObject.layer = gameObject.layer;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _lengthRaycastBackground);
    }
}