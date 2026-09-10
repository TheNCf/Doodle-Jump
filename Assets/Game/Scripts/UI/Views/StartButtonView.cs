using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Views
{
    public class StartButtonView : MonoBehaviour
    {
        [Data("OnStartClick")] [SerializeField] public Button button;
    }
}