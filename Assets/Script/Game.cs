using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script
{
    public class Game : MonoBehaviour
    {
        public GameObject patientPrefab;
        private int _waveNumber = 0;
        private int _remainingPatients = 0;
        private List<Patient> _patients = new();
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GeneratePatients();
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePatients();
            if (_remainingPatients == 0)
            {
                _waveNumber++;
                GeneratePatients();
            }
        }

        void GeneratePatients()
        {
            for (int i = 0; i < 4 + _waveNumber; i++)
            {
                var newPatient = Instantiate(patientPrefab, new Vector2(i*2, 0), Quaternion.identity);
                _patients.Add(newPatient.GetComponent<Patient>());
            }
            _remainingPatients = _patients.Count;
        }

        void UpdatePatients()
        {
            foreach (var patient in _patients.ToList())
            {
                switch (patient.state)
                {
                    case PatientStates.WalkingOut:
                        break;
                    case PatientStates.Exited:
                        _remainingPatients--;
                        Destroy(patient.gameObject);
                        _patients.Remove(patient);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
