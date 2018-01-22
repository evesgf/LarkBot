// GUI Animator for Unity UI
// Version: 1.1.1
// Compatilble: Unity 5.4.0 or higher, more info in Readme.txt file.
//
// Developer:				Gold Experience Team (http://www.ge-team.com)
// Unity Asset Store:		https://www.assetstore.unity3d.com/en/#!/content/28709
// Gold Experience Store:	https://www.ge-team.com/en/products/gui-animator-for-unity-ui/
// Support:					geteamdev@gmail.com
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com.

#region Namespaces

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
// DOTween Documentation: http://dotween.demigiant.com/documentation.php

// HOTween: https://www.assetstore.unity3d.com/#/content/3311
// HOTween Documentation:  http://hotween.demigiant.com/documentation.html

// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html

// iTween: https://www.assetstore.unity3d.com/#/content/84 
// iTween Documentation: http://itween.pixelplacement.com/documentation.php

#if DOTWEEN
	using DG.Tweening;
#elif HOTWEEN
	using Holoville.HOTween;
#elif ITWEEN
#elif LEANTWEEN
#endif

#endregion // Namespaces

// ######################################################################
// GAui class
// Controls position, rotation, scale and fade animations using DOTween/HOTween/iTween/LeanTween tweeners.
// To switch between tweeners or add your own favorite, please read "!How to.txt" and "Readme.txt".
// ######################################################################

public class GAui : GUIAnim
{

	#region Variables

	// Idle animations

	// If you have found error somewhere in these following lines, check out a section names "How to switch tweeners between DOTween, HOTween, iTween and LeanTween." in read me file.
#if DOTWEEN
	Tweener m_DOTweenRotationLoop = null;
			Tweener m_DOTweenScaleLoop = null;
#elif HOTWEEN
	Tweener m_HOTweenRotationLoop = null;
			Tweener m_HOTweenScaleLoop = null;
#elif ITWEEN
#elif LEANTWEEN
	LTDescr m_LeanTweenRotateLoop = null;
	LTDescr m_LeanTweenScaleLoop = null;
#else
#endif

	// If you have found error somewhere in these following lines, check out a section names "How to switch tweeners between DOTween, HOTween, iTween and LeanTween." in read me file.
#if DOTWEEN
			Tweener m_DOTweenFadeLoop = null;
#elif HOTWEEN
			Tweener m_HOTweenFadeLoop = null;
#elif ITWEEN
#elif LEANTWEEN
	LTDescr m_LeanTweenFadeLoop = null;
#else
#endif

	#endregion // Variables

	// ########################################
	// MonoBehaviour Functions
	// ########################################

	#region MonoBehaviour

	// Awake is called when the script instance is being loaded.
	public override void Anim_Awake()
	{
		// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
		// DOTween Documentation: http://dotween.demigiant.com/documentation.php

		// HOTween: https://www.assetstore.unity3d.com/#/content/3311
		// HOTween Documentation:  http://hotween.demigiant.com/documentation.html

		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html

		// iTween: https://www.assetstore.unity3d.com/#/content/84 
		// iTween Documentation: http://itween.pixelplacement.com/documentation.php

#if DOTWEEN
#elif HOTWEEN
#elif ITWEEN
#elif LEANTWEEN
		// LEANTWEEN INITIALIZATION
		LeanTween.init(3200); // This line is optional. Here you can specify the maximum number of tweens you will use (the default is 400).  This must be called before any use of LeanTween is made for it to be effective.
#else
		base.Anim_Awake();
#endif

	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	public override void Anim_Start()
	{

		// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
		// DOTween Documentation: http://dotween.demigiant.com/documentation.php

		// HOTween: https://www.assetstore.unity3d.com/#/content/3311
		// HOTween Documentation:  http://hotween.demigiant.com/documentation.html

		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html

		// iTween: https://www.assetstore.unity3d.com/#/content/84 
		// iTween Documentation: http://itween.pixelplacement.com/documentation.php

#if DOTWEEN
			// DOTWEEN INITIALIZATION
			// Initialize DOTween (needs to be done only once).
			// If you don't initialize DOTween yourself,
			// it will be automatically initialized with default values.
			// DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
#elif HOTWEEN
			// HOTWEEN INITIALIZATION
			// Must be done only once, before the creation of your first tween
			// (you can skip this if you want, and HOTween will be initialized automatically
			// when you create your first tween - using default values)
			HOTween.Init(true, true, true);
#elif ITWEEN
#elif LEANTWEEN
#else
#endif

	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// Play In-Animation functions
	// ########################################

	#region In-Animations

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_In_Move()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_MoveIn.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_MoveIn.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_MoveVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			//DOTween.To(()=>m_MoveVariable, x=> m_MoveVariable = x, 1.0f, 1).set;
			DOTween.To(x=> m_MoveVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)	// Sets a delayed startup for the tween. 
				.SetEase(DOTweenEaseType(m_MoveIn.EaseType))								// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
				.OnUpdate(AnimIn_MoveUpdate)												// Sets a callback that will be fired every time the tween updates.
				.OnComplete(AnimIn_MoveComplete)											// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
				.SetUpdate(UpdateType.Normal, true);										// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_MoveVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_MoveVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_MoveIn.EaseType))
					.OnUpdate(AnimIn_MoveUpdate)
					.OnComplete(AnimIn_MoveComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
				
			m_MoveIn.Began = true;
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_MoveIn.EaseType),
														"onupdate", "AnimIn_MoveUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimIn_MoveComplete"));
				
