using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
	//UI에서 EventSystem이 없으면 아예 반응하지 않음. 
	private EventSystem eventSystem;

	private Canvas popUpCanvas; //팝업 UI는 모두 popUpCanvas의 하위 자식으로 만들 예정
								//Canvas 벗어난 UI는 가끔 설정하지 않은 위치에 나올 수 있기 때문에 모든 ui는 Canvas 하위메뉴로 들어가 있어야하기 때문임

	private Canvas windowCanvas; //윈도우 UI가 팝업 UI를 가리면 안되지만, 게임 씬보다는 앞에 있어야 해
								 //  -> 우선순위 조절이 필요  -> 윈도우 캔버스를 만듦

	private Canvas inGameCanvas;

	private Stack<PopUpUI> popUpStack; //ui는 statck 형식임!! -> 열린 순서에 맞게 차례대로 닫혀야 함
	private List<WindowUI> windowUI; 

	private void Awake()
	{
		//EventSystem이 무조건 필요하니까 리소시스화 해서 처음 시작과 동시에 이벤트 시스템을 추가하도록 함
		eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
		eventSystem.transform.parent = transform; //UIManager로 넣어주면 자연스럽게 DontDestory 안으로 들어가니 모든 씬에서 존재하는 효과를 볼 수 있음

		inGameCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		inGameCanvas.gameObject.name = "InGameCanvas";
		inGameCanvas.sortingOrder = 30;

		popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		popUpCanvas.gameObject.name = "PopUpCanvas";
		popUpCanvas.sortingOrder = 20; //모든 게임 씬 위에 나타나도록 order를 좀 높게 줌

		popUpStack = new Stack<PopUpUI>();


		windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		windowCanvas.gameObject.name = "WindowCanvas";
		windowCanvas.sortingOrder = 10;

		//gameSceneCanvas.sortingOrder = 1;

		
	}

	public T ShowPopUpUI<T>(T popUpUi) where T : PopUpUI
	{
		//이전에 열린 PopUp UI는 안 보이도록 설정해서 최상위에 있는 팝업 화면이 잘 보이도록 함
		// => 새로운 팝업 화면을 열기 전에 최상단의 화면(지금 활성화 되어 있는 팝업 화면)을 비활성화 처리함 
		if (popUpStack.Count > 0)
		{
			PopUpUI prevUI = popUpStack.Peek(); //가장 최상위 ui를 가져옴
			prevUI.gameObject.SetActive(false); //비활성화함
		}

		T ui = GameManager.Pool.GetUI(popUpUi); //ui대여

		ui.transform.SetParent(popUpCanvas.transform, worldPositionStays : false);
		//보통 계층구조가 되면 하위 오브젝트는 상대 위치로 설정이 됨. -> 상위 오브젝트에서 (N, M, Z)만큼 떨어진 위치. => worldPositionStays = true (기본값)
		//worldPositionStays = false가되면 그냥 내가 지정한 위치가 됨.
		//보통 UI는 그냥 지정한 위치에 그대로 나와야하니 worldPositionStays를 false로 설정함

		popUpStack.Push(ui);

		Time.timeScale = 0; //timeScale을 0으로 만듦 = 시간을 멈춤 => 일시정지 효과

		return ui;
	}

	public T ShowPopUpUI<T>(string path) where T : PopUpUI
	{
		T ui = GameManager.Resource.Load<T>(path);
		return ShowPopUpUI(ui);
	}

	public void ClosePopUpUI()
	{
		PopUpUI ui =  popUpStack.Pop();
		GameManager.Pool.Release(ui.gameObject); //ui반납

		if(popUpStack.Count > 0) //아직 팝업 열려있는 팝업 화면이 남아있닫면?
		{
			//가장 최상위 ui를 가져와 활성화 시켜줌 -> 전에 열렸던 창은 스택에서 꺼낸 후 가장 최상단 화면을 활성화함 
			PopUpUI prevUI = popUpStack.Peek(); 
			prevUI.gameObject.SetActive(true); 
		}

		if(popUpStack.Count == 0) //현재 열린 창이 하나도 없다면?
		{
			Time.timeScale = 1; //시간을 다시 흐르게 함 -> 일시정지 해제
		}
	}

	public T ShowWindowUI<T>(T windowUI) where T : WindowUI
	{
		T ui = GameManager.Pool.GetUI(windowUI);
		ui.transform.SetParent(windowCanvas.transform, false);

		//윈도우 창 닫았다가 다시 열면 닫았던 위치에서 다시 열리는 이유는 pooling으로 대여하는 방식으로 사용해서 그런 거임. 
		//닫은 위치를 기억하고 있기 때문임. 
		//늘 같은 곳에서 열고 싶다면? 화면을 열고 위치를 다시 조정해주면 돼
		ui.transform.position = new Vector3(521, 163, 0);

		return ui;
	}

	public T ShowWindowUI<T>(string path) where T : WindowUI
	{
		return ShowWindowUI(GameManager.Resource.Load<T>(path));
	}

	//닫을 창을 받아옴
	public void CloseWindowUI<T>(T windowUI) where T : WindowUI
	{
		GameManager.Pool.Release(windowUI.gameObject);
	}

	public void SelectWindowUI<T>(T windowUI) where T : WindowUI
	{
		windowUI.transform.SetAsLastSibling(); //같은 레벨의 오브젝트 중 SetAsLastSibling한 윈도우 화면을 가장 마지막으로 설정함 = 최상위로 올라옴
	}

	public T ShowInGameUI<T>(T inGameUI) where T : InGameUI
	{
		T ui = GameManager.Pool.GetUI(inGameUI);
		ui.transform.SetParent(inGameCanvas.transform, false);

		return ui;
	}

	public T ShowInGameUI<T>(string path) where T : InGameUI
	{
		return ShowInGameUI(GameManager.Resource.Load<T>(path));
	}

	public void CloseInGameUI<T>(T inGameUI) where T : InGameUI
	{
		GameManager.Pool.ReleaseUI(inGameUI.gameObject);
	}

	public void CloseAllInGameUI<T>() where T : InGameUI
	{
		var ui = inGameCanvas.GetComponentsInChildren<T>();

		foreach(var item in ui)
		{
			CloseInGameUI(item);
		}

	}
}
