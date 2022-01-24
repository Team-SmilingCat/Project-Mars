// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""caccde88-be8a-40cd-bf40-3ee436dba563"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""34eaac38-139d-4ca8-9b36-5d96216f3f17"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""46f24fe4-875c-4a28-94d1-fd934ce6425a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7cb9e783-3ec3-45e0-908e-fcbed6717b9d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""c0fc5bd8-9397-4eaa-a9f8-76056869b776"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""604509ae-4050-4092-b89c-ace0263fec98"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ef623d08-7431-4def-b323-b6d7590a1158"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6de0d3bb-6a5e-4a90-a915-86a3a224b6c4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f57ad9fc-dfa0-4d08-b6c4-48ca2a716aa7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""b30daf36-dea5-428d-8301-33cf7669e9b7"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e26cff93-54e9-492a-91f1-35dc77acf0d9"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""59761482-6f3b-4dfa-8afa-66e8746586b7"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f1589858-dce9-4772-8874-be4044970630"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fa81eddc-9249-4042-9039-3ededd845c40"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f76ed6a9-5f95-4db7-8f7d-e71530f3efce"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone,NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ad3bfbf-2578-47cc-9bfe-0be3ff1ec298"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Actions"",
            ""id"": ""48cf9b66-92b0-4149-bd47-4064dcde5c25"",
            ""actions"": [
                {
                    ""name"": ""Dash Button"",
                    ""type"": ""Button"",
                    ""id"": ""cb2d56fd-5c07-4139-81af-f8acb6e436c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk Button"",
                    ""type"": ""Button"",
                    ""id"": ""7a5d883c-0043-4631-a839-1d737da417af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump Button"",
                    ""type"": ""Button"",
                    ""id"": ""1737151f-f1e5-4168-b893-f26e7ee2e55e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lclick"",
                    ""type"": ""Button"",
                    ""id"": ""0c56cd4d-4128-4890-870f-f2ee1d3ba154"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rclick"",
                    ""type"": ""Button"",
                    ""id"": ""595379ae-d61c-48fb-85ec-75d64ec798fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hook"",
                    ""type"": ""Button"",
                    ""id"": ""102b40c6-1355-438c-a92b-fb95bcb5a032"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shield Button"",
                    ""type"": ""Button"",
                    ""id"": ""5d978f25-025e-4b64-a21f-60c2c354d6f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8418b7a1-9f15-4d8d-8008-97ecd1389835"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68858f28-6c17-4ecb-a0e3-11679c71b1dd"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a33359cc-a3d9-4a72-ad45-0bdf9df2a2fa"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5b385eb-3bd2-4b44-bd69-b442541ed91d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lclick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aabef1c1-63b8-497f-ac7f-22faeaa0bfab"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rclick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef3052fa-b4a2-4fa6-9a02-ab70a7e0e374"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1795441-f38d-4b29-b303-d9d05dfcc6d3"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shield Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        // Player Actions
        m_PlayerActions = asset.FindActionMap("Player Actions", throwIfNotFound: true);
        m_PlayerActions_DashButton = m_PlayerActions.FindAction("Dash Button", throwIfNotFound: true);
        m_PlayerActions_WalkButton = m_PlayerActions.FindAction("Walk Button", throwIfNotFound: true);
        m_PlayerActions_JumpButton = m_PlayerActions.FindAction("Jump Button", throwIfNotFound: true);
        m_PlayerActions_Lclick = m_PlayerActions.FindAction("Lclick", throwIfNotFound: true);
        m_PlayerActions_Rclick = m_PlayerActions.FindAction("Rclick", throwIfNotFound: true);
        m_PlayerActions_Hook = m_PlayerActions.FindAction("Hook", throwIfNotFound: true);
        m_PlayerActions_ShieldButton = m_PlayerActions.FindAction("Shield Button", throwIfNotFound: true);
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

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Player Actions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_DashButton;
    private readonly InputAction m_PlayerActions_WalkButton;
    private readonly InputAction m_PlayerActions_JumpButton;
    private readonly InputAction m_PlayerActions_Lclick;
    private readonly InputAction m_PlayerActions_Rclick;
    private readonly InputAction m_PlayerActions_Hook;
    private readonly InputAction m_PlayerActions_ShieldButton;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DashButton => m_Wrapper.m_PlayerActions_DashButton;
        public InputAction @WalkButton => m_Wrapper.m_PlayerActions_WalkButton;
        public InputAction @JumpButton => m_Wrapper.m_PlayerActions_JumpButton;
        public InputAction @Lclick => m_Wrapper.m_PlayerActions_Lclick;
        public InputAction @Rclick => m_Wrapper.m_PlayerActions_Rclick;
        public InputAction @Hook => m_Wrapper.m_PlayerActions_Hook;
        public InputAction @ShieldButton => m_Wrapper.m_PlayerActions_ShieldButton;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @DashButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDashButton;
                @DashButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDashButton;
                @DashButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDashButton;
                @WalkButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnWalkButton;
                @WalkButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnWalkButton;
                @WalkButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnWalkButton;
                @JumpButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJumpButton;
                @JumpButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJumpButton;
                @JumpButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJumpButton;
                @Lclick.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLclick;
                @Lclick.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLclick;
                @Lclick.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLclick;
                @Rclick.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRclick;
                @Rclick.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRclick;
                @Rclick.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRclick;
                @Hook.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHook;
                @Hook.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHook;
                @Hook.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnHook;
                @ShieldButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShieldButton;
                @ShieldButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShieldButton;
                @ShieldButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShieldButton;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DashButton.started += instance.OnDashButton;
                @DashButton.performed += instance.OnDashButton;
                @DashButton.canceled += instance.OnDashButton;
                @WalkButton.started += instance.OnWalkButton;
                @WalkButton.performed += instance.OnWalkButton;
                @WalkButton.canceled += instance.OnWalkButton;
                @JumpButton.started += instance.OnJumpButton;
                @JumpButton.performed += instance.OnJumpButton;
                @JumpButton.canceled += instance.OnJumpButton;
                @Lclick.started += instance.OnLclick;
                @Lclick.performed += instance.OnLclick;
                @Lclick.canceled += instance.OnLclick;
                @Rclick.started += instance.OnRclick;
                @Rclick.performed += instance.OnRclick;
                @Rclick.canceled += instance.OnRclick;
                @Hook.started += instance.OnHook;
                @Hook.performed += instance.OnHook;
                @Hook.canceled += instance.OnHook;
                @ShieldButton.started += instance.OnShieldButton;
                @ShieldButton.performed += instance.OnShieldButton;
                @ShieldButton.canceled += instance.OnShieldButton;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnDashButton(InputAction.CallbackContext context);
        void OnWalkButton(InputAction.CallbackContext context);
        void OnJumpButton(InputAction.CallbackContext context);
        void OnLclick(InputAction.CallbackContext context);
        void OnRclick(InputAction.CallbackContext context);
        void OnHook(InputAction.CallbackContext context);
        void OnShieldButton(InputAction.CallbackContext context);
    }
}
