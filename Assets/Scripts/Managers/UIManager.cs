using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
	//UI���� EventSystem�� ������ �ƿ� �������� ����. 
	private EventSystem eventSystem;

	private Canvas popUpCanvas; //�˾� UI�� ��� popUpCanvas�� ���� �ڽ����� ���� ����
								//Canvas ��� UI�� ���� �������� ���� ��ġ�� ���� �� �ֱ� ������ ��� ui�� Canvas �����޴��� �� �־���ϱ� ������

	private Canvas windowCanvas; //������ UI�� �˾� UI�� ������ �ȵ�����, ���� �����ٴ� �տ� �־�� ��
								 //  -> �켱���� ������ �ʿ�  -> ������ ĵ������ ����

	private Canvas inGameCanvas;

	private Stack<PopUpUI> popUpStack; //ui�� statck ������!! -> ���� ������ �°� ���ʴ�� ������ ��
	private List<WindowUI> windowUI; 

	private void Awake()
	{
		//EventSystem�� ������ �ʿ��ϴϱ� ���ҽý�ȭ �ؼ� ó�� ���۰� ���ÿ� �̺�Ʈ �ý����� �߰��ϵ��� ��
		eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
		eventSystem.transform.parent = transform; //UIManager�� �־��ָ� �ڿ������� DontDestory ������ ���� ��� ������ �����ϴ� ȿ���� �� �� ����

		inGameCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		inGameCanvas.gameObject.name = "InGameCanvas";
		inGameCanvas.sortingOrder = 30;

		popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		popUpCanvas.gameObject.name = "PopUpCanvas";
		popUpCanvas.sortingOrder = 20; //��� ���� �� ���� ��Ÿ������ order�� �� ���� ��

		popUpStack = new Stack<PopUpUI>();


		windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
		windowCanvas.gameObject.name = "WindowCanvas";
		windowCanvas.sortingOrder = 10;

		//gameSceneCanvas.sortingOrder = 1;

		
	}

	public T ShowPopUpUI<T>(T popUpUi) where T : PopUpUI
	{
		//������ ���� PopUp UI�� �� ���̵��� �����ؼ� �ֻ����� �ִ� �˾� ȭ���� �� ���̵��� ��
		// => ���ο� �˾� ȭ���� ���� ���� �ֻ���� ȭ��(���� Ȱ��ȭ �Ǿ� �ִ� �˾� ȭ��)�� ��Ȱ��ȭ ó���� 
		if (popUpStack.Count > 0)
		{
			PopUpUI prevUI = popUpStack.Peek(); //���� �ֻ��� ui�� ������
			prevUI.gameObject.SetActive(false); //��Ȱ��ȭ��
		}

		T ui = GameManager.Pool.GetUI(popUpUi); //ui�뿩

		ui.transform.SetParent(popUpCanvas.transform, worldPositionStays : false);
		//���� ���������� �Ǹ� ���� ������Ʈ�� ��� ��ġ�� ������ ��. -> ���� ������Ʈ���� (N, M, Z)��ŭ ������ ��ġ. => worldPositionStays = true (�⺻��)
		//worldPositionStays = false���Ǹ� �׳� ���� ������ ��ġ�� ��.
		//���� UI�� �׳� ������ ��ġ�� �״�� ���;��ϴ� worldPositionStays�� false�� ������

		popUpStack.Push(ui);

		Time.timeScale = 0; //timeScale�� 0���� ���� = �ð��� ���� => �Ͻ����� ȿ��

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
		GameManager.Pool.Release(ui.gameObject); //ui�ݳ�

		if(popUpStack.Count > 0) //���� �˾� �����ִ� �˾� ȭ���� �����ִݸ�?
		{
			//���� �ֻ��� ui�� ������ Ȱ��ȭ ������ -> ���� ���ȴ� â�� ���ÿ��� ���� �� ���� �ֻ�� ȭ���� Ȱ��ȭ�� 
			PopUpUI prevUI = popUpStack.Peek(); 
			prevUI.gameObject.SetActive(true); 
		}

		if(popUpStack.Count == 0) //���� ���� â�� �ϳ��� ���ٸ�?
		{
			Time.timeScale = 1; //�ð��� �ٽ� �帣�� �� -> �Ͻ����� ����
		}
	}

	public T ShowWindowUI<T>(T windowUI) where T : WindowUI
	{
		T ui = GameManager.Pool.GetUI(windowUI);
		ui.transform.SetParent(windowCanvas.transform, false);

		//������ â �ݾҴٰ� �ٽ� ���� �ݾҴ� ��ġ���� �ٽ� ������ ������ pooling���� �뿩�ϴ� ������� ����ؼ� �׷� ����. 
		//���� ��ġ�� ����ϰ� �ֱ� ������. 
		//�� ���� ������ ���� �ʹٸ�? ȭ���� ���� ��ġ�� �ٽ� �������ָ� ��
		ui.transform.position = new Vector3(521, 163, 0);

		return ui;
	}

	public T ShowWindowUI<T>(string path) where T : WindowUI
	{
		return ShowWindowUI(GameManager.Resource.Load<T>(path));
	}

	//���� â�� �޾ƿ�
	public void CloseWindowUI<T>(T windowUI) where T : WindowUI
	{
		GameManager.Pool.Release(windowUI.gameObject);
	}

	public void SelectWindowUI<T>(T windowUI) where T : WindowUI
	{
		windowUI.transform.SetAsLastSibling(); //���� ������ ������Ʈ �� SetAsLastSibling�� ������ ȭ���� ���� ���������� ������ = �ֻ����� �ö��
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
