using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Core.Tools.ExtensionMethods
{
    public static class RectTransformEx
    {
        public static float GetWorldTop(this RectTransform rt)
        {
            return rt.position.y + (1f - rt.pivot.y) * rt.rect.height;
        }

        public static float GetWorldBottom(this RectTransform rt)
        {
            return rt.position.y - rt.pivot.y * rt.rect.height;
        }

        public static float GetWorldLeft(this RectTransform rt)
        {
            return rt.position.x - rt.pivot.x * rt.rect.width;
        }

        public static float GetWorldRight(this RectTransform rt)
        {
            return rt.position.x + (1f - rt.pivot.x) * rt.rect.width;
        }

        public static Bounds GetBoundsWithChildren(this RectTransform @this, bool recursive = true)
        {
            Bounds result = new Bounds(@this.localPosition, @this.GetSize());

            Vector2 layoutElementSize = GetLayoutElementSize(@this);
            Vector2 layoutGroupSize = GetLayoutGroupSize(@this);
            if (layoutElementSize != Vector2.zero)
                return new Bounds(@this.localPosition, layoutElementSize);
            if (layoutGroupSize != Vector2.zero)
                return new Bounds(@this.localPosition, layoutGroupSize);

            foreach (RectTransform child in @this)
            {
                if (!child.gameObject.activeSelf) continue;

                result.Encapsulate(new Bounds(child.localPosition, child.GetSize()));
            }

            return result;
        }

        public static Vector2 GetSize(this RectTransform @this)
        {
            Vector2 size = @this.sizeDelta;

            Vector2 layoutElementSize = GetLayoutElementSize(@this);
            Vector2 layoutGroupSize = GetLayoutGroupSize(@this);

            if (layoutElementSize != Vector2.zero)
                return layoutElementSize;
            if (layoutGroupSize != Vector2.zero)
                return layoutGroupSize;

            return size;
        }


        public static Vector2 GetLayoutGroupSize(this RectTransform @this)
        {
            LayoutGroup layoutGroup = @this.GetComponent<LayoutGroup>();
            if (layoutGroup != null) return new Vector2(layoutGroup.preferredWidth, layoutGroup.preferredHeight);

            return Vector2.zero;
        }

        public static Vector2 GetLayoutElementSize(this RectTransform @this)
        {
            LayoutElement layout = @this.GetComponent<LayoutElement>();
            if (layout != null && !layout.ignoreLayout) return new Vector2(layout.minWidth, layout.minHeight);

            return Vector2.zero;
        }

        public static Bounds GetBounds(this RectTransform @this)
        {
            return new Bounds(@this.position, @this.GetSize());
        }

        public static float GetBottom(this RectTransform @this)
        {
            return @this.anchoredPosition.y - @this.GetSize().y * @this.localScale.y * @this.pivot.y;
        }

        public static void ClearChilds(this RectTransform @this)
        {
            foreach (Transform t in @this) Object.Destroy(t.gameObject);
        }

        public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            if (rectTransform == null) return;

            Vector2 size = rectTransform.rect.size;
            Vector2 scale = rectTransform.localScale;
            Vector2 deltaPivot = rectTransform.pivot - pivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x * scale.x, deltaPivot.y * size.y * scale.y);
            rectTransform.pivot = pivot;
            rectTransform.localPosition -= deltaPosition;
        }
    }
}