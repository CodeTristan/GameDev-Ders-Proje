//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""MainSceneControls"",
            ""id"": ""3e9ec0c7-fbe8-446c-ad4b-112c4d385022"",
            ""actions"": [
                {
                    ""name"": ""MapControl"",
                    ""type"": ""Button"",
                    ""id"": ""9ae54531-e71b-4c33-bbdb-9b356243d99a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleQuestUI"",
                    ""type"": ""Button"",
                    ""id"": ""352c1c57-d9d0-42f4-a434-89570856d61a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleInventoryUI"",
                    ""type"": ""Button"",
                    ""id"": ""ed024103-45e8-4334-8a84-feba269a8278"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleMenuUI"",
                    ""type"": ""Button"",
                    ""id"": ""3804030c-ca2b-4ad3-a695-bbae88d3a8cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5b5cce21-508c-4063-82de-cf456fa8f71d"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2f75496-7a9b-4435-ac41-5447458edf25"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleQuestUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8773d9c3-8668-4c40-8dc3-34bf8ff71f50"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleInventoryUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be3d85ac-b1b7-418a-80a2-78bba615c6c5"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleMenuUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""609fe5e7-028e-47d7-a124-9479c73a01a7"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleMenuUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DialogControls"",
            ""id"": ""342d68d7-176f-4830-a026-dd09937dc6e2"",
            ""actions"": [
                {
                    ""name"": ""NextSentence"",
                    ""type"": ""Button"",
                    ""id"": ""2fb574cc-3a7f-4607-9edb-87fdd9ba3382"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DebugSkipBranch"",
                    ""type"": ""Button"",
                    ""id"": ""70206920-241e-4815-9792-f51caf2cfa6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleDialogCanvas"",
                    ""type"": ""Button"",
                    ""id"": ""45e9bd55-139a-4641-b125-9d81b75022a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a2e3912a-9e6d-4382-8a42-8f112f6d01ca"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextSentence"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b28b0ff-796e-4300-80b9-cd10d8e20bf4"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextSentence"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6d7d6ff-5589-4bfa-a06b-321e888299e8"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DebugSkipBranch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f87e5f07-6b44-4628-8561-91e967ee9628"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleDialogCanvas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ChoiceControls"",
            ""id"": ""d626a60a-1b90-4b9f-967d-39558af0eccd"",
            ""actions"": [
                {
                    ""name"": ""ConfirmChoice"",
                    ""type"": ""Button"",
                    ""id"": ""359a9f88-3c65-4cac-a23e-e683f0d16ef3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0176f98c-5db4-4a90-b0f2-8584b6a797aa"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0952bc9-6ffb-4ae7-8fec-1c5632af7f24"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4399f1f1-1ba3-4b49-ba2a-b095889a1d00"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50037874-7bce-43b6-a763-5bee3a1bb7c7"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5977f286-5d51-41d8-860a-79db0915ed51"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca89539b-93d1-4b4f-b935-c75119d40fc5"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""045a443e-ce93-43ef-a88f-ced0fb3e0a7e"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b5da6f1-3277-48e6-bd06-31adb3144568"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c425bf01-36ec-4797-b164-31bfad0b5f13"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bae531e5-ef51-471d-9f3f-d89ddfe8d782"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a60ceb66-8f70-4504-b84a-9c4b8815f775"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9950f4c9-644a-403f-9421-d206c193a1f6"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a44b1c7a-b445-4afc-9438-5f1e8d66175f"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd08a06f-8ff7-4695-92a3-652a75fd3e77"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46f22883-87cf-48ce-bdb5-c0f30126a737"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db97083d-6dbf-4f3b-bf5d-055f51ea5066"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fd3dda3-c9a3-41df-ab9e-4be9dfc8e95f"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0eaa7d1e-82ce-4455-9b03-adbbc49b8f52"",
                    ""path"": ""<Keyboard>/numpad9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChoice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""LogControls"",
            ""id"": ""3af0b08d-cf12-4948-a0ad-816f2f2959cc"",
            ""actions"": [
                {
                    ""name"": ""CloseDialogLog"",
                    ""type"": ""Button"",
                    ""id"": ""15b05b33-3f0b-4872-ac30-bad172ff5a51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShowDialogLog"",
                    ""type"": ""Button"",
                    ""id"": ""379163f1-b892-452b-b38c-120e9e20fdb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2f4449d1-e5bb-4aff-b06d-a50eac8edc2c"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShowDialogLog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de25189b-d66d-469c-9244-9e7955657742"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseDialogLog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainSceneControls
        m_MainSceneControls = asset.FindActionMap("MainSceneControls", throwIfNotFound: true);
        m_MainSceneControls_MapControl = m_MainSceneControls.FindAction("MapControl", throwIfNotFound: true);
        m_MainSceneControls_ToggleQuestUI = m_MainSceneControls.FindAction("ToggleQuestUI", throwIfNotFound: true);
        m_MainSceneControls_ToggleInventoryUI = m_MainSceneControls.FindAction("ToggleInventoryUI", throwIfNotFound: true);
        m_MainSceneControls_ToggleMenuUI = m_MainSceneControls.FindAction("ToggleMenuUI", throwIfNotFound: true);
        // DialogControls
        m_DialogControls = asset.FindActionMap("DialogControls", throwIfNotFound: true);
        m_DialogControls_NextSentence = m_DialogControls.FindAction("NextSentence", throwIfNotFound: true);
        m_DialogControls_DebugSkipBranch = m_DialogControls.FindAction("DebugSkipBranch", throwIfNotFound: true);
        m_DialogControls_ToggleDialogCanvas = m_DialogControls.FindAction("ToggleDialogCanvas", throwIfNotFound: true);
        // ChoiceControls
        m_ChoiceControls = asset.FindActionMap("ChoiceControls", throwIfNotFound: true);
        m_ChoiceControls_ConfirmChoice = m_ChoiceControls.FindAction("ConfirmChoice", throwIfNotFound: true);
        // LogControls
        m_LogControls = asset.FindActionMap("LogControls", throwIfNotFound: true);
        m_LogControls_CloseDialogLog = m_LogControls.FindAction("CloseDialogLog", throwIfNotFound: true);
        m_LogControls_ShowDialogLog = m_LogControls.FindAction("ShowDialogLog", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MainSceneControls
    private readonly InputActionMap m_MainSceneControls;
    private List<IMainSceneControlsActions> m_MainSceneControlsActionsCallbackInterfaces = new List<IMainSceneControlsActions>();
    private readonly InputAction m_MainSceneControls_MapControl;
    private readonly InputAction m_MainSceneControls_ToggleQuestUI;
    private readonly InputAction m_MainSceneControls_ToggleInventoryUI;
    private readonly InputAction m_MainSceneControls_ToggleMenuUI;
    public struct MainSceneControlsActions
    {
        private @PlayerActions m_Wrapper;
        public MainSceneControlsActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MapControl => m_Wrapper.m_MainSceneControls_MapControl;
        public InputAction @ToggleQuestUI => m_Wrapper.m_MainSceneControls_ToggleQuestUI;
        public InputAction @ToggleInventoryUI => m_Wrapper.m_MainSceneControls_ToggleInventoryUI;
        public InputAction @ToggleMenuUI => m_Wrapper.m_MainSceneControls_ToggleMenuUI;
        public InputActionMap Get() { return m_Wrapper.m_MainSceneControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainSceneControlsActions set) { return set.Get(); }
        public void AddCallbacks(IMainSceneControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_MainSceneControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainSceneControlsActionsCallbackInterfaces.Add(instance);
            @MapControl.started += instance.OnMapControl;
            @MapControl.performed += instance.OnMapControl;
            @MapControl.canceled += instance.OnMapControl;
            @ToggleQuestUI.started += instance.OnToggleQuestUI;
            @ToggleQuestUI.performed += instance.OnToggleQuestUI;
            @ToggleQuestUI.canceled += instance.OnToggleQuestUI;
            @ToggleInventoryUI.started += instance.OnToggleInventoryUI;
            @ToggleInventoryUI.performed += instance.OnToggleInventoryUI;
            @ToggleInventoryUI.canceled += instance.OnToggleInventoryUI;
            @ToggleMenuUI.started += instance.OnToggleMenuUI;
            @ToggleMenuUI.performed += instance.OnToggleMenuUI;
            @ToggleMenuUI.canceled += instance.OnToggleMenuUI;
        }

        private void UnregisterCallbacks(IMainSceneControlsActions instance)
        {
            @MapControl.started -= instance.OnMapControl;
            @MapControl.performed -= instance.OnMapControl;
            @MapControl.canceled -= instance.OnMapControl;
            @ToggleQuestUI.started -= instance.OnToggleQuestUI;
            @ToggleQuestUI.performed -= instance.OnToggleQuestUI;
            @ToggleQuestUI.canceled -= instance.OnToggleQuestUI;
            @ToggleInventoryUI.started -= instance.OnToggleInventoryUI;
            @ToggleInventoryUI.performed -= instance.OnToggleInventoryUI;
            @ToggleInventoryUI.canceled -= instance.OnToggleInventoryUI;
            @ToggleMenuUI.started -= instance.OnToggleMenuUI;
            @ToggleMenuUI.performed -= instance.OnToggleMenuUI;
            @ToggleMenuUI.canceled -= instance.OnToggleMenuUI;
        }

        public void RemoveCallbacks(IMainSceneControlsActions instance)
        {
            if (m_Wrapper.m_MainSceneControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainSceneControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_MainSceneControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainSceneControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainSceneControlsActions @MainSceneControls => new MainSceneControlsActions(this);

    // DialogControls
    private readonly InputActionMap m_DialogControls;
    private List<IDialogControlsActions> m_DialogControlsActionsCallbackInterfaces = new List<IDialogControlsActions>();
    private readonly InputAction m_DialogControls_NextSentence;
    private readonly InputAction m_DialogControls_DebugSkipBranch;
    private readonly InputAction m_DialogControls_ToggleDialogCanvas;
    public struct DialogControlsActions
    {
        private @PlayerActions m_Wrapper;
        public DialogControlsActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @NextSentence => m_Wrapper.m_DialogControls_NextSentence;
        public InputAction @DebugSkipBranch => m_Wrapper.m_DialogControls_DebugSkipBranch;
        public InputAction @ToggleDialogCanvas => m_Wrapper.m_DialogControls_ToggleDialogCanvas;
        public InputActionMap Get() { return m_Wrapper.m_DialogControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogControlsActions set) { return set.Get(); }
        public void AddCallbacks(IDialogControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_DialogControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DialogControlsActionsCallbackInterfaces.Add(instance);
            @NextSentence.started += instance.OnNextSentence;
            @NextSentence.performed += instance.OnNextSentence;
            @NextSentence.canceled += instance.OnNextSentence;
            @DebugSkipBranch.started += instance.OnDebugSkipBranch;
            @DebugSkipBranch.performed += instance.OnDebugSkipBranch;
            @DebugSkipBranch.canceled += instance.OnDebugSkipBranch;
            @ToggleDialogCanvas.started += instance.OnToggleDialogCanvas;
            @ToggleDialogCanvas.performed += instance.OnToggleDialogCanvas;
            @ToggleDialogCanvas.canceled += instance.OnToggleDialogCanvas;
        }

        private void UnregisterCallbacks(IDialogControlsActions instance)
        {
            @NextSentence.started -= instance.OnNextSentence;
            @NextSentence.performed -= instance.OnNextSentence;
            @NextSentence.canceled -= instance.OnNextSentence;
            @DebugSkipBranch.started -= instance.OnDebugSkipBranch;
            @DebugSkipBranch.performed -= instance.OnDebugSkipBranch;
            @DebugSkipBranch.canceled -= instance.OnDebugSkipBranch;
            @ToggleDialogCanvas.started -= instance.OnToggleDialogCanvas;
            @ToggleDialogCanvas.performed -= instance.OnToggleDialogCanvas;
            @ToggleDialogCanvas.canceled -= instance.OnToggleDialogCanvas;
        }

        public void RemoveCallbacks(IDialogControlsActions instance)
        {
            if (m_Wrapper.m_DialogControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDialogControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_DialogControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DialogControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DialogControlsActions @DialogControls => new DialogControlsActions(this);

    // ChoiceControls
    private readonly InputActionMap m_ChoiceControls;
    private List<IChoiceControlsActions> m_ChoiceControlsActionsCallbackInterfaces = new List<IChoiceControlsActions>();
    private readonly InputAction m_ChoiceControls_ConfirmChoice;
    public struct ChoiceControlsActions
    {
        private @PlayerActions m_Wrapper;
        public ChoiceControlsActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ConfirmChoice => m_Wrapper.m_ChoiceControls_ConfirmChoice;
        public InputActionMap Get() { return m_Wrapper.m_ChoiceControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ChoiceControlsActions set) { return set.Get(); }
        public void AddCallbacks(IChoiceControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_ChoiceControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ChoiceControlsActionsCallbackInterfaces.Add(instance);
            @ConfirmChoice.started += instance.OnConfirmChoice;
            @ConfirmChoice.performed += instance.OnConfirmChoice;
            @ConfirmChoice.canceled += instance.OnConfirmChoice;
        }

        private void UnregisterCallbacks(IChoiceControlsActions instance)
        {
            @ConfirmChoice.started -= instance.OnConfirmChoice;
            @ConfirmChoice.performed -= instance.OnConfirmChoice;
            @ConfirmChoice.canceled -= instance.OnConfirmChoice;
        }

        public void RemoveCallbacks(IChoiceControlsActions instance)
        {
            if (m_Wrapper.m_ChoiceControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IChoiceControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_ChoiceControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ChoiceControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ChoiceControlsActions @ChoiceControls => new ChoiceControlsActions(this);

    // LogControls
    private readonly InputActionMap m_LogControls;
    private List<ILogControlsActions> m_LogControlsActionsCallbackInterfaces = new List<ILogControlsActions>();
    private readonly InputAction m_LogControls_CloseDialogLog;
    private readonly InputAction m_LogControls_ShowDialogLog;
    public struct LogControlsActions
    {
        private @PlayerActions m_Wrapper;
        public LogControlsActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @CloseDialogLog => m_Wrapper.m_LogControls_CloseDialogLog;
        public InputAction @ShowDialogLog => m_Wrapper.m_LogControls_ShowDialogLog;
        public InputActionMap Get() { return m_Wrapper.m_LogControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LogControlsActions set) { return set.Get(); }
        public void AddCallbacks(ILogControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_LogControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LogControlsActionsCallbackInterfaces.Add(instance);
            @CloseDialogLog.started += instance.OnCloseDialogLog;
            @CloseDialogLog.performed += instance.OnCloseDialogLog;
            @CloseDialogLog.canceled += instance.OnCloseDialogLog;
            @ShowDialogLog.started += instance.OnShowDialogLog;
            @ShowDialogLog.performed += instance.OnShowDialogLog;
            @ShowDialogLog.canceled += instance.OnShowDialogLog;
        }

        private void UnregisterCallbacks(ILogControlsActions instance)
        {
            @CloseDialogLog.started -= instance.OnCloseDialogLog;
            @CloseDialogLog.performed -= instance.OnCloseDialogLog;
            @CloseDialogLog.canceled -= instance.OnCloseDialogLog;
            @ShowDialogLog.started -= instance.OnShowDialogLog;
            @ShowDialogLog.performed -= instance.OnShowDialogLog;
            @ShowDialogLog.canceled -= instance.OnShowDialogLog;
        }

        public void RemoveCallbacks(ILogControlsActions instance)
        {
            if (m_Wrapper.m_LogControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILogControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_LogControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LogControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LogControlsActions @LogControls => new LogControlsActions(this);
    public interface IMainSceneControlsActions
    {
        void OnMapControl(InputAction.CallbackContext context);
        void OnToggleQuestUI(InputAction.CallbackContext context);
        void OnToggleInventoryUI(InputAction.CallbackContext context);
        void OnToggleMenuUI(InputAction.CallbackContext context);
    }
    public interface IDialogControlsActions
    {
        void OnNextSentence(InputAction.CallbackContext context);
        void OnDebugSkipBranch(InputAction.CallbackContext context);
        void OnToggleDialogCanvas(InputAction.CallbackContext context);
    }
    public interface IChoiceControlsActions
    {
        void OnConfirmChoice(InputAction.CallbackContext context);
    }
    public interface ILogControlsActions
    {
        void OnCloseDialogLog(InputAction.CallbackContext context);
        void OnShowDialogLog(InputAction.CallbackContext context);
    }
}
