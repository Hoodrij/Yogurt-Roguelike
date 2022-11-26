using System;
using System.Runtime.CompilerServices;

namespace FlowJob
{
    public unsafe struct MemoryPool<T> where T : unmanaged, IInitialize
    {
        public int Length;
        private int elementSize;
        private void* memory;

        public MemoryPool(int length)
        {
            Length = length < 1 ? 1 : length;
            elementSize = sizeof(T);
            memory = (void*) UnmanagedMemory.Alloc(Length * elementSize);

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
                memory = (void*) UnmanagedMemory.ReAlloc(memory, Length * elementSize);
                
                for (int i = Length >> 1; i < Length; i++)
                {
                    T* initialize = (T*) ((byte*) memory + i * elementSize);
                    initialize->Initialize();
                }
            }
            return (T*) ((byte*) memory + index * elementSize);
        }

        public void Dispose()
        {
            UnmanagedMemory.Free((IntPtr) memory);
            memory = null;
            Length = 0;
        }
    }

    public interface IInitialize
    {
        void Initialize();
    }
}