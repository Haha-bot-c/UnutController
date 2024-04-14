using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class BaseWarehouse : MonoBehaviour
{
    [SerializeField] private BaseManager _base;
    [SerializeField] private Transform _pathPoint;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            MoveResourceToPoint(other.transform);
            _base.AddCollectedResource(resource);
        }
    }

    private void MoveResourceToPoint(Transform resourceTransform)
    {
        _audioSource.Play();
        resourceTransform.DOMove(_pathPoint.position, 1f);
    }
}

