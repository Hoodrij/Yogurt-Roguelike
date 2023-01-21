using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FlowJob
{
    public unsafe struct UnsafeSpan<T> where T : unmanaged, IInitialize, IDisposable
    {
        private int elementSize;
        private int length;
        private IntPtr memoryPointer;

        public UnsafeSpan(int length)
        {
            this.length = length < 4 ? 4 : length;
            elementSize = sizeof(T);
            memoryPointer = Marshal.AllocHGlobal(this.length * elementSize);

            for (int i = 0; i < length; i++)
            {
                Get(i)->Initialize();
            }
        }

        public T* this[int index] => Get(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T* Get(int index)
        {
            if (index >= length)
            {
                length <<= 1;
                memoryPointer = Marshal.ReAllocHGlobal(memoryPointer, (IntPtr)(length * elementSize));
                
                for (int i = length >> 1; i < length; i++)
                {
                    Get(i)->Initialize();
                }
            }
            return (T*) (memoryPointer + index * elementSize);
        }

        public void Dispose()
        {
            for (int i = 0; i < length; i++)
            {
                Get(i)->Dispose();
            }
            
            Marshal.FreeHGlobal(memoryPointer);
            memoryPointer = IntPtr.Zero;
            length = 0;
        }
    }

    public interface IInitialize
    {
        void Initialize();
    }
}