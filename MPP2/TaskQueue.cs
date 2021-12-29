using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FirstTask
{
    public class TaskQueue
    {
        private Thread[] threadPool;
        public delegate void taskDelegate();
        private Queue<taskDelegate> TaskDelegateQueue = new Queue<taskDelegate>();

        public bool keepRunning = true;

        public TaskQueue(int quantityThread)
        {
            this.threadPool = new Thread[quantityThread];
            for (int i = 0; i < quantityThread; i++)
            {
                threadPool[i] = new Thread(ThreadWork);
                threadPool[i].Start();
            }
        }
        public void EnqueueTask(taskDelegate task)
        {
            lock (TaskDelegateQueue)
            {
                TaskDelegateQueue.Enqueue(task);
            }
        }

        public void ThreadWork()
        {
            taskDelegate task;
            while (keepRunning)
            {
                try
                {
                    while (TaskDelegateQueue.Count > 0)
                    {
                        task = null;
                        lock (TaskDelegateQueue)
                        {
                            task = TaskDelegateQueue.Dequeue();
                        }
                        if (task != null)
                        {
                            task.Invoke();
                        }
                    }
                }
                catch { }
            }
        }

        public void Finish()
        {
            keepRunning = false;
        }
    }
}