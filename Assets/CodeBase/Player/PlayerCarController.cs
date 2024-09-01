using CodeBase.Car;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerCarController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CarMover _carMover;

        private void FixedUpdate()
        {
            _carMover.Move(Input.GetAxis("Vertical"));
            _carMover.Turn(Input.GetAxis("Horizontal"));
            if (Input.GetKey(KeyCode.Space)) 
                _carMover.SetBrake(1f);
            else
                _carMover.SetBrake(0f);
        }
    }
}