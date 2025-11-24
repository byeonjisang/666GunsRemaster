<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButtonView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image coolDownImage;

    public event Action OnClick;

    private void Start()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());
    }
}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButtonView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image coolDownImage;

    public event Action OnClick;

    private void Start()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());
    }
}
>>>>>>> origin/main
