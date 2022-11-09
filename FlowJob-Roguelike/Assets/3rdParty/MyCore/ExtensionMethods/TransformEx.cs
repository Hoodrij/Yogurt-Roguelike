using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class TransformEx
    {
        // Template
        // DOTween.To(()=> myFloat, x=> myFloat = x, 52, 1);
        // DOTween.To(x => progress = x, 1, 52, 1);


        /////////////////////////////////// POSITION
        public static void MoveTo(this Transform t, Vector3 pos)
        {
            Move(t, pos, false);
        }

        public static void MoveBy(this Transform t, Vector3 pos)
        {
            Move(t, pos, true);
        }

        private static void Move(this Transform t, Vector3 position, bool relative = false)
        {
            if (relative)
                t.position += position;
            else
                t.position = position;
        }

        public static void MoveXTo(this Transform t, float pos)
        {
            MoveX(t, pos, false);
        }

        public static void MoveXBy(this Transform t, float pos)
        {
            MoveX(t, pos, true);
        }

        private static void MoveX(this Transform t, float x, bool relative = false)
        {
            if (relative)
                t.position += new Vector3(x, 0, 0);
            else
                t.position = new Vector3(x, t.position.y, t.position.z);
        }

        public static void MoveYTo(this Transform t, float pos)
        {
            MoveY(t, pos, false);
        }

        public static void MoveYBy(this Transform t, float pos)
        {
            MoveY(t, pos, true);
        }

        private static void MoveY(this Transform t, float y, bool relative = false)
        {
            if (relative)
                t.position += new Vector3(0, y, 0);
            else
                t.position = new Vector3(t.position.x, y, t.position.z);
        }

        public static void MoveZTo(this Transform t, float pos)
        {
            MoveZ(t, pos, false);
        }

        public static void MoveZBy(this Transform t, float pos)
        {
            MoveZ(t, pos, true);
        }

        private static void MoveZ(this Transform t, float z, bool relative = false)
        {
            if (relative)
                t.position += new Vector3(0, 0, z);
            else
                t.position = new Vector3(t.position.x, t.position.y, z);
        }


        /////////////////////////////////// ANCHORED POSITION
        public static void MoveTo(this RectTransform t, Vector2 pos)
        {
            Move(t, pos, false);
        }

        public static void MoveBy(this RectTransform t, Vector2 pos)
        {
            Move(t, pos, true);
        }

        private static void Move(this RectTransform t, Vector2 position, bool relative = false)
        {
            if (relative)
                t.anchoredPosition += position;
            else
                t.anchoredPosition = position;
        }

        public static void MoveXTo(this RectTransform t, float pos)
        {
            MoveX(t, pos, false);
        }

        public static void MoveXBy(this RectTransform t, float pos)
        {
            MoveX(t, pos, true);
        }

        private static void MoveX(this RectTransform t, float x, bool relative = false)
        {
            if (relative)
                t.anchoredPosition += new Vector2(x, 0);
            else
                t.anchoredPosition = new Vector2(x, t.anchoredPosition.y);
        }

        public static void MoveYTo(this RectTransform t, float pos)
        {
            MoveY(t, pos, false);
        }

        public static void MoveYBy(this RectTransform t, float pos)
        {
            MoveY(t, pos, true);
        }

        private static void MoveY(this RectTransform t, float y, bool relative = false)
        {
            if (relative)
                t.anchoredPosition += new Vector2(0, y);
            else
                t.anchoredPosition = new Vector2(t.anchoredPosition.x, y);
        }


        //////////////////////////////// LOCAL POSITION
        public static void LocalMoveTo(this Transform t, Vector3 pos)
        {
            LocalMove(t, pos, false);
        }

        public static void LocalMoveBy(this Transform t, Vector3 pos)
        {
            LocalMove(t, pos, true);
        }

        private static void LocalMove(this Transform t, Vector3 position, bool relative = false)
        {
            if (relative)
                t.localPosition += position;
            else
                t.localPosition = position;
        }

        public static void LocalMoveXTo(this Transform t, float pos)
        {
            LocalMoveX(t, pos, false);
        }

        public static void LocalMoveXBy(this Transform t, float pos)
        {
            LocalMoveX(t, pos, true);
        }

        private static void LocalMoveX(this Transform t, float x, bool relative = false)
        {
            if (relative)
                t.localPosition += new Vector3(x, 0, 0);
            else
                t.localPosition = new Vector3(x, t.localPosition.y, t.localPosition.z);
        }

        public static void LocalMoveYTo(this Transform t, float pos)
        {
            LocalMoveY(t, pos, false);
        }

        public static void LocalMoveYBy(this Transform t, float pos)
        {
            LocalMoveY(t, pos, true);
        }

        private static void LocalMoveY(this Transform t, float y, bool relative = false)
        {
            if (relative)
                t.localPosition += new Vector3(0, y, 0);
            else
                t.localPosition = new Vector3(t.localPosition.x, y, t.localPosition.z);
        }

        public static void LocalMoveZTo(this Transform t, float pos)
        {
            LocalMoveZ(t, pos, false);
        }

        public static void LocalMoveZBy(this Transform t, float pos)
        {
            LocalMoveZ(t, pos, true);
        }

        private static void LocalMoveZ(this Transform t, float z, bool relative = false)
        {
            if (relative)
                t.localPosition += new Vector3(0, 0, z);
            else
                t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, z);
        }


        //////////////////////////////// SCALE
        public static void ScaleTo(this Transform t, Vector3 scale)
        {
            Scale(t, scale, false);
        }

        public static void ScaleBy(this Transform t, Vector3 scale)
        {
            Scale(t, scale, true);
        }

        private static void Scale(this Transform t, Vector3 scale, bool relative = false)
        {
            if (relative)
                t.localScale += scale;
            else
                t.localScale = scale;
        }

        public static void ScaleTo(this Transform t, float scale)
        {
            Scale(t, scale, false);
        }

        public static void ScaleBy(this Transform t, float scale)
        {
            Scale(t, scale, true);
        }

        private static void Scale(this Transform t, float scale, bool relative = false)
        {
            if (relative)
                t.localScale += new Vector3(scale, scale, scale);
            else
                t.localScale = new Vector3(scale, scale, scale);
        }

        public static void ScaleXTo(this Transform t, float scale)
        {
            ScaleX(t, scale, false);
        }

        public static void ScaleXBy(this Transform t, float scale)
        {
            ScaleX(t, scale, true);
        }

        private static void ScaleX(this Transform t, float x, bool relative = false)
        {
            if (relative)
                t.localScale += new Vector3(x, 0, 0);
            else
                t.localScale = new Vector3(x, t.localScale.y, t.localScale.z);
        }

        public static void ScaleYTo(this Transform t, float scale)
        {
            ScaleY(t, scale, false);
        }

        public static void ScaleYBy(this Transform t, float scale)
        {
            ScaleY(t, scale, true);
        }

        private static void ScaleY(this Transform t, float y, bool relative = false)
        {
            if (relative)
                t.localScale += new Vector3(0, y, 0);
            else
                t.localScale = new Vector3(t.localScale.x, y, t.localScale.z);
        }

        public static void ScaleZTo(this Transform t, float scale)
        {
            ScaleZ(t, scale, false);
        }

        public static void ScaleZBy(this Transform t, float scale)
        {
            ScaleZ(t, scale, true);
        }

        private static void ScaleZ(this Transform t, float z, bool relative = false)
        {
            if (relative)
                t.localScale += new Vector3(0, 0, z);
            else
                t.localScale = new Vector3(t.localScale.x, t.localScale.y, z);
        }


        ///////////////////////////////// ROTATION
        public static void SetRotation(this Transform t, Vector3 rotation)
        {
            t.eulerAngles = new Vector3(rotation.x, rotation.y, rotation.z);
        }

        public static void RotateBy(this Transform t, Vector3 rotation)
        {
            t.eulerAngles = t.eulerAngles + new Vector3(rotation.x, rotation.y, rotation.z);
        }

        public static void RotateX(this Transform t, float x)
        {
            t.eulerAngles = new Vector3(x, t.localRotation.y, t.localRotation.z);
        }

        public static void RotateY(this Transform t, float y)
        {
            t.eulerAngles = new Vector3(t.localRotation.x, y, t.localRotation.z);
        }

        public static void RotateZ(this Transform t, float z)
        {
            t.eulerAngles = new Vector3(t.localRotation.x, t.localRotation.y, z);
        }


        public static List<Transform> Childs(this Transform transform, bool recursive = false,
            bool includeInactive = false)
        {
            List<Transform> result = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf || includeInactive)
                    result.Add(transform.GetChild(i));

                if (!recursive) continue;

                result.AddRange(transform.GetChild(i).Childs(true, includeInactive)
                    .Where(childOfChild => transform.GetChild(i).gameObject.activeSelf || includeInactive));
            }

            return result;
        }

        public static void ClearChilds(this Transform transform)
        {
            foreach (Transform child in transform) Object.Destroy(child.gameObject);
        }
    }
}