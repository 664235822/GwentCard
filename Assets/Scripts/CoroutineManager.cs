using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentCard
{
    public class CoroutineManager : Singleton<CoroutineManager>
    {
        Queue queue = new Queue();
        IEnumerator current = null;
        bool isFinish = true;

        private void FixedUpdate()
        {
            DoTask();
        }

        public void AddTask(IEnumerator enumerator)
        {
            queue.Enqueue(enumerator);
        }

        void DoTask()
        {
            if (queue.Count == 0 && current == null)
                return;

            if (current == null)
                current = queue.Dequeue() as IEnumerator;

            if (isFinish == true)
            {
                StartCoroutine(current);
                isFinish = false;
            }
        }

        public void Finish()
        {
            current = null;
            isFinish = true;
        }

        public bool GetFinish()
        {
            return isFinish;
        }
    }
}