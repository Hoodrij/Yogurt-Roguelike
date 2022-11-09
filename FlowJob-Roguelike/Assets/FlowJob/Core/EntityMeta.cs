using System;
using System.Runtime.InteropServices;

namespace FlowJob
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EntityMeta : IInitialize
    {
        private const int SIZE = sizeof(ushort);
        
        internal bool IsAlive;
        internal int Age;
        internal Mask ComponentsMask;

        private byte groupsLength;
        internal byte GroupsAmount;
        internal int* Groups;

        public void Initialize()
        {
            groupsLength = 4;
            Groups = (int*) Marshal.AllocHGlobal(groupsLength * SIZE);
            GroupsAmount = 0;
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal((IntPtr) Groups);
        }

        public void AddGroup(int groupID)
        {
            if (groupsLength == GroupsAmount)
            {
                groupsLength = (byte) (GroupsAmount << 1);
                Groups = (int*) Marshal.ReAllocHGlobal((IntPtr) Groups, (IntPtr) (groupsLength * sizeof(ushort)));
            }
        
            Groups[GroupsAmount++] = groupID;
        }
        
        public void RemoveGroup(int groupID)
        {
            for (int i = 0; i <= GroupsAmount; i++)
            {
                if (Groups[i] == groupID)
                {
                    for (int j = i; j < GroupsAmount; ++j)
                    {
                        Groups[j] = Groups[j + 1];
                    }
        
                    GroupsAmount--;
                    break;
                }
            }
        }
    }
}