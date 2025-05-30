//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Source/Scripts/InputSystem/PlayerInput.inputactions
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

namespace Input
{
    public partial class @PlayerInput: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerClick"",
            ""id"": ""7f06d7f6-ba43-4f7d-816f-860ee464fe4d"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""0cba37ec-0976-4be3-ac35-aae23b061ff3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScreenPosition"",
                    ""type"": ""Value"",
                    ""id"": ""4d92e47c-0090-4aa6-b543-375d1c4d4d41"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""27a5ed63-464a-444c-a26f-74bb16e91a78"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97d00b6a-bd6b-4666-933b-768e189b6ebe"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7571f392-db37-4b5a-8450-fcd3fa1db4e4"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ScreenPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d0a20fe-acee-4a00-8e07-f0c1e9ebbf36"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""ScreenPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // PlayerClick
            m_PlayerClick = asset.FindActionMap("PlayerClick", throwIfNotFound: true);
            m_PlayerClick_Click = m_PlayerClick.FindAction("Click", throwIfNotFound: true);
            m_PlayerClick_ScreenPosition = m_PlayerClick.FindAction("ScreenPosition", throwIfNotFound: true);
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

        // PlayerClick
        private readonly InputActionMap m_PlayerClick;
        private List<IPlayerClickActions> m_PlayerClickActionsCallbackInterfaces = new List<IPlayerClickActions>();
        private readonly InputAction m_PlayerClick_Click;
        private readonly InputAction m_PlayerClick_ScreenPosition;
        public struct PlayerClickActions
        {
            private @PlayerInput m_Wrapper;
            public PlayerClickActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_PlayerClick_Click;
            public InputAction @ScreenPosition => m_Wrapper.m_PlayerClick_ScreenPosition;
            public InputActionMap Get() { return m_Wrapper.m_PlayerClick; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerClickActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerClickActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerClickActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerClickActionsCallbackInterfaces.Add(instance);
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScreenPosition.started += instance.OnScreenPosition;
                @ScreenPosition.performed += instance.OnScreenPosition;
                @ScreenPosition.canceled += instance.OnScreenPosition;
            }

            private void UnregisterCallbacks(IPlayerClickActions instance)
            {
                @Click.started -= instance.OnClick;
                @Click.performed -= instance.OnClick;
                @Click.canceled -= instance.OnClick;
                @ScreenPosition.started -= instance.OnScreenPosition;
                @ScreenPosition.performed -= instance.OnScreenPosition;
                @ScreenPosition.canceled -= instance.OnScreenPosition;
            }

            public void RemoveCallbacks(IPlayerClickActions instance)
            {
                if (m_Wrapper.m_PlayerClickActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerClickActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerClickActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerClickActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerClickActions @PlayerClick => new PlayerClickActions(this);
        private int m_MouseSchemeIndex = -1;
        public InputControlScheme MouseScheme
        {
            get
            {
                if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
                return asset.controlSchemes[m_MouseSchemeIndex];
            }
        }
        private int m_MobileSchemeIndex = -1;
        public InputControlScheme MobileScheme
        {
            get
            {
                if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
                return asset.controlSchemes[m_MobileSchemeIndex];
            }
        }
        public interface IPlayerClickActions
        {
            void OnClick(InputAction.CallbackContext context);
            void OnScreenPosition(InputAction.CallbackContext context);
        }
    }
}
