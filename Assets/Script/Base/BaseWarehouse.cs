using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]
public class BaseWarehouse : MonoBehaviour
{
    [SerializeField] private UnitDispatcher _base;

    private AudioSource _audioSource;
    private Queue<Resource> _collectedResource = new Queue<Resource>();

    public event Action<int> OnCollectedResourcesChanged;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource) && _collectedResource.Contains(resource) == false)
        {
            PlayAudioWhenResourceMoved();
            EnqueueCollectedResource(resource);   
        }
    }

    private void PlayAudioWhenResourceMoved() 
    {
        _audioSource.Play();
    }

    private void EnqueueCollectedResource(Resource resource) 
    {
        _collectedResource.Enqueue(resource);
        OnCollectedResourcesChanged?.Invoke(_collectedResource.Count);
    }
}
