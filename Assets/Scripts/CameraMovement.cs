using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Path Settings")]
    [SerializeField] private Transform[] pathPoints; // Lista punktów ścieżki, które kamera ma odwiedzać
    [SerializeField] private float moveSpeed = 5f;   // Prędkość poruszania się kamery
    [SerializeField] private float reachThreshold = 0.1f; // Minimalna odległość, by uznać, że kamera osiągnęła punkt

    private int currentPointIndex = 0; // Indeks aktualnego punktu

    private void Update()
    {
        if (pathPoints.Length == 0) return; // Jeśli brak punktów, nie rób nic
        // Pobierz aktualny cel
        Transform targetPoint = pathPoints[currentPointIndex];

        // Ruch kamery w kierunku celu
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Sprawdzenie, czy kamera osiągnęła punkt
        if (Vector3.Distance(transform.position, targetPoint.position) <= reachThreshold)
        {   
            currentPointIndex++; // Przejdź do następnego punktu

            // Jeśli osiągnięto ostatni punkt, rozpocznij od nowa (opcjonalne)
            if (currentPointIndex >= pathPoints.Length)
            {
                currentPointIndex = 0; // Powrót do początku ścieżki
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Rysowanie ścieżki w edytorze Unity
        if (pathPoints == null || pathPoints.Length == 0) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            if (pathPoints[i] != null && pathPoints[i + 1] != null)
            {
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
                Gizmos.DrawSphere(pathPoints[i].position, 0.2f);
            }
        }

        // Rysowanie ostatniego punktu
        if (pathPoints[pathPoints.Length - 1] != null)
        {
            Gizmos.DrawSphere(pathPoints[pathPoints.Length - 1].position, 0.2f);
        }
    }
}