			m_MoveIn.Began = true;
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimIn_MoveUpdateByValue, 0.0f, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_MoveIn.EaseType))
				.setOnComplete(AnimIn_MoveComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_In_Move();
#endif
	}

	// -- This function will be called when In-Animation move is done --
	public override void Anim_In_MoveComplete()
	{

	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_In_Rotate()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_RotationIn.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_RotationIn.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_RotationVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			DOTween.To(x=> m_RotationVariable = x, 0.0f, 1.0f, time)
					.SetDelay(delay)	// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_RotationIn.EaseType))				// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.OnUpdate(AnimIn_RotationUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(AnimIn_RotationComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);							// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_RotationVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_RotationVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_RotationIn.EaseType))
					.OnUpdate(AnimIn_RotationUpdate)
					.OnComplete(AnimIn_RotationComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
				
			m_RotationIn.Began = true;
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_RotationIn.EaseType),
														"onupdate", "AnimIn_RotationUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimIn_RotationComplete"));
				
			m_RotationIn.Began = true;
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimIn_RotationUpdateByValue, 0, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_RotationIn.EaseType))
				.setOnComplete(AnimIn_RotationComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_In_Rotate();
#endif
	}

	// -- This function will be called when In-Animation rotate is done --
	public override void Anim_In_RotateComplete()
	{

	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_In_Scale()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_ScaleIn.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_ScaleIn.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_ScaleVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			DOTween.To(x=> m_ScaleVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)	// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_ScaleIn.EaseType))				// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.OnUpdate(AnimIn_ScaleUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(AnimIn_ScaleComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);						// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_ScaleVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_ScaleVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_ScaleIn.EaseType))
					.OnUpdate(AnimIn_ScaleUpdate)
					.OnComplete(AnimIn_ScaleComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
				
			m_ScaleIn.Began = true;
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_ScaleIn.EaseType),
														"onupdate", "AnimIn_ScaleUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimIn_ScaleComplete"));
				
			m_ScaleIn.Began = true;
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimIn_ScaleUpdateByValue, 0, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_ScaleIn.EaseType))
				.setOnComplete(AnimIn_ScaleComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_In_Scale();
#endif
	}

	// -- This function will be called when In-Animation scale is done --
	public override void Anim_In_ScaleComplete()
	{

	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_In_Fade()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_FadeIn.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_FadeIn.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_FadeVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			DOTween.To(x=> m_FadeVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)	// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_FadeIn.EaseType))				// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.OnUpdate(AnimIn_FadeUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(AnimIn_FadeComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);						// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_FadeVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_FadeVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_FadeIn.EaseType))
					.OnUpdate(AnimIn_FadeUpdate)
					.OnComplete(AnimIn_FadeComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_FadeIn.EaseType),
														"onupdate", "AnimIn_FadeUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimIn_FadeComplete"));
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimIn_FadeUpdateByValue, 0, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_FadeIn.EaseType))
				.setOnComplete(AnimIn_FadeComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_In_Fade();
#endif
	}

	// -- This function will be called when In-Animation fade is done --
	public override void Anim_In_FadeComplete()
	{

	}

	// -- This function will be called when all In-Animations are done --
	public override void Anim_In_AllComplete()
	{

	}

	#endregion // In-Animations

	// ########################################
	// Play Out-Animation functions
	// ########################################

	#region Out-Animations

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_Out_Move()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_MoveOut.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_MoveOut.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_MoveVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			DOTween.To(x=> m_MoveVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)	// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_MoveOut.EaseType))				// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.OnUpdate(AnimOut_MoveUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(AnimOut_MoveComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);						// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_MoveVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_MoveVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_MoveOut.EaseType))
					.OnUpdate(AnimOut_MoveUpdate)
					.OnComplete(AnimOut_MoveComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
				
			m_MoveOut.Began = true;
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_MoveOut.EaseType),
														"onupdate", "AnimOut_MoveUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimOut_MoveComplete"));
				
			m_MoveOut.Began = true;
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimOut_MoveUpdateByValue, 0.0f, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_MoveOut.EaseType))
				.setOnComplete(AnimOut_MoveComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_Out_Move();
#endif
	}

	// -- This function will be called when Out-Animation move is done --
	public override void Anim_Out_MoveComplete()
	{

	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_Out_Rotate()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_RotationOut.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_RotationOut.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_RotationVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			DOTween.To(x=> m_RotationVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)	// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_RotationOut.EaseType))				// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.OnUpdate(AnimOut_RotationUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(AnimOut_RotationComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);						// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 

#elif HOTWEEN
			m_RotationVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_RotationVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_RotationOut.EaseType))
					.OnUpdate(AnimOut_RotationUpdate)
					.OnComplete(AnimOut_RotationComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
				
			m_RotationOut.Began = true;
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_RotationOut.EaseType),
														"onupdate", "AnimOut_RotationUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimOut_RotationComplete"));
				
			m_RotationOut.Began = true;
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimOut_RotationUpdateByValue, 0.0f, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_RotationOut.EaseType))
				.setOnComplete(AnimOut_RotationComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_Out_Rotate();
#endif

	}

	// -- This function will be called when Out-Animation rotate is done --
	public override void Anim_Out_RotateComplete()
	{

	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_Out_Scale()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_ScaleOut.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_ScaleOut.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_ScaleVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			DOTween.To(x=> m_ScaleVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)	// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_ScaleOut.EaseType))				// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.OnUpdate(AnimOut_ScaleUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(AnimOut_ScaleComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);						// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_ScaleVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_ScaleVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_ScaleOut.EaseType))
					.OnUpdate(AnimOut_ScaleUpdate)
					.OnComplete(AnimOut_ScaleComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
				
			m_ScaleOut.Began = true;
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_ScaleOut.EaseType),
														"onupdate", "AnimOut_ScaleUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimOut_ScaleComplete"));
				
			m_ScaleOut.Began = true;
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimOut_ScaleUpdateByValue, 0.0f, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_ScaleOut.EaseType))
				.setOnComplete(AnimOut_ScaleComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_Out_Scale();
#endif

	}

	// -- This function will be called when Out-Animation scale is done --
	public override void Anim_Out_ScaleComplete()
	{

	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	public override void Anim_Out_Fade()
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_FadeOut.Time/(float)GSui.Instance.m_GUISpeed;
		float delay = (float)m_FadeOut.Delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_FadeVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			DOTween.To(x=> m_FadeVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)	// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_FadeOut.EaseType))				// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.OnUpdate(AnimOut_FadeUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(AnimOut_FadeComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);						// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_FadeVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			HOTween.To(this, time, new TweenParms()
					.Prop("m_FadeVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_FadeOut.EaseType))
					.OnUpdate(AnimOut_FadeUpdate)
					.OnComplete(AnimOut_FadeComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
						);
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_FadeOut.EaseType),
														"onupdate", "AnimOut_FadeUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "AnimOut_FadeComplete"));
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		LeanTween.value(this.gameObject, AnimOut_FadeUpdateByValue, 0, 1.0f, time)
			.setDelay(delay)
				.setEase(LeanTweenEaseType(m_FadeOut.EaseType))
				.setOnComplete(AnimOut_FadeComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_Out_Fade();
#endif

	}

	// -- This function will be called when Out-Animation fade is done --
	public override void Anim_Out_FadeComplete()
	{

	}

	// -- This function will be called when all Out-Animations are done --
	public override void Anim_Out_AllComplete()
	{

	}

	#endregion // Out-Animations

	// ########################################
	// Play Idle-Animation functions
	// ########################################

	#region Idle-Animations


	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_RotateLoop(float), Anim_Idle_StopRotateLoop(), Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	// -- Begin Rotate-loop --
	public override void Anim_Idle_RotationLoop(float delay)
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_RotationLoop.Time / (float)GSui.Instance.m_GUISpeed;
		delay = (float)delay / (float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
		m_RotationVariable = 0.0f;
		if (m_RotationLoop.m_RotationType == eLoopRotationType.OneDirection)
		{
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			m_DOTweenRotationLoop = DOTween.To(x => m_RotationVariable = x, 0.0f, 1.0f, time)
					.SetDelay(delay)        // Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_RotationLoop.EaseType))      // Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.SetLoops(-1, LoopType.Restart)                         // Sets the looping options (Restart, Yoyo, Incremental) for the tween.
					.OnUpdate(RotationLoopUpdate)                           // Sets a callback that will be fired every time the tween updates.
					.OnComplete(RotationLoopComplete)                       // Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);                    // Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
		}
		else if (m_RotationLoop.m_RotationType == eLoopRotationType.PingPong)
		{
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			m_DOTweenRotationLoop = DOTween.To(x => m_RotationVariable = x, 0.0f, 1.0f, time)
					.SetDelay(delay)        // Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_RotationLoop.EaseType))      // Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.SetLoops(-1, LoopType.Yoyo)                            // Sets the looping options (Restart, Yoyo, Incremental) for the tween.
					.OnUpdate(RotationLoopUpdate)                           // Sets a callback that will be fired every time the tween updates.
					.OnComplete(RotationLoopComplete)                       // Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);                    // Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
		}
#elif HOTWEEN
		m_RotationVariable = 0.0f;
		if (m_RotationLoop.m_RotationType == eLoopRotationType.OneDirection)
		{
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			m_HOTweenRotationLoop = HOTween.To(this, time, new TweenParms()
					.Prop("m_RotationVariable", 1.0f, false)
											.Delay(delay)
					.Ease(HOTweenEaseType(m_RotationLoop.EaseType))
					.Loops(-1, LoopType.Restart)
					.OnUpdate(RotationLoopUpdate)
					.OnComplete(RotationLoopUpdate)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate));
		}
		else if (m_RotationLoop.m_RotationType == eLoopRotationType.PingPong)
		{
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			m_HOTweenRotationLoop = HOTween.To(this, time, new TweenParms()
					.Prop("m_RotationVariable", 1.0f, false)
											.Delay(delay)
					.Ease(HOTweenEaseType(m_RotationLoop.EaseType))
					.Loops(-1, LoopType.Yoyo)
					.OnUpdate(RotationLoopUpdate)
					.OnComplete(RotationLoopUpdate)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate));
		}

