/**
* Copyright (c) 2020 Vuplex Inc. All rights reserved.
*
* Licensed under the Vuplex Commercial Software Library License, you may
* not use this file except in compliance with the License. You may obtain
* a copy of the License at
*
*     https://vuplex.com/commercial-library-license
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using TMPro;
#if NET_4_6 || NET_STANDARD_2_0
using System.Threading.Tasks;
#endif



namespace Vuplex.WebView
{
    /// <summary>
    /// A prefab that makes it easy to create and interact with an `IWebView` in world space.
    /// </summary>
    /// <remarks>
    /// `WebViewPrefab` takes care of creating and initializing an `IWebView`, displaying its texture,
    /// and handling click and scroll interactions from the user. So, all you need to do is load some web
    /// content from a URL or HTML string, and then the user can view and interact with it.
    ///
    /// You can create a `WebViewPrefab` one of the following ways:
    /// - By dragging WebViewPrefab.prefab into your scene via the editor and setting its "Initial URL" property.
    /// - By instantiating an instance at runtime with `WebViewPrefab.Instantiate()` and then
    ///   waiting for its `Initialized` event to be raised, after which you can call methods on its `WebView` property.
    ///
    /// `WebViewPrefab` handles standard events from Unity's input system
    /// (like `IPointerDownHandler`), so it works with input modules that extend Unity's `BaseInputModule`,
    /// like Unity's Standalone Input Module and third-party modules.
    ///
    /// If your use case requires a high degree of customization, you can instead create an `IWebView`
    /// outside of the prefab using `Web.CreateWebView()` and initialize it with a texture created
    /// with `Web.CreateMaterial()`.
    /// </remarks>
    [HelpURL("https://developer.vuplex.com/webview/WebViewPrefab")]
    public class WebViewPrefab : MonoBehaviour
    {

        //public event EventHandler Clicked;
        public float count;
        public Vector3 newPos;
        public Vector3 currentPos;
        public Vector3 changePos;
        public float changecount;

        public TextMeshProUGUI manual;
        public TextMeshProUGUI notManual;
        
        /// <summary>
        /// Indicates that the prefab was clicked. Note that the prefab automatically
        /// calls the `IWebView.Click()` method for you.
        /// </summary>
        public event EventHandler<ClickedEventArgs> Clicked;

        /// <summary>
        /// Indicates that the prefab finished initializing,
        /// so its `WebView` property is ready to use.
        /// </summary>
        public event EventHandler Initialized;

        /// <summary>
        /// Indicates that the prefab was scrolled. Note that the prefab automatically
        /// calls the `IWebView.Scroll()` method for you.
        /// </summary>
        public event EventHandler<ScrolledEventArgs> Scrolled;

        /// <summary>
        /// If you drag a WebViewPrefab.prefab into the scene via the editor,
        /// you can set this property to make it so that the instance
        /// automatically initializes itself with the given URL. To load a new URL
        /// after the prefab has been initialized, use `IWebView.LoadUrl()` instead.
        /// </summary>
        [Label("Initial URL to load (optional)")]
        [Tooltip("Or you can leave the Initial URL blank if you want to initialize the prefab programmatically by calling Init().")]
        public string InitialUrl;

        /// <summary>
        /// Sets the webview's initial resolution in pixels per Unity unit.
        /// You can change the resolution to make web content appear larger or smaller.
        /// For more information on scaling web content, see
        /// [this support article](https://support.vuplex.com/articles/how-to-scale-web-content).
        /// </summary>
        [Label("Initial Resolution (px / Unity unit)")]
        [Tooltip("You can change this to make web content appear larger or smaller.")]
        public float InitialResolution = 1300;

        /// <summary>
        /// Determines how the prefab handles drag interactions.
        /// </summary>
        [Tooltip("Note: \"Drag Within Page\" is not supported on iOS or UWP.")]
        public DragMode DragMode = DragMode.DragToScroll;

        [Header("Other Settings")]
        /// <summary>
        /// Clicking is enabled by default, but can be disabled by
        /// setting this property to `false`.
        /// </summary>
        public bool ClickingEnabled = true;

        /// <summary>
        /// Hover interactions are enabled by default, but can be disabled by
        /// setting this property to `false`.
        /// Note that hovering only works for webview implementations that
        /// support the `IWithMovablePointer` interface (i.e. Android, Windows, and macOS).
        /// </summary>
        [Tooltip("Note: Hovering is not supported on iOS or UWP.")]
        public bool HoveringEnabled = true;

        /// <summary>
        /// Scrolling is enabled by default, but can be disabled by
        /// setting this property to `false`.
        /// </summary>
        public bool ScrollingEnabled = true;

        /// <summary>
        /// Determines the threshold (in web pixels) for triggering a drag. The default is 20.
        /// </summary>
        /// <remarks>
        /// When the `DragMode` is set to `DragToScroll`, this property determines
        /// the distance that the pointer must drag before it's no longer
        /// considered a click.
        /// </remarks>
        /// <remarks>
        /// When the `DragMode` is set to `DragWithinPage`, this property determines
        /// the distance that the pointer must drag before it triggers
        /// a drag within the page.
        /// </remarks>
        [Label("Drag Threshold (px)")]
        [Tooltip("Determines the threshold (in web pixels) for triggering a drag.")]
        public float DragThreshold = 20;

        [Obsolete("The WebViewPrefab.DragToScrollThreshold property is obsolete. Please use DragThreshold instead.")]
        public float DragToScrollThreshold { get; set; }

        /// <summary>
        /// Allows the scroll sensitivity to be adjusted.
        /// The default sensitivity is 0.005.
        /// </summary>
        public float ScrollingSensitivity = 0.005f;

        [Obsolete("The static WebViewPrefab.ScrollSensitivity property is obsolete. Please use one of the following instance properties instead: WebViewPrefab.ScrollingSensitivity or CanvasWebViewPrefab.ScrollingSensitivity.")]
        public static float ScrollSensitivity { get; set; }

        /// <summary>
        /// The prefab's collider.
        /// </summary>
        public Collider Collider
        {
            get
            {
                return _getDefaultView().GetComponent<Collider>();
            }
        }

        /// <summary>
        /// The prefab's material.
        /// </summary>
        public Material Material
        {
            get
            {
                return _view == null ? null : _view.Material;
            }
            set
            {
                _view.Material = value;
            }
        }

        /// <summary>
        /// Controls whether the instance is visible or hidden.
        /// </summary>
        public bool Visible
        {
            // Use _getView and _getVideoLayer in case the instance isn't
            // initialized yet.
            get
            {
                return _getView().Visible || _getVideoLayer().Visible;
            }
            set
            {
                _getView().Visible = value;
                _getVideoLayer().Visible = value;
            }
        }

        /// <summary>
        /// A reference to the prefab's `IWebView` instance, which
        /// is available after the `Initialized` event is raised.
        /// Before initialization is complete, this property is `null`.
        /// </summary>
        public IWebView WebView { get { return _webView; } }

        /// <summary>
        /// Creates a new instance with the given
        /// dimensions in Unity units and initializes it asynchronously.
        /// </summary>
        /// <remarks>
        /// The `WebView` property is available after initialization completes,
        /// which is indicated by the `Initialized` event.
        /// A webview's default resolution is 1300px per Unity unit but can be
        /// changed with `IWebView.SetResolution()`.
        /// </remarks>
        /// <example>
        /// Example:
        ///
        /// ```
        /// // Create a 0.5 x 0.5 instance
        /// var webViewPrefab = WebViewPrefab.Instantiate(0.5f, 0.5f);
        /// // Position the prefab how we want it
        /// webViewPrefab.transform.parent = transform;
        /// webViewPrefab.transform.localPosition = new Vector3(0, 0f, 0.5f);
        /// webViewPrefab.transform.LookAt(transform);
        /// // Load a URL once the prefab finishes initializing
        /// webViewPrefab.Initialized += (sender, e) => {
        ///     webViewPrefab.WebView.LoadUrl("https://vuplex.com");
        /// };
        /// ```
        /// </example>
        public static WebViewPrefab Instantiate(float width, float height)
        {

            return Instantiate(width, height, new WebViewOptions());
        }

        /// <summary>
        /// Like `Instantiate(float, float)`, except it also accepts an object
        /// of options flags that can be used to alter the generated webview's behavior.
        /// </summary>
        public static WebViewPrefab Instantiate(float width, float height, WebViewOptions options)
        {

            var prefabPrototype = (GameObject)Resources.Load("WebViewPrefab");
            var viewGameObject = (GameObject)Instantiate(prefabPrototype);
            var webViewPrefab = viewGameObject.GetComponent<WebViewPrefab>();
            webViewPrefab.Init(width, height, options);
            return webViewPrefab;
        }

        /// <summary>
        /// Like `Instantiate()`, except it initializes the instance with an existing, initialized
        /// `IWebView` instance. This causes the `WebViewPrefab` to use the existing
        /// `IWebView` instance instead of creating a new one.
        /// </summary>
        public static WebViewPrefab Instantiate(IWebView webView)
        {

            var prefabPrototype = (GameObject)Resources.Load("WebViewPrefab");
            var viewGameObject = (GameObject)Instantiate(prefabPrototype);
            var webViewPrefab = viewGameObject.GetComponent<WebViewPrefab>();
            webViewPrefab.Init(webView);
            return webViewPrefab;
        }

        /// <summary>
        /// Asynchronously initializes the instance using the width and height
        /// set via the Unity editor.
        /// </summary>
        /// <remarks>
        /// You only need to call this method if you place a WebViewPrefab.prefab in your
        /// scene via the Unity editor and don't set its "Initial URL" property.
        /// You don't need to call this method if you set the "Initial URL" property in
        /// the editor or if you instantiate the prefab programmatically at runtime using
        /// `Instantiate()`. In those cases, `Init()` is called automatically for you.
        /// This method asynchronously initializes the `WebView` property, which is
        /// available for use after the `Initialized` event is raised.
        /// A webview's default resolution is 1300px per Unity unit but can be
        /// changed with `IWebView.SetResolution()`.
        /// </remarks>
        public void Init()
        {

            _init(transform.localScale.x, transform.localScale.y);
        }

        /// <summary>
        /// Like `Init()`, except it initializes the instance to the specified
        /// width and height in Unity units.
        /// </summary>
        public virtual void Init(float width, float height)
        {

            _init(width, height);
        }

        /// <summary>
        /// Like `Init(float, float)`, except it also accepts an object
        /// of options flags that can be used to alter the webview's behavior.
        /// </summary>
        public virtual void Init(float width, float height, WebViewOptions options)
        {

            _init(width, height, options);
        }

        /// <summary>
        /// Like `Init()`, except it initializes the instance with an existing, initialized
        /// `IWebView` instance. This causes the `WebViewPrefab` to use the existing
        /// `IWebView` instance instead of creating a new one.
        /// </summary>
        public void Init(IWebView webView)
        {

            if (!webView.IsInitialized)
            {
                throw new ArgumentException("WebViewPrefab.Init(IWebView) was called with an uninitialized webview, but an initialized webview is required.");
            }
            _init(webView.Size.x, webView.Size.y, new WebViewOptions(), webView);
        }

        /// <summary>
        /// Converts the given world position to a normalized screen point.
        /// </summary>
        /// <returns>
        /// A point where the x and y components are normalized
        /// values between 0 and 1.
        /// </returns>
        public Vector2 ConvertToScreenPoint(Vector3 worldPosition)
        {

            var localPosition = _viewResizer.transform.InverseTransformPoint(worldPosition);
            return new Vector2(1 - localPosition.x, -1 * localPosition.y);
        }

        /// <summary>
        /// Destroys the instance and its children. Note that you don't have
        /// to call this method if you destroy the instance's parent with
        /// `Object.Destroy()`.
        /// </summary>
        public void Destroy()
        {

            UnityEngine.Object.Destroy(gameObject);
        }

        /// <summary>
        /// Resizes the prefab mesh and webview to the given dimensions in Unity units.
        /// </summary>
        /// <remarks>
        /// A webview's default resolution is 1300px per Unity unit but can be changed
        /// with `IWebView.SetResolution()`.
        /// </remarks>
        public void Resize(float width, float height)
        {

            if (_webView != null)
            {
                _webView.Resize(width, height);
            }
            _setViewSize(width, height);
        }

        public void SetCutoutRect(Rect rect)
        {

            _view.SetCutoutRect(rect);
        }

        /// <summary>
        /// By default, WebViewPrefab detects pointer input events like clicks through
        /// Unity's event system, but you can use this method to override the way that
        /// input events are detected.
        /// </summary>
        public void SetPointerInputDetector(IPointerInputDetector pointerInputDetector)
        {

            var previousPointerInputDetector = _pointerInputDetector;
            _pointerInputDetector = pointerInputDetector;
            // If _webView hasn't been set yet, then _initPointerInputDetector
            // will get called before it's set to initialize _pointerInputDetector.
            if (_webView != null)
            {
                _initPointerInputDetector(_webView, previousPointerInputDetector);
            }
        }

        public void SetView(ViewportMaterialView view)
        {

            _viewOverride = view;
            _getDefaultView().enabled = view == null;
        }

#if NET_4_6 || NET_STANDARD_2_0
        /// <summary>
        /// Returns a task that resolves when the prefab is initialized
        /// (i.e. when its `WebView` property is ready for use).
        /// </summary>
        public Task WaitUntilInitialized()
        {

            var taskCompletionSource = new TaskCompletionSource<bool>();
            var isInitialized = _webView != null;
            if (isInitialized)
            {
                taskCompletionSource.SetResult(true);
            }
            else
            {
                Initialized += (sender, e) => taskCompletionSource.SetResult(true);
            }
            return taskCompletionSource.Task;
        }
#endif

        Vector2 _pointerDownRatioPoint;
        // Used for DragMode.DragToScroll and DragMode.Disabled
        bool _clickIsPending;
        bool _loggedDragWarning;
        WebViewOptions _options;
        IPointerInputDetector _pointerInputDetector;
        bool _pointerIsDown;
        Vector2 _previousDragPoint;
        Vector2 _previousMovePointerPoint;
        ViewportMaterialView _videoLayer;
        bool _videoLayerDisabled;
        Material _videoMaterial;
        Transform _videoRectPositioner;
        protected ViewportMaterialView _view;
        Material _viewMaterial;
        ViewportMaterialView _viewOverride;
        public Transform _viewResizer;
        public IWebView _webView;
        GameObject _webViewGameObject;

        void _attachWebViewEventHandlers(IWebView webView)
        {

            if (!_options.disableVideo)
            {
                webView.VideoRectChanged += (sender, e) => _setVideoRect(e.Value);
            }
        }

        void Awake()
        {

            if (!String.IsNullOrEmpty(InitialUrl))
            {
                Init();
            }
        }

        Vector2 _convertRatioPointToUnityUnits(Vector2 point)
        {

            var unityUnitsX = _viewResizer.transform.localScale.x * point.x;
            var unityUnitsY = _viewResizer.transform.localScale.y * point.y;
            return new Vector2(unityUnitsX, unityUnitsY);
        }

        ViewportMaterialView _getDefaultView()
        {

            return transform.Find("WebViewPrefabResizer/WebViewPrefabView").GetComponent<ViewportMaterialView>();
        }

        ViewportMaterialView _getVideoLayer()
        {

            return _getVideoRectPositioner().GetComponentInChildren<ViewportMaterialView>();
        }

        Transform _getVideoRectPositioner()
        {

            var viewResizer = transform.GetChild(0);
            return viewResizer.Find("VideoRectPositioner");
        }

        protected virtual ViewportMaterialView _getView()
        {

            if (_viewOverride != null)
            {
                return _viewOverride;
            }
            return _getDefaultView();
        }

#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
        static void _handleScriptsReloadedInEditor() {

            var prefabs = Resources.FindObjectsOfTypeAll(typeof(WebViewPrefab)) as WebViewPrefab[];
            foreach (var prefab in prefabs) {
                if (prefab.gameObject.activeInHierarchy) {
                    prefab._reinitAfterScriptsReloaded();
                }
            }
        }
#endif

        void _init(float width, float height, WebViewOptions options = new WebViewOptions(), IWebView initializedWebView = null)
        {

            _throwExceptionIfInitialized();
            _options = options;
            _resetLocalScale();

            // Note: Only set _webView *after* the webview it has been initialized
            // in order to guarantee that if WebViewPrefab.WebView is ready to use
            // as long as it's not null.
            var webView = initializedWebView == null ? Web.CreateWebView(_options.preferredPlugins) : initializedWebView;
            var webViewMonoBehaviour = webView as MonoBehaviour;
            if (webViewMonoBehaviour != null)
            {
                // When scripts are reloaded in the editor, the _webView reference
                // is reset to null, so save a reference to the IWebView's gameObject
                // so that it can be used to recover the reference to the webview in that scenario.
                _webViewGameObject = webViewMonoBehaviour.gameObject;
            }
            _attachWebViewEventHandlers(webView);

            if (InitialResolution <= 0)
            {
                Debug.LogWarningFormat("Invalid value for InitialResolution ({0}) will be ignored.", InitialResolution);
            }
            else if (InitialResolution != Config.NumberOfPixelsPerUnityUnit)
            {
                if (webView.IsInitialized)
                {
                    Debug.LogWarning("Custom InitialResolution setting will be ignored because an initialized IWebView was provided.");
                }
                else
                {
                    // Set the resolution prior to initializing the webview
                    // so the initial size is correct.
                    webView.SetResolution(InitialResolution);
                }
            }

            _viewResizer = transform.GetChild(0);
            _setViewSize(width, height);
            _initView();
            Web.CreateMaterial(viewMaterial =>
            {
                _viewMaterial = viewMaterial;
                _view.Material = viewMaterial;
                _initWebViewIfReady(webView);
            });
            _videoRectPositioner = _getVideoRectPositioner();
            _initVideoLayer();
            if (options.disableVideo)
            {
                _videoLayerDisabled = true;
                _videoRectPositioner.gameObject.SetActive(false);
                _initWebViewIfReady(webView);
            }
            else
            {
                Web.CreateVideoMaterial(videoMaterial =>
                {
                    if (videoMaterial == null)
                    {
                        _videoLayerDisabled = true;
                        _videoRectPositioner.gameObject.SetActive(false);
                    }
                    else
                    {
                        _videoMaterial = videoMaterial;
                        _videoLayer.Material = videoMaterial;
                        _setVideoRect(new Rect(0, 0, 0, 0));
                    }
                    _initWebViewIfReady(webView);
                });
            }
        }

        void _initPointerInputDetector(IWebView webView, IPointerInputDetector previousPointerInputDetector = null)
        {

            if (previousPointerInputDetector != null)
            {
                previousPointerInputDetector.BeganDrag -= InputDetector_BeganDrag;
                previousPointerInputDetector.Dragged -= InputDetector_Dragged;
                previousPointerInputDetector.PointerDown -= InputDetector_PointerDown;
                previousPointerInputDetector.PointerExited -= InputDetector_PointerExited;
                previousPointerInputDetector.PointerMoved -= InputDetector_PointerMoved;
                previousPointerInputDetector.PointerUp -= InputDetector_PointerUp;
                previousPointerInputDetector.Scrolled -= InputDetector_Scrolled;
            }

            if (_pointerInputDetector == null)
            {
                _pointerInputDetector = _viewResizer.GetComponentInChildren<IPointerInputDetector>();
            }

            // Only enable the PointerMoved event if the webview implementation has MovePointer().
            _pointerInputDetector.PointerMovedEnabled = (webView as IWithMovablePointer) != null;
            _pointerInputDetector.BeganDrag += InputDetector_BeganDrag;
            _pointerInputDetector.Dragged += InputDetector_Dragged;
            _pointerInputDetector.PointerDown += InputDetector_PointerDown;
            _pointerInputDetector.PointerExited += InputDetector_PointerExited;
            _pointerInputDetector.PointerMoved += InputDetector_PointerMoved;
            _pointerInputDetector.PointerUp += InputDetector_PointerUp;
            _pointerInputDetector.Scrolled += InputDetector_Scrolled;
        }

        void _initVideoLayer()
        {

            _videoLayer = _getVideoLayer();
        }

        void _initView()
        {

            _view = _getView();
        }

        void _initWebViewIfReady(IWebView webView)
        {

            if (_view.Texture == null || (!_videoLayerDisabled && _videoLayer.Texture == null))
            {
                // Wait until both views' textures are ready.
                return;
            }
            var initializedWebViewWasProvided = webView.IsInitialized;
            if (initializedWebViewWasProvided)
            {
                // An initialized webview was provided via WebViewPrefab.Init(IWebView),
                // so just hook up its existing textures.
                _view.Texture = webView.Texture;
                _videoLayer.Texture = webView.VideoTexture;
            }
            else
            {
                webView.Init(_view.Texture, _viewResizer.localScale.x, _viewResizer.localScale.y, _videoLayer.Texture);
            }

            // Init the pointer input detector just before setting _webView so that
            // SetPointerInputDetector() will behave correctly if it's called before _webView is set.
            _initPointerInputDetector(webView);
            // _webView can be set now that the webview is initialized.
            _webView = webView;
            var handler = Initialized;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
            if (!String.IsNullOrEmpty(InitialUrl))
            {
                if (initializedWebViewWasProvided)
                {
                    Debug.LogWarning("Custom InitialUrl value will be ignored because an initialized webview was provided.");
                }
                else
                {
                    var url = InitialUrl.Trim();
                    if (!url.Contains(":"))
                    {
                        url = "http://" + url;
                    }
                    webView.LoadUrl(url);
                }
            }
        }

        void InputDetector_BeganDrag(object sender, EventArgs<Vector2> eventArgs)
        {

            _previousDragPoint = _convertRatioPointToUnityUnits(_pointerDownRatioPoint);
        }

        void InputDetector_Dragged(object sender, EventArgs<Vector2> eventArgs)
        {

            // The point is Vector3.zero when the user drags off of the screen.
            if (DragMode == DragMode.Disabled || _webView == null)
            {
                return;
            }
            var point = eventArgs.Value;
            var previousDragPoint = _previousDragPoint;
            var newDragPoint = _convertRatioPointToUnityUnits(point);
            _previousDragPoint = newDragPoint;
            var totalDragDelta = _convertRatioPointToUnityUnits(_pointerDownRatioPoint) - newDragPoint;

            if (DragMode == DragMode.DragWithinPage)
            {
                var dragThresholdReached = totalDragDelta.magnitude * _webView.Resolution > DragThreshold;
                if (dragThresholdReached)
                {
                    _movePointerIfNeeded(point);
                }
                return;
            }

            // DragMode == DragMode.DragToScroll
            var dragDelta = previousDragPoint - newDragPoint;
            _scrollIfNeeded(dragDelta, _pointerDownRatioPoint);

            // Check whether to cancel a pending viewport click so that drag-to-scroll
            // doesn't unintentionally trigger a click.
            if (_clickIsPending)
            {
                var dragThresholdReached = totalDragDelta.magnitude * _webView.Resolution > DragThreshold;
                if (dragThresholdReached)
                {
                    _clickIsPending = false;
                }
            }
        }

        protected virtual void InputDetector_PointerDown(object sender, PointerEventArgs eventArgs)
        {

            _pointerIsDown = true;
            _pointerDownRatioPoint = eventArgs.Point;

            if (!ClickingEnabled || _webView == null)
            {
                return;
            }
            if (DragMode == DragMode.DragWithinPage)
            {
                var webViewWithPointerDown = _webView as IWithPointerDownAndUp;
                if (webViewWithPointerDown != null)
                {
                    webViewWithPointerDown.PointerDown(eventArgs.Point, eventArgs.ToPointerOptions());
                    return;
                }
                else if (!_loggedDragWarning)
                {
                    _loggedDragWarning = true;
                    Debug.LogWarningFormat("The WebViewPrefab's DragMode is set to DragWithinPage, but the webview implementation for this platform ({0}) doesn't support the PointerDown and PointerUp methods needed for dragging within a page. For more info, see https://developer.vuplex.com/webview/IWithPointerDownAndUp .", _webView.PluginType);
                    // Fallback to setting _clickIsPending so Click() can be called.
                }
            }
            // Defer calling PointerDown() for DragToScroll so that the click can
            // be cancelled if drag exceeds the threshold needed to become a scroll.
            _clickIsPending = true;
        }

        void InputDetector_PointerExited(object sender, EventArgs eventArgs)
        {

            if (HoveringEnabled)
            {
                // Remove the hover state when the pointer exits.
                _movePointerIfNeeded(Vector2.zero);
            }
        }

        void InputDetector_PointerMoved(object sender, EventArgs<Vector2> eventArgs)
        {

            // InputDetector_Dragged handles calling MovePointer while dragging.
            if (_pointerIsDown || !HoveringEnabled)
            {
                return;
            }
            _movePointerIfNeeded(eventArgs.Value);
        }

        protected virtual void InputDetector_PointerUp(object sender, PointerEventArgs eventArgs)
        {
            
            _pointerIsDown = false;
            if (!ClickingEnabled || _webView == null)
            {
                
                return;
            }
            var webViewWithPointerDownAndUp = _webView as IWithPointerDownAndUp;
            if (DragMode == DragMode.DragWithinPage && webViewWithPointerDownAndUp != null)
            {
                var totalDragDelta = _convertRatioPointToUnityUnits(_pointerDownRatioPoint) - _convertRatioPointToUnityUnits(eventArgs.Point);
                var dragThresholdReached = totalDragDelta.magnitude * _webView.Resolution > DragThreshold;
                var pointerUpPoint = dragThresholdReached ? eventArgs.Point : _pointerDownRatioPoint;
                webViewWithPointerDownAndUp.PointerUp(pointerUpPoint, eventArgs.ToPointerOptions());
            }
            else
            {
                if (!_clickIsPending)
                {
                    Debug.Log("Yow");
                    return;
                }
                _clickIsPending = false;
                // PointerDown() and PointerUp() don't support the preventStealingFocus parameter.
                if (webViewWithPointerDownAndUp == null || _options.clickWithoutStealingFocus)
                {
                    _webView.Click(eventArgs.Point, _options.clickWithoutStealingFocus);
                    //notManual.text = eventArgs.Point.ToString();
                    Debug.Log("This");
                }
                else
                {
                    var pointerOptions = eventArgs.ToPointerOptions();
                    webViewWithPointerDownAndUp.PointerDown(eventArgs.Point, pointerOptions);
                    webViewWithPointerDownAndUp.PointerUp(eventArgs.Point, pointerOptions);
                    Debug.Log("That");
                }
            }

            var handler = Clicked;
            if (handler != null)
            {
                Debug.Log("Yaw");
                handler(this, new ClickedEventArgs(eventArgs.Point));
            }
        }

        void InputDetector_Scrolled(object sender, ScrolledEventArgs eventArgs)
        {

            var scaledScrollDelta = new Vector2(
                eventArgs.ScrollDelta.x * ScrollingSensitivity,
                eventArgs.ScrollDelta.y * ScrollingSensitivity
            );
            _scrollIfNeeded(scaledScrollDelta, eventArgs.Point);
        }

        void _movePointerIfNeeded(Vector2 point)
        {

            var webViewWithMovablePointer = _webView as IWithMovablePointer;
            if (webViewWithMovablePointer == null)
            {
                return;
            }
            if (point != _previousMovePointerPoint)
            {
                _previousMovePointerPoint = point;
                webViewWithMovablePointer.MovePointer(point);
            }
        }

        void OnDestroy()
        {

            if (_webView != null && !_webView.IsDisposed)
            {
                _webView.Dispose();
            }
            Destroy();
            // Unity doesn't automatically destroy materials and textures
            // when the GameObject is destroyed.
            if (_viewMaterial != null)
            {
                Destroy(_viewMaterial.mainTexture);
                Destroy(_viewMaterial);
            }
            if (_videoMaterial != null)
            {
                Destroy(_videoMaterial.mainTexture);
                Destroy(_videoMaterial);
            }
        }

        void _reinitAfterScriptsReloaded()
        {

            if (_webViewGameObject == null)
            {
                // The IWebView is not a MonoBehaviour, so the reference to it cannot
                // be recovered after the scripts were recompiled in the editor.
                return;
            }
            var webView = _webViewGameObject.GetComponentInChildren<IWebView>();
            _attachWebViewEventHandlers(webView);
            _initView();
            _initVideoLayer();
            _initPointerInputDetector(webView);
            _webView = webView;
        }

        /// <summary>
        /// The top-level WebViewPrefab object's scale must be (1, 1),
        /// so the scale that was set via the editor is transferred from WebViewPrefab
        /// to WebViewPrefabResizer, and WebViewPrefab is moved to compensate
        /// for how WebViewPrefabResizer is moved in _setViewSize.
        /// </summary>
        void _resetLocalScale()
        {

            var localScale = transform.localScale;
            var localPosition = transform.localPosition;
            transform.localScale = new Vector3(1, 1, localScale.z);
            var offsetMagnitude = 0.5f * localScale.x;
            transform.localPosition = transform.localPosition + Quaternion.Euler(transform.localEulerAngles) * new Vector3(offsetMagnitude, 0, 0);
        }

        void _scrollIfNeeded(Vector2 scrollDelta, Vector2 point)
        {

            // scrollDelta can be zero when the user drags the cursor off the screen.
            if (!ScrollingEnabled || _webView == null || scrollDelta == Vector2.zero)
            {
                return;
            }
            _webView.Scroll(scrollDelta, point);
            var handler = Scrolled;
            if (handler != null)
            {
                handler(this, new ScrolledEventArgs(scrollDelta, point));
            }
        }

        void _setVideoRect(Rect videoRect)
        {

            _view.SetCutoutRect(videoRect);
            // The origins of the prefab and the video rect are in their top-right
            // corners instead of their top-left corners.
            _videoRectPositioner.localPosition = new Vector3(
                1 - (videoRect.x + videoRect.width),
                -1 * videoRect.y,
                _videoRectPositioner.localPosition.z
            );
            _videoRectPositioner.localScale = new Vector3(videoRect.width, videoRect.height, _videoRectPositioner.localScale.z);

            // This code applies a cropping rect to the video layer's shader based on what part of the video (if any)
            // falls outside of the viewport and therefore needs to be hidden. Note that the dimensions here are divided
            // by the videoRect's width or height, because in the videoLayer shader, the width of the videoRect is 1
            // and the height is 1 (i.e. the dimensions are normalized).
            float videoRectXMin = Math.Max(0, -1 * videoRect.x / videoRect.width);
            float videoRectYMin = Math.Max(0, -1 * videoRect.y / videoRect.height);
            float videoRectXMax = Math.Min(1, (1 - videoRect.x) / videoRect.width);
            float videoRectYMax = Math.Min(1, (1 - videoRect.y) / videoRect.height);
            var videoCropRect = Rect.MinMaxRect(videoRectXMin, videoRectYMin, videoRectXMax, videoRectYMax);
            if (videoCropRect == new Rect(0, 0, 1, 1))
            {
                // The entire video rect fits within the viewport, so set the cropt rect to zero to disable it.
                videoCropRect = new Rect(0, 0, 0, 0);
            }
            _videoLayer.SetCropRect(videoCropRect);
        }

        void _setViewSize(float width, float height)
        {

            var depth = _viewResizer.localScale.z;
            _viewResizer.localScale = new Vector3(width, height, depth);
            var localPosition = _viewResizer.localPosition;
            // Set the view resizer so that its middle aligns with the middle of this parent class's gameobject.
            localPosition.x = width * -0.5f;
            _viewResizer.localPosition = localPosition;
        }

        void _throwExceptionIfInitialized()
        {

            if (_webView != null)
            {
                throw new InvalidOperationException("Init() cannot be called on a WebViewPrefab that has already been initialized.");
            }
        }

        void Update()
        {
            changecount++;
            newPos = GameObject.Find("Main Camera").GetComponent<GazeProvider>().HitPosition;
            if (newPos == changePos)
            {

                count++;
                if (count % 100 == 99)
                {
                    //Debug.Log(newPos);
                    //View_Dwell();
                    count = 0;
                }
            }
            if (newPos != changePos)
            {
                count = 0;
            }
            if (changecount % 30 == 29)
            {
                changePos = newPos;
                changecount = 0;
            }
            /*else
            {
                count = 0;
                Debug.Log("Change");
            }*/
            /*if (newPos != GameObject.Find("Main Camera").GetComponent<GazeProvider>().HitPosition)
            {
                count = 0;
            }*/


        }
        public void View_Dwell()
        {
            /*
                        if (!(_clickingEnabled && _clickIsPending))
                        {
                            return;
                        }*/
            _clickIsPending = false;


            // HACK: clickHandler is assigned here because there's a weird I2CPP issue
            // when targeting x86 UWP where accessing Clicked after accessing _webView
            // below leads to an access violation.
            //var clickHandler = Clicked;
            Vector3 hitpos = GameObject.Find("Main Camera").GetComponent<GazeProvider>().HitPosition;
            var point1 = _convertToScreenPosition(hitpos);
            Debug.Log("The point1 is "+ point1);
            //Debug.Log("screenpos" + point1);
            /*Vector2 point2 = point1;
            Debug.Log("screenpos2" + point2);*/
             // click
            //Debug.Log("Pointer down click");
            //Debug.Log(point1 + "click position");
            currentPos = newPos;
            /*
                        if (clickHandler != null)
                        {
                            clickHandler(this, new ClickedEventArgs(point1));
                            Debug.Log("click event pointer up");
                        }*/
            Debug.Log(point1);
            //var webViewWithPointerDownAndUp = _webView as IWithPointerDownAndUp;
            //_webView.Click(point1,_options.clickWithoutStealingFocus);
            _webView.Click(point1);
            //
            try
            {
                manual.text = point1.ToString();
            }
            catch
            {

            }
            var input = new PointerEventArgs();
            input.Point = point1;
            //_clickIsPending = false;
            //InputDetector_PointerUp(this, input);
            //webViewWithPointerDownAndUp.PointerDown(point1);
            //webViewWithPointerDownAndUp.PointerUp(point1);
            Debug.Log("point up down");
            /*if (webViewWithPointerDownAndUp == null || _options.clickWithoutStealingFocus)
            {
                _webView.Click(point1);
                Debug.Log("Point clicked");
            }
            else
            {
                //var pointerOptions = eventArgs.ToPointerOptions();
                webViewWithPointerDownAndUp.PointerDown(point1);
                webViewWithPointerDownAndUp.PointerUp(point1);
                Debug.Log("point up down");
            }*/


            /*var handler = Clicked;
            //handler(this, new ClickedEventArgs(point1));
            if (handler != null)
            {
                handler(this, new ClickedEventArgs(point1));
                Debug.Log("test point");
            }*/


        }
        protected Vector2 _convertToScreenPosition(Vector3 worldPosition)
        {

            var localPosition = _viewResizer.transform.InverseTransformPoint(worldPosition);
            return new Vector2(1 - localPosition.x, -1 * localPosition.y);
        }
    }
}
