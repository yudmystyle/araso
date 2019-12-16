using UnityEngine;

public class CategoryCanvasSetter : MonoBehaviour
{
    private void Awake()
    {
        AlignComponents();
    }

	public void AlignComponents()
    {
        RectTransform T = this.gameObject.GetComponent<RectTransform>();

        if (!T)
            return;

        T.anchorMin = new Vector2(0, 1);
        T.anchorMax = new Vector2(1, 1);
        T.pivot = new Vector2(0.5f, 1);

        T.anchoredPosition = new Vector2(0, 0);
        T.sizeDelta = new Vector3(0, 0, 0);
    }
}