#elif ITWEEN
		if (m_RotationLoop.m_RotationType == eLoopRotationType.OneDirection)
		{
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_RotationLoop.EaseType),
														"looptype", "loop",
														"onupdate", "RotationLoopUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "RotateLoopComplete"
														));
		}
		else if (m_RotationLoop.m_RotationType == eLoopRotationType.PingPong)
		{
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_RotationLoop.EaseType),
														"looptype","pingpong",
														"onupdate", "RotationLoopUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "RotateLoopComplete"
														));
		}

#elif LEANTWEEN
		if (m_RotationLoop.m_RotationType == eLoopRotationType.OneDirection)
		{
			// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
			// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
			m_LeanTweenRotateLoop = LeanTween.value(this.gameObject, RotationLoopUpdateByValue, 0.0f, 1.0f, time)
						.setDelay(delay)
						.setEase(LeanTweenEaseType(m_RotationLoop.EaseType))
						.setLoopClamp()
						.setOnComplete(RotationLoopComplete)
						.setUseEstimatedTime(true);
		}
		else if (m_RotationLoop.m_RotationType == eLoopRotationType.PingPong)
		{
			// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
			// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
			m_LeanTweenRotateLoop = LeanTween.value(this.gameObject, RotationLoopUpdateByValue, 0.0f, 1.0f, time)
						.setDelay(delay)
						.setEase(LeanTweenEaseType(m_RotationLoop.EaseType))
						.setLoopPingPong()
						.setOnComplete(RotationLoopComplete)
						.setUseEstimatedTime(true);
		}

