using System.Collections.Generic;
using UnityEngine;


public class SimpleObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private int _initialPoolSize = 3;
    private int _maxPoolSize = 10;
    private List<T> _activeElements = new List<T>();
    protected readonly List<T> _elements = new List<T>();


    protected virtual void GenerateNewElement()
    {
    }

    public List<T> GetActiveElements => _activeElements;

    public void SetupPool(int maxCount)
    {
        _maxPoolSize = maxCount;
        for (int i = 0; i < _initialPoolSize; i++)
        {
            GenerateNewElement();
        }
    }

    public void RevertAllToPool()
    {
        foreach (var element in _activeElements)
        {
            element.gameObject.SetActive(false);
        }

        _activeElements.Clear();
    }

    public void RevertToPool(T element)
    {
        element.gameObject.SetActive(false);
        _activeElements.Remove(element);
    }

    public T GetElement()
    {
        foreach (T destination in _elements)
        {
            if (!destination.gameObject.activeInHierarchy)
            {
                destination.gameObject.SetActive(true);

                _activeElements.Add(destination);
                return destination;
            }
        }

        if (_elements.Count < _maxPoolSize)
        {
            GenerateNewElement();
            T lastDestination = _elements[_elements.Count - 1];

            lastDestination.gameObject.SetActive(true);
            _activeElements.Add(lastDestination);

            return lastDestination;
        }

        return null;
    }
}