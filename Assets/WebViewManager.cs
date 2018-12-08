using System;
using UnityEngine;
using UnityEngine.UI;

public class WebViewManager : MonoBehaviour
{
	public InputField inputField;
	public Button buttonEnter;
	public Button buttonClose;
	public Button ButtonCSCallJS;
	private UniWebView uniWebView;

	private void Awake()
	{
		buttonEnter.onClick.AddListener(OpenUrl);
		buttonClose.onClick.AddListener(CloseUrl);
		ButtonCSCallJS.onClick.AddListener(UnityCallWebView);
	}



	public void OpenUrl()
	{
		if (uniWebView != null)
		{
			uniWebView.CleanCache();
		}
		if (inputField.text == null)
		{
			return;
		}


		string path = "http://120.78.196.100:8080/Test/Test.html";

		uniWebView = UniWebViewHelper.CreateUniWebView(gameObject, path, 300, 0, 50, 0);


		uniWebView.OnLoadComplete += OnLoadComplete;
		uniWebView.OnReceivedMessage += OnReceivedMessage;
		uniWebView.OnEvalJavaScriptFinished += OnEvalJavaScriptFinished;
		uniWebView.Load();
	}
	private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
	{
		if (success)
		{
			//  显示 加载完成的界面  
			webView.Show();
		}
		else
		{
			//   输出 错误码  
			Debug.LogError("Something wrong in webview loading: " + errorMessage);
		}
	}
	public void CloseUrl()
	{
		uniWebView.Hide();
		//反注册
		uniWebView.OnLoadComplete -= OnLoadComplete;
		uniWebView.OnReceivedMessage -= OnReceivedMessage;
		uniWebView.OnEvalJavaScriptFinished -= OnEvalJavaScriptFinished;
		Destroy(uniWebView);
	}
	/// <summary>
	/// UniWebView Call Unity
	/// </summary>
	/// <param name="webView"></param>
	/// <param name="message"></param>
	private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
	{
		Debug.Log(">>>OnReceivedMessage");
		Debug.Log(message.rawMessage);
	}
	/// <summary>
	/// Unity Call WebView
	/// </summary>
	/// <param name="javaScript">JavaScript代码</param>
	private void UnityCallWebView(string javaScript)
	{
		uniWebView.EvaluatingJavaScript(javaScript);
	}
	/// <summary>
	/// Unity Call WebView回调
	/// </summary>
	/// <param name="webView"></param>
	/// <param name="result"></param>
	private void OnEvalJavaScriptFinished(UniWebView webView, string result)
	{
		Debug.Log(">>>OnEvalJavaScriptFinished");
	}

	/// <summary>
	/// Unity Call WebView
	/// </summary>
	private void UnityCallWebView()
	{
		UnityCallWebView("pupop();");
	}
}