#else
		base.Anim_Idle_RotationLoop(delay);
#endif
	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_RotateLoop(float), Anim_Idle_StopRotateLoop(), Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	// -- Stop Rotate-loop animation --
	public override void Anim_Idle_StopRotationLoop()
	{
#if DOTWEEN
		if (m_DOTweenRotationLoop != null)
		{
			m_DOTweenRotationLoop.Kill();
			m_DOTweenRotationLoop = null;
		}
#elif HOTWEEN
		if (m_HOTweenRotationLoop != null)
		{
			m_HOTweenRotationLoop.Kill();
			m_HOTweenRotationLoop = null;
		}
#elif ITWEEN
#elif LEANTWEEN
		if (m_LeanTweenRotateLoop != null)
		{
			m_LeanTweenRotateLoop.cancel(this.gameObject);
			m_LeanTweenRotateLoop = null;
		}
#else
		base.Anim_Idle_StopRotationLoop();
#endif
	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	// -- Begin Scale-loop --
	public override void Anim_Idle_ScaleLoop(float delay)
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_ScaleLoop.Time/(float)GSui.Instance.m_GUISpeed;
		delay = (float)delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_ScaleVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			m_DOTweenScaleLoop = DOTween.To(x=> m_ScaleVariable = x, 0.0f, 1.0f, time)
					.SetDelay(delay)		// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_ScaleLoop.EaseType))			// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.SetLoops(-1, LoopType.Yoyo)							// Sets the looping options (Restart, Yoyo, Incremental) for the tween.
					.OnUpdate(ScaleLoopUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(ScaleLoopComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);					// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_ScaleVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			m_HOTweenScaleLoop = HOTween.To(this, time, new TweenParms()
					.Prop("m_ScaleVariable", 1.0f, false)
											.Delay(delay)
					.Ease(HOTweenEaseType(m_ScaleLoop.EaseType))
					.Loops(-1,LoopType.Yoyo)
					.OnUpdate(ScaleLoopUpdate)
					.OnComplete(ScaleLoopComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
		);

#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_ScaleLoop.EaseType),
														"looptype","pingpong",
														"onupdate", "ScaleLoopUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "ScaleLoopComplete"
														));

