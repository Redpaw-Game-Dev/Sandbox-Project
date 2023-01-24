//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Settings/Input/Input Actions Config.inputactions
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

namespace Scripts.Input
{
    public partial class @InputActionsConfig : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActionsConfig()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Actions Config"",
    ""maps"": [
        {
            ""name"": ""Base"",
            ""id"": ""d7b5aa7a-2002-4aa7-9fc4-ea2c9156c9d0"",
            ""actions"": [
                {
                    ""name"": ""PointerMove"",
                    ""type"": ""Value"",
                    ""id"": ""1d932d30-aadf-44b9-881b-dcd264111162"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PointerClick"",
                    ""type"": ""Button"",
                    ""id"": ""9e083ea6-5691-4ac2-a5fe-d5a7957fed4a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""50dfe80f-27cf-4e07-917a-500f2d32d3d6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c1e34c9a-08ea-4ed1-a532-e195f94dfe4a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""PointerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8cfe1a1-1ce4-48f8-8a9b-81516e4a0fd5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""PointerClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a94fd3f-0164-4ff8-aa01-a358340ab82f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Touchscreen"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bdb35e57-392d-4af1-a7d0-d90efa9c4d2c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4ead261c-c981-48d4-ac23-15fcf987c844"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""59b69141-9023-4d4f-b77f-1e0b32dc4770"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b4bb8d23-5e1a-4c80-adb6-03990a87a5c8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""eec2993f-1504-43fb-948b-9fb633bc9ad5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Touchscreen"",
            ""bindingGroup"": ""Touchscreen"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Base
            m_Base = asset.FindActionMap("Base", throwIfNotFound: true);
            m_Base_PointerMove = m_Base.FindAction("PointerMove", throwIfNotFound: true);
            m_Base_PointerClick = m_Base.FindAction("PointerClick", throwIfNotFound: true);
            m_Base_Move = m_Base.FindAction("Move", throwIfNotFound: true);
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

        // Base
        private readonly InputActionMap m_Base;
        private IBaseActions m_BaseActionsCallbackInterface;
        private readonly InputAction m_Base_PointerMove;
        private readonly InputAction m_Base_PointerClick;
        private readonly InputAction m_Base_Move;
        public struct BaseActions
        {
            private @InputActionsConfig m_Wrapper;
            public BaseActions(@InputActionsConfig wrapper) { m_Wrapper = wrapper; }
            public InputAction @PointerMove => m_Wrapper.m_Base_PointerMove;
            public InputAction @PointerClick => m_Wrapper.m_Base_PointerClick;
            public InputAction @Move => m_Wrapper.m_Base_Move;
            public InputActionMap Get() { return m_Wrapper.m_Base; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BaseActions set) { return set.Get(); }
            public void SetCallbacks(IBaseActions instance)
            {
                if (m_Wrapper.m_BaseActionsCallbackInterface != null)
                {
                    @PointerMove.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnPointerMove;
                    @PointerMove.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnPointerMove;
                    @PointerMove.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnPointerMove;
                    @PointerClick.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnPointerClick;
                    @PointerClick.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnPointerClick;
                    @PointerClick.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnPointerClick;
                    @Move.started -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_BaseActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_BaseActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PointerMove.started += instance.OnPointerMove;
                    @PointerMove.performed += instance.OnPointerMove;
                    @PointerMove.canceled += instance.OnPointerMove;
                    @PointerClick.started += instance.OnPointerClick;
                    @PointerClick.performed += instance.OnPointerClick;
                    @PointerClick.canceled += instance.OnPointerClick;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public BaseActions @Base => new BaseActions(this);
        private int m_TouchscreenSchemeIndex = -1;
        public InputControlScheme TouchscreenScheme
        {
            get
            {
                if (m_TouchscreenSchemeIndex == -1) m_TouchscreenSchemeIndex = asset.FindControlSchemeIndex("Touchscreen");
                return asset.controlSchemes[m_TouchscreenSchemeIndex];
            }
        }
        private int m_KeyboardandMouseSchemeIndex = -1;
        public InputControlScheme KeyboardandMouseScheme
        {
            get
            {
                if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
                return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IBaseActions
        {
            void OnPointerMove(InputAction.CallbackContext context);
            void OnPointerClick(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
        }
    }
}