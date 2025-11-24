using System.Collections.Generic;
using UnityEngine;

public class TriggerWithSound : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;     // Sound to play
    [SerializeField] private AudioSource audioSource; // AudioSource to play through

    // Keep track of objects that have already triggered
    private HashSet<GameObject> triggeredObjects = new HashSet<GameObject>();

    private void Awake()
    {
        // Try to find or create an AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f; // Make it 3D
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger once per object
        if (triggeredObjects.Contains(other.gameObject))
            return;

        triggeredObjects.Add(other.gameObject);

        // Play sound if available
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}