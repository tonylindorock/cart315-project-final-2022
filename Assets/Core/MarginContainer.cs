using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarginContainer : LayoutGroup
{
    
    public override void CalculateLayoutInputHorizontal(){
        base.CalculateLayoutInputHorizontal();

        float totalWidth = rectTransform.rect.width;
        float totalHeight = rectTransform.rect.height;

        float childWidth = totalWidth - padding.left - padding.right;
        float childHeight = totalHeight - padding.top - padding.bottom;

        for (int i = 0; i < rectChildren.Count; i++){
            var item = rectChildren[i];
            var xPos = padding.left;
            var yPos = padding.top;

            SetChildAlongAxis(item, 0, xPos, childWidth);
            SetChildAlongAxis(item, 1, yPos, childHeight);
        }
    }

    public override void CalculateLayoutInputVertical(){
    }

    public override void SetLayoutHorizontal(){
    }

    public override void SetLayoutVertical(){
    }
}
