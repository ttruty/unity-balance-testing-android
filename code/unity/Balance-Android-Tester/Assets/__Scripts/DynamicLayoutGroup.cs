using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Layout group switches orientation automatically between landscape and
/// portrait layouts so it either acts like a VerticalLayoutGroup or a
/// HorizontalLayoutGroup.
/// </summary>
[AddComponentMenu("Layout/Dynamic Layout Group", 150)]
public class DynamicLayoutGroup : HorizontalOrVerticalLayoutGroup
{

    /// <summary>
    /// When is the layout vertical? In portrait or landscape?
    /// </summary>
    [SerializeField]
    public ScreenOrientation verticalWhen = ScreenOrientation.Portrait;

    public bool IsVertical { get { return GetIsVertical(); } }

    private bool GetIsVertical()
    {
        bool isVertical;

        if (UnityEngine.Screen.width > UnityEngine.Screen.height)
        {
            //orientation = ScreenOrientation.Landscape;
            isVertical = (verticalWhen == ScreenOrientation.Landscape) ? true : false;
        }
        else
        {
            //orientation = ScreenOrientation.Portrait;
            isVertical = (verticalWhen == ScreenOrientation.Portrait) ? true : false;
        }

        //Log.Debug(string.Format("DynamicLayoutGroup.OnRectTransformDimensionsChange() isVertical={0}a, ID={1}", isVertical, GetInstanceID()));
        return isVertical;
    }

    public override void CalculateLayoutInputHorizontal()
    {
        //Log.Debug(string.Format("DynamicLayoutGroup.CalculateLayoutInputHorizontal() IsVertical={0}, ID={1}", IsVertical, GetInstanceID()));

        base.CalculateLayoutInputHorizontal();
        CalcAlongAxis(0, isVertical: IsVertical);
    }

    public override void CalculateLayoutInputVertical()
    {
        //Log.Debug(string.Format("DynamicLayoutGroup.CalculateLayoutInputVertical() IsVertical={0}, ID={1}", IsVertical, GetInstanceID()));

        CalcAlongAxis(1, isVertical: IsVertical);
    }

    public override void SetLayoutHorizontal()
    {
        //Log.Debug(string.Format("DynamicLayoutGroup.SetLayoutHorizontal() IsVertical={0}, ID={1}", IsVertical, GetInstanceID()));

        SetChildrenAlongAxis(0, isVertical: IsVertical);
    }

    public override void SetLayoutVertical()
    {
        //Log.Debug(string.Format("DynamicLayoutGroup.SetLayoutVertical() IsVertical={0}, ID={1}", IsVertical, GetInstanceID()));

        SetChildrenAlongAxis(1, isVertical: IsVertical);
    }
}
