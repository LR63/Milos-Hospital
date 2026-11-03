using UnityEngine;
using static Script.PatientStates;

namespace Script
{
    public class Patient : MonoBehaviour
    {
        private PatientStates _state = Waiting;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            Debug.Log(_state);
        }

        // Update is called once per frame
        private void Update()
        {
            switch (_state)
            {
                case Waiting:
                    // Waiting for an empty room
                    break;
                case Walking:
                    // Walking toward a room
                    break;
                case Laying:
                    // Waiting for treatment
                    break;
                case Dead:
                    // PABLO ??!?
                    break;
                case WalkingOut:
                    // Panic
                    break;
                default:
                    Debug.Log("c'est pas normal");
                    break;
            }
        }
    }
}