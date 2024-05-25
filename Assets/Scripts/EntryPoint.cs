using PlayerModule;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _entryPointTransform;
    [SerializeField] private PlayerInteractor _playerInteractor;
    
    private void Start()
    {
        _playerInteractor.Move(_entryPointTransform.position);
    }
}