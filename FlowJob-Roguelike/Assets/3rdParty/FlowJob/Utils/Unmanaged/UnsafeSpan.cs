using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FlowJob
{
    public unsafe struct UnsafeSpan<T> where T : unmanaged, IInitialize
    {
        public int Length;
        private int elementSize;
        private void* memoryPointer;

        public UnsafeSpan(int length)
        {
            Length = length < 4 ? 4 : length;
            elementSize = sizeof(T);
            memoryPointer = Marshal.AllocHGlobal(Length * elementSize).ToPointer();

            for (int i = 0; i < Length; i++)
            {
                Get(i)->Initialize();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T* Get(int index)
        {
            if (index >= Length)
            {
                Length <<= 1;
                memoryPointer = Marshal.ReAllocHGlobal((IntPtr)memoryPointer, (IntPtr)(Length * elementSize)).ToPointer();
                
                for (int i = Length >> 1; i < Length; i++)
                {
                    T* initialize = (T*) ((byte*) memoryPointer + i * elementSize);
                    initialize->Initialize();
                }
            }
            return (T*) ((byte*) memoryPointer + index * elementSize);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal((IntPtr)memoryPointer);
            memoryPointer = null;
            Length = 0;
        }
    }

    public interface IInitialize
    {
        void Initialize();
    }
}