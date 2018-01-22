using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SGF.Utils
{
    public static class GameObjectUiils
    {
        internal static void SetActiveRecursively(GameObject gameObject, bool value)
        {
            //TODO:临时方法
            gameObject.SetActive(value);
        }

        internal static T EnsureComponent<T>(GameObject go)
        {
            throw new NotImplementedException();
        }
    }
}
