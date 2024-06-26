using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class WrapLayoutGroup : LayoutGroup
{
    public float spacing = 5f;
    public bool childForceExpandWidth = false;
    public bool childForceExpandHeight = false;

    private float m_ContentHeight;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        
        float width = rectTransform.rect.width;
        float x = 0f;
        float y = 0f;
        float rowHeight = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            var item = rectChildren[i];
            
            var itemWidth = LayoutUtility.GetPreferredWidth(item);
            var itemHeight = LayoutUtility.GetPreferredHeight(item);

            if (x + itemWidth > width)
            {
                x = 0;
                y += rowHeight + spacing;
                rowHeight = 0;
            }

            rowHeight = Mathf.Max(rowHeight, itemHeight);
            x += itemWidth + spacing;
        }

        m_ContentHeight = y + rowHeight;
    }

    public override void SetLayoutHorizontal()
    {
        float width = rectTransform.rect.width;
        float x = 0f;
        float y = 0f;
        float rowHeight = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            var item = rectChildren[i];
            
            var itemWidth = LayoutUtility.GetPreferredWidth(item);
            var itemHeight = LayoutUtility.GetPreferredHeight(item);

            if (x + itemWidth > width)
            {
                x = 0;
                y += rowHeight + spacing;
                rowHeight = 0;
            }

            SetChildAlongAxis(item, 0, x, itemWidth);
            SetChildAlongAxis(item, 1, y, itemHeight);

            rowHeight = Mathf.Max(rowHeight, itemHeight);
            x += itemWidth + spacing;
        }
    }

    public override void SetLayoutVertical()
    {
        // Not needed as vertical layout is handled in SetLayoutHorizontal
    }

    public override void CalculateLayoutInputVertical()
    {
        SetLayoutInputForAxis(m_ContentHeight, m_ContentHeight, 0, 1);
    }
}