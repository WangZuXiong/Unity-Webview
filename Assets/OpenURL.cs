#region 模块信息
// **********************************************************************
// Copyright (C) 2018 Blazors
// Please contact me if you have any questions
// File Name:             OpenURL
// Author:                romantic123fly
// WeChat||QQ:            at853394528 || 853394528 
// **********************************************************************
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenURL : MonoBehaviour
{
	public InputField _url;
	public Button _enterBtn;
	public Button _backBtn;

	UniWebView _view;
	private void Awake()
	{
		_enterBtn.onClick.AddListener(OpenUrl);
		_backBtn.onClick.AddListener(CloseUrl);
		_backBtn.gameObject.SetActive(false);
	}

	public void OpenUrl()
	{
		if (_view != null)
		{
			_view.CleanCache();
		}
		if (_url.text == null)
		{
			return;
		}
		_view = UniWebViewHelper.CreateUniWebView(gameObject, "https://" + _url.text, 100, 0, 50, 0);
		_view.OnLoadComplete += View_OnLoadComplete;
		_view.Load();
	}

	private void View_OnLoadComplete(UniWebView webView, bool success, string errorMessage)
	{
		if (success)
		{
			//  显示 加载完成的界面  
			webView.Show();
			_backBtn.gameObject.SetActive(true);
		}
		else
		{
			//   输出 错误码  
			Debug.LogError("Something wrong in webview loading: " + errorMessage);
		}
	}
	public void CloseUrl()
	{
		_view.Hide();
		_view.OnLoadComplete -= View_OnLoadComplete;
		Destroy(_view);
	}
}
