using System.Collections;
using UnityEngine;

namespace RTS.Infrastucture
{
    public class CoroutineRunner : MonoBehaviour
    {
        public new void StartCoroutine(IEnumerator coroutine)
             => base.StartCoroutine(coroutine);
    }
}