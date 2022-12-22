using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _items = default;
    [SerializeField]
    private int _delayTime = 1000;
    public async void Generate()
    {
        await Task.Delay(_delayTime);
        // ©•ª‚ÌˆÊ’u‚©‚çItem‚ğ¶¬‚·‚éB
        var index = Random.Range(0, _items.Length);
        Instantiate(_items[index], transform.position, Quaternion.identity);
    }
    private void Start()
    {
        // Å‰‚ÌˆêŒÂ‚ğ¶¬‚·‚é
        Generate();
    }
}
