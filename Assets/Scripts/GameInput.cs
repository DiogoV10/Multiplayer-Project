// Ignore Spelling: Melee

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace V10
{
    public class GameInput : MonoBehaviour
    {


        private const string PLAYER_PREFS_BINDINGS = "InputBindings";


        public static GameInput Instance { get; private set; }


        public event EventHandler OnJumpAction;
        public event EventHandler OnSprintAction;


        public enum Binding
        {
            Move_Up,
            Move_Down,
            Move_Left,
            Move_Right,
            Run,
            Jump,
        }


        private PlayerInputActions playerInputActions;


        private void Awake()
        {
            Instance = this;

            playerInputActions = new PlayerInputActions();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }

            playerInputActions.Player.Enable();

            playerInputActions.Player.Jump.performed += Jump_performed;
            playerInputActions.Player.Sprint.performed += Sprint_performed;
        }

        private void Sprint_performed(InputAction.CallbackContext obj)
        {
            OnSprintAction?.Invoke(this, EventArgs.Empty);
        }

        private void Jump_performed(InputAction.CallbackContext obj)
        {
            OnJumpAction?.Invoke(this, EventArgs.Empty);
        }

        private void OnDestroy()
        {
            playerInputActions.Dispose();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

            inputVector = inputVector.normalized;

            return inputVector;
        }
        
        public Vector2 GetLookVector()
        {
            Vector2 inputVector = playerInputActions.Player.Look.ReadValue<Vector2>();

            return inputVector;
        }

        public string GetBindingText(Binding binding)
        {
            switch (binding)
            {
                default:
                case Binding.Move_Up:
                    return playerInputActions.Player.Move.bindings[1].ToDisplayString();
                case Binding.Move_Down:
                    return playerInputActions.Player.Move.bindings[2].ToDisplayString();
                case Binding.Move_Left:
                    return playerInputActions.Player.Move.bindings[3].ToDisplayString();
                case Binding.Move_Right:
                    return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            }
        }

        public void RebindBinding(Binding binding, Action onActionRebound)
        {
            playerInputActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                default:
                case Binding.Move_Up:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.Move_Down:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.Move_Left:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.Move_Right:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 4;
                    break;
            }

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    playerInputActions.Player.Enable();
                    onActionRebound();

                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();

                    //OnBindingRebind?.Invoke(this, EventArgs.Empty);
                })
                .Start();
        }


    }
}

