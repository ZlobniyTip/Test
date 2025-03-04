using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    private void Update()
    {
        //получаем позицию курсора и двигаем обьект "руки" месте с курсором
        Vector3 handPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(handPosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}