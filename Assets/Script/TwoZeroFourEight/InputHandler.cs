using UnityEngine;
using UnityEngine.InputSystem;

namespace TwoZeroFourEight
{
    public class InputHandler : MonoBehaviour
    {
        private Keyboard keyboard;
        private float lastInputTime = 0f;

        private void Awake()
        {
            keyboard = Keyboard.current;
        }

        private void Update()
        {
            if (keyboard == null) return;
            
            // Don't process input if the game is over or input delay hasn't passed
            if (!Two_GameManager.Instance.GameStateManager.CanReceiveInput() || 
                Time.time - lastInputTime < Two_GameConstants.INPUT_DELAY)
            {
                return;
            }

            if (keyboard[Key.W].wasPressedThisFrame || keyboard[Key.UpArrow].wasPressedThisFrame)
            {
                Two_GameManager.Instance.MovementSystem.MoveUp();
                lastInputTime = Time.time;
            }
            else if (keyboard[Key.S].wasPressedThisFrame || keyboard[Key.DownArrow].wasPressedThisFrame)
            {
                Two_GameManager.Instance.MovementSystem.MoveDown();
                lastInputTime = Time.time;
            }
            else if (keyboard[Key.A].wasPressedThisFrame || keyboard[Key.LeftArrow].wasPressedThisFrame)
            {
                Two_GameManager.Instance.MovementSystem.MoveLeft();
                lastInputTime = Time.time;
            }
            else if (keyboard[Key.D].wasPressedThisFrame || keyboard[Key.RightArrow].wasPressedThisFrame)
            {
                Two_GameManager.Instance.MovementSystem.MoveRight();
                lastInputTime = Time.time;
            }
        }
    }
}