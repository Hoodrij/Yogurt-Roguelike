using System;

namespace FlowJob
{
    public class ManagedMetasList
    {
        private EntityManagedMeta[] array;

        public ManagedMetasList(int capacity)
        {
            array = new EntityManagedMeta[capacity];

            for (int index = 0; index < array.Length; index++)
            {
                array[index].Initialize();
            }
        }

        public ref EntityManagedMeta Get(int index)
        {
            if (index >= array.Length)
            {
                int newSize = array.Length << 1;
                Array.Resize(ref array, newSize);

                for (int i = array.Length >> 1; i < newSize; i++)
                {
                    array[i].Initialize();
                }
            }
            return ref array[index];
        }

        public void Clear()
        {
            array = Array.Empty<EntityManagedMeta>();
        }
    }
}