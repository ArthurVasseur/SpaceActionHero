//
// © Arthur Vasseur arthurvasseur.fr
//

using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField][Tooltip("Background position")]private Transform backGround1;
    [SerializeField][Tooltip("Background position")]private Transform backGround2;
    [SerializeField][Tooltip("Scroll speed of background")]private float scrollSpeed;

    private float backgroundWidth;

    private void Start()
    {
        backgroundWidth = backGround1.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
    }

    private void Update()
    {
        backGround1.position = new Vector3(backGround1.position.x, backGround1.position.y - (scrollSpeed * Time.deltaTime), backGround2.position.z);
        backGround2.position -= new Vector3(0f, scrollSpeed * Time.deltaTime, 0f);
        if (backGround1.position.y < -backgroundWidth - 1)
            backGround1.position += new Vector3(0f, backgroundWidth * 2, 0f);

        if (backGround2.position.y < -backgroundWidth - 1)
                backGround2.position += new Vector3(0f, backgroundWidth * 2, 0f);
    }
}