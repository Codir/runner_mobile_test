using UI.Models;
using UnityEngine;

namespace UI.Views
{
    public class BaseScreenView<T> : MonoBehaviour
        where T : IScreenModelView, new()
    {
        public readonly T Model;

        protected BaseScreenView()
        {
            Model = new T();
        }
    }
}