#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		m_LeanTweenScaleLoop = LeanTween.value(this.gameObject, ScaleLoopUpdateByValue, 0.0f, 1.0f, time)
					.setDelay(delay)
					.setEase(LeanTweenEaseType(m_ScaleLoop.EaseType))
					.setLoopPingPong()
					.setOnComplete(ScaleLoopComplete)
					.setUseEstimatedTime(true);

#else
		base.Anim_Idle_ScaleLoop(delay);
#endif
	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	// -- Stop Scale-loop animation --
	public override void Anim_Idle_StopScaleLoop()
	{
#if DOTWEEN
			if(m_DOTweenScaleLoop!=null)
			{
				m_DOTweenScaleLoop.Kill();
				m_DOTweenScaleLoop = null;
			}
#elif HOTWEEN
			if(m_HOTweenScaleLoop!=null)
			{
				m_HOTweenScaleLoop.Kill();
				m_HOTweenScaleLoop = null;
			}
#elif ITWEEN
#elif LEANTWEEN
		if (m_LeanTweenScaleLoop != null)
		{
			m_LeanTweenScaleLoop.cancel(this.gameObject);
			m_LeanTweenScaleLoop = null;
		}
#else
		base.Anim_Idle_StopScaleLoop();
#endif
	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//   You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	// -- Begin Fade-loop-- 
	public override void Anim_Idle_FadeLoop(float delay)
	{
#if DOTWEEN || HOTWEEN || ITWEEN || LEANTWEEN
		float time = (float)m_FadeLoop.Time/(float)GSui.Instance.m_GUISpeed;
		delay = (float)delay/(float)GSui.Instance.m_GUISpeed;
#endif
#if DOTWEEN
			m_FadeVariable = 0.0f;
			// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
			// DOTween Documentation: http://dotween.demigiant.com/documentation.php
			m_DOTweenFadeLoop = DOTween.To(x=> m_FadeVariable = x, 0.0f, 1.0f, time)
				.SetDelay(delay)			// Sets a delayed startup for the tween. 
					.SetEase(DOTweenEaseType(m_FadeLoop.EaseType))			// Sets the ease of the tween. More info, check out http://easings.net or http://www.robertpenner.com/easing/easing_demo.html
					.SetLoops(-1, LoopType.Yoyo)							// Sets the looping options (Restart, Yoyo, Incremental) for the tween.
					.OnUpdate(FadeLoopUpdate)								// Sets a callback that will be fired every time the tween updates.
					.OnComplete(FadeLoopComplete)							// Sets a callback that will be fired the moment the tween reaches completion, all loops included.
					.SetUpdate(UpdateType.Normal, true);					// Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. 
#elif HOTWEEN
			m_FadeVariable = 0.0f;
			// HOTween: https://www.assetstore.unity3d.com/#/content/3311
			// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
			m_HOTweenFadeLoop = HOTween.To(this, time, new TweenParms()
					.Prop("m_FadeVariable", 1.0f, false)
					.Delay(delay)
					.Ease(HOTweenEaseType(m_FadeLoop.EaseType))
					.Loops(-1,LoopType.Yoyo)
					.OnUpdate(FadeLoopUpdate)
					.OnComplete(FadeLoopComplete)
					.UpdateType(UpdateType.TimeScaleIndependentUpdate)
		);
#elif ITWEEN
			// iTween: https://www.assetstore.unity3d.com/#/content/84 
			// iTween Documentation: http://itween.pixelplacement.com/documentation.php
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0.0f,
														"to", 1.0f,
														"time", time,
														"ignoretimescale", true,
														"delay", delay,
														"easeType", iTweenEaseType(m_FadeLoop.EaseType),
														"looptype","pingpong",
														"onupdate", "FadeLoopUpdateByValue",
														"onupdatetarget", this.gameObject,
														"oncomplete", "FadeLoopComplete"
														));
#elif LEANTWEEN
		// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
		// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
		m_LeanTweenFadeLoop = LeanTween.value(this.gameObject, FadeLoopUpdateByValue, 0.0f, 1.0f, time)
				.setDelay(delay)
				.setEase(LeanTweenEaseType(m_FadeLoop.EaseType))
				.setLoopPingPong()
				.setOnComplete(FadeLoopComplete)
				.setUseEstimatedTime(true);
