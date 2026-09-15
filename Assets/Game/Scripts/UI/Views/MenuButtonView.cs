using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Views
{
    public class MenuButtonView : MonoBehaviour
    {
        [Data("OnToMenuClick")] [SerializeField]
        public Button button;
    }
}