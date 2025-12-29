using UnityEngine;

public class ExitPresenter
{  
    // 나가기 버튼 클릭 이벤트
    public System.Action OnExitRequested;

    // 뷰
    private ExitView _view;

    public ExitPresenter(ExitView view)
    {
        _view = view;
    }
}