#else
		base.Anim_Idle_FadeLoop(delay);
#endif
	}

	// GUI Animator compatible to GETWEEN and 4 tweeners; DOTWEEN/HOTWEEN/ITWEEN/LEANTWEEN are provided.	
	// To switch between please read "How to switch tweeners?" in "!Howto.txt" file.
	//		You can add support to other tweeners by tweak these following functions; 
	//		Anim_In_Move(), Anim_In_Rotate(), Anim_In_Scale(), Anim_In_Fade(),
	//		Anim_Out_Move(), Anim_Out_Rotate(), Anim_Out_Scale(), Anim_Out_Fade(),
	//		Anim_Idle_ScaleLoop(float), Anim_Idle_StopScaleLoop(), Anim_Idle_FadeLoop(float), Anim_Idle_StopFadeLoop().
	//		And add ease type convert function into EaseType Converter region (the last region of this file).
	// -- Stop Fade-loop --
	public override void Anim_Idle_StopFadeLoop()
	{

		// Stop FadeLoop animation
#if DOTWEEN
			if(m_DOTweenFadeLoop!=null)
			{
				m_DOTweenFadeLoop.Kill();
				m_DOTweenFadeLoop = null;
			}
#elif HOTWEEN
			if(m_HOTweenFadeLoop!=null)
			{
				m_HOTweenFadeLoop.Kill();
				m_HOTweenFadeLoop = null;
			}
#elif ITWEEN
#elif LEANTWEEN
		if (m_LeanTweenFadeLoop != null)
		{
			m_LeanTweenFadeLoop.cancel(this.gameObject);
			m_LeanTweenFadeLoop = null;
		}
#else
		base.Anim_Idle_StopFadeLoop();
#endif
	}

	#endregion // Idle-Animations

	// ########################################
	// EaseType Converter for DOTween/HOTween/LeanTween/iTween
	// ########################################

	#region EaseType Converter

#if DOTWEEN
	// DOTween: https://www.assetstore.unity3d.com/en/#!/content/27676
	// DOTween Documentation: http://dotween.demigiant.com/documentation.php
	public Ease DOTweenEaseType(eEaseType easeType)
	{
		Ease result = Ease.Linear;
		switch (easeType)
		{
		case eEaseType.InQuad:			result = Ease.InQuad; break;
		case eEaseType.OutQuad:			result = Ease.OutQuad; break;
		case eEaseType.InOutQuad:		result = Ease.InOutQuad; break;
		case eEaseType.InCubic:			result = Ease.OutCubic; break;
		case eEaseType.OutCubic:		result = Ease.OutCubic; break;
		case eEaseType.InOutCubic:		result = Ease.InOutCubic; break;
		case eEaseType.InQuart:			result = Ease.InQuart; break;
		case eEaseType.OutQuart:		result = Ease.OutQuart; break;
		case eEaseType.InOutQuart:		result = Ease.InOutQuart; break;
		case eEaseType.InQuint:			result = Ease.InQuint; break;
		case eEaseType.OutQuint:		result = Ease.OutQuint; break;
		case eEaseType.InOutQuint:		result = Ease.InOutQuint; break;
		case eEaseType.InSine:			result = Ease.InSine; break;
		case eEaseType.OutSine:			result = Ease.OutSine; break;
		case eEaseType.InOutSine:		result = Ease.InOutSine; break;
		case eEaseType.InExpo:			result = Ease.InExpo; break;
		case eEaseType.OutExpo:			result = Ease.OutExpo; break;
		case eEaseType.InOutExpo:		result = Ease.InOutExpo; break;
		case eEaseType.InCirc:			result = Ease.InCirc; break;
		case eEaseType.OutCirc:			result = Ease.OutCirc; break;
		case eEaseType.InOutCirc:		result = Ease.InOutCirc; break;
		case eEaseType.linear:			result = Ease.Linear; break;
		case eEaseType.InBounce:		result = Ease.InBounce; break;
		case eEaseType.OutBounce:		result = Ease.OutBounce; break;
		case eEaseType.InOutBounce:		result = Ease.InOutBounce; break;
		case eEaseType.InBack:			result = Ease.InBack; break;
		case eEaseType.OutBack:			result = Ease.OutBack; break;
		case eEaseType.InOutBack:		result = Ease.InOutBack; break;
		case eEaseType.InElastic:		result = Ease.InElastic; break;
		case eEaseType.OutElastic:		result = Ease.OutElastic; break;
		case eEaseType.InOutElastic:	result = Ease.InOutElastic; break;
		default:						result = Ease.Linear; break;
		}
		return result;
	}
#elif HOTWEEN
	// HOTween: https://www.assetstore.unity3d.com/#/content/3311
	// HOTween Documentation:  http://hotween.demigiant.com/documentation.html
	public Holoville.HOTween.EaseType HOTweenEaseType(eEaseType easeType)
	{
		Holoville.HOTween.EaseType result = Holoville.HOTween.EaseType.Linear;
		switch (easeType)
		{
		case eEaseType.InQuad:			result = Holoville.HOTween.EaseType.EaseInQuad; break;
		case eEaseType.OutQuad:			result = Holoville.HOTween.EaseType.EaseOutQuad; break;
		case eEaseType.InOutQuad:		result = Holoville.HOTween.EaseType.EaseInOutQuad; break;
		case eEaseType.InCubic:			result = Holoville.HOTween.EaseType.EaseOutCubic; break;
		case eEaseType.OutCubic:		result = Holoville.HOTween.EaseType.EaseOutCubic; break;
		case eEaseType.InOutCubic:		result = Holoville.HOTween.EaseType.EaseInOutCubic; break;
		case eEaseType.InQuart:			result = Holoville.HOTween.EaseType.EaseInQuart; break;
		case eEaseType.OutQuart:		result = Holoville.HOTween.EaseType.EaseOutQuart; break;
		case eEaseType.InOutQuart:		result = Holoville.HOTween.EaseType.EaseInOutQuart; break;
		case eEaseType.InQuint:			result = Holoville.HOTween.EaseType.EaseInQuint; break;
		case eEaseType.OutQuint:		result = Holoville.HOTween.EaseType.EaseOutQuint; break;
		case eEaseType.InOutQuint:		result = Holoville.HOTween.EaseType.EaseInOutQuint; break;
		case eEaseType.InSine:			result = Holoville.HOTween.EaseType.EaseInSine; break;
		case eEaseType.OutSine:			result = Holoville.HOTween.EaseType.EaseOutSine; break;
		case eEaseType.InOutSine:		result = Holoville.HOTween.EaseType.EaseInOutSine; break;
		case eEaseType.InExpo:			result = Holoville.HOTween.EaseType.EaseInExpo; break;
		case eEaseType.OutExpo:			result = Holoville.HOTween.EaseType.EaseOutExpo; break;
		case eEaseType.InOutExpo:		result = Holoville.HOTween.EaseType.EaseInOutExpo; break;
		case eEaseType.InCirc:			result = Holoville.HOTween.EaseType.EaseInCirc; break;
		case eEaseType.OutCirc:			result = Holoville.HOTween.EaseType.EaseOutCirc; break;
		case eEaseType.InOutCirc:		result = Holoville.HOTween.EaseType.EaseInOutCirc; break;
		case eEaseType.linear:			result = Holoville.HOTween.EaseType.Linear; break;
		case eEaseType.InBounce:		result = Holoville.HOTween.EaseType.EaseInBounce; break;
		case eEaseType.OutBounce:		result = Holoville.HOTween.EaseType.EaseOutBounce; break;
		case eEaseType.InOutBounce:		result = Holoville.HOTween.EaseType.EaseInOutBounce; break;
		case eEaseType.InBack:			result = Holoville.HOTween.EaseType.EaseInBack; break;
		case eEaseType.OutBack:			result = Holoville.HOTween.EaseType.EaseOutBack; break;
		case eEaseType.InOutBack:		result = Holoville.HOTween.EaseType.EaseInOutBack; break;
		case eEaseType.InElastic:		result = Holoville.HOTween.EaseType.EaseInElastic; break;
		case eEaseType.OutElastic:		result = Holoville.HOTween.EaseType.EaseOutElastic; break;
		case eEaseType.InOutElastic:	result = Holoville.HOTween.EaseType.EaseInOutElastic; break;
		default:						result = Holoville.HOTween.EaseType.Linear; break;
		}
		return result;
	}
#elif ITWEEN
	// iTween: https://www.assetstore.unity3d.com/#/content/84 
	// iTween Documentation: http://itween.pixelplacement.com/documentation.php
	public string iTweenEaseType(eEaseType easeType)
	{
		string result = "linear";
		switch (easeType)
		{
		case eEaseType.InQuad:			result = "easeInQuad"; break;
		case eEaseType.OutQuad:			result = "easeOutQuad"; break;
		case eEaseType.InOutQuad:		result = "easeInOutQuad"; break;
		case eEaseType.InCubic:			result = "easeOutCubic"; break;
		case eEaseType.OutCubic:		result = "easeOutCubic"; break;
		case eEaseType.InOutCubic:		result = "easeInOutCubic"; break;
		case eEaseType.InQuart:			result = "easeInQuart"; break;
		case eEaseType.OutQuart:		result = "easeOutQuart"; break;
		case eEaseType.InOutQuart:		result = "easeInOutQuart"; break;
		case eEaseType.InQuint:			result = "easeInQuint"; break;
		case eEaseType.OutQuint:		result = "easeOutQuint"; break;
		case eEaseType.InOutQuint:		result = "easeInOutQuint"; break;
		case eEaseType.InSine:			result = "easeInSine"; break;
		case eEaseType.OutSine:			result = "easeOutSine"; break;
		case eEaseType.InOutSine:		result = "easeInOutSine"; break;
		case eEaseType.InExpo:			result = "easeInExpo"; break;
		case eEaseType.OutExpo:			result = "easeOutExpo"; break;
		case eEaseType.InOutExpo:		result = "easeInOutExpo"; break;
		case eEaseType.InCirc:			result = "easeInCirc"; break;
		case eEaseType.OutCirc:			result = "easeOutCirc"; break;
		case eEaseType.InOutCirc:		result = "easeInOutCirc"; break;
		case eEaseType.linear:			result = "linear"; break;
		case eEaseType.InBounce:		result = "easeInBounce"; break;
		case eEaseType.OutBounce:		result = "easeOutBounce"; break;
		case eEaseType.InOutBounce:		result = "easeInOutBounce"; break;
		case eEaseType.InBack:			result = "easeInBack"; break;
		case eEaseType.OutBack:			result = "easeOutBack"; break;
		case eEaseType.InOutBack:		result = "easeInOutBack"; break;
		case eEaseType.InElastic:		result = "easeInElastic"; break;
		case eEaseType.OutElastic:		result = "easeOutElastic"; break;
		case eEaseType.InOutElastic:	result = "easeInOutElastic"; break;
		default:						result = "linear"; break;
		}
		return result;
	}
#elif LEANTWEEN
	// LeanTween: https://www.assetstore.unity3d.com/#/content/3595
	// LeanTween Documentation: http://dentedpixel.com/LeanTweenDocumentation/classes/LeanTween.html
	public LeanTweenType LeanTweenEaseType(eEaseType easeType)
	{
		LeanTweenType result = LeanTweenType.linear;
		switch (easeType)
		{
			case eEaseType.InQuad: result = LeanTweenType.easeInQuad; break;
			case eEaseType.OutQuad: result = LeanTweenType.easeOutQuad; break;
			case eEaseType.InOutQuad: result = LeanTweenType.easeInOutQuad; break;
			case eEaseType.InCubic: result = LeanTweenType.easeOutCubic; break;
			case eEaseType.OutCubic: result = LeanTweenType.easeOutCubic; break;
			case eEaseType.InOutCubic: result = LeanTweenType.easeInOutCubic; break;
			case eEaseType.InQuart: result = LeanTweenType.easeInQuart; break;
			case eEaseType.OutQuart: result = LeanTweenType.easeOutQuart; break;
			case eEaseType.InOutQuart: result = LeanTweenType.easeInOutQuart; break;
			case eEaseType.InQuint: result = LeanTweenType.easeInQuint; break;
			case eEaseType.OutQuint: result = LeanTweenType.easeOutQuint; break;
			case eEaseType.InOutQuint: result = LeanTweenType.easeInOutQuint; break;
			case eEaseType.InSine: result = LeanTweenType.easeInSine; break;
			case eEaseType.OutSine: result = LeanTweenType.easeOutSine; break;
			case eEaseType.InOutSine: result = LeanTweenType.easeInOutSine; break;
			case eEaseType.InExpo: result = LeanTweenType.easeInExpo; break;
			case eEaseType.OutExpo: result = LeanTweenType.easeOutExpo; break;
			case eEaseType.InOutExpo: result = LeanTweenType.easeInOutExpo; break;
			case eEaseType.InCirc: result = LeanTweenType.easeInCirc; break;
			case eEaseType.OutCirc: result = LeanTweenType.easeOutCirc; break;
			case eEaseType.InOutCirc: result = LeanTweenType.easeInOutCirc; break;
			case eEaseType.linear: result = LeanTweenType.linear; break;
			case eEaseType.InBounce: result = LeanTweenType.easeInBounce; break;
			case eEaseType.OutBounce: result = LeanTweenType.easeOutBounce; break;
			case eEaseType.InOutBounce: result = LeanTweenType.easeInOutBounce; break;
			case eEaseType.InBack: result = LeanTweenType.easeInBack; break;
			case eEaseType.OutBack: result = LeanTweenType.easeOutBack; break;
			case eEaseType.InOutBack: result = LeanTweenType.easeInOutBack; break;
			case eEaseType.InElastic: result = LeanTweenType.easeInElastic; break;
			case eEaseType.OutElastic: result = LeanTweenType.easeOutElastic; break;
			case eEaseType.InOutElastic: result = LeanTweenType.easeInOutElastic; break;
			default: result = LeanTweenType.linear; break;
		}
		return result;
	}
#endif

	#endregion // EaseType Converter
}