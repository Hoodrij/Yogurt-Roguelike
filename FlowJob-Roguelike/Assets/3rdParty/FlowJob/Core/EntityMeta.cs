using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FlowJob
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EntityMeta : IInitialize, IDisposable
    {
        internal bool IsAlive;
        internal int Age;
        internal Mask ComponentsMask;

        internal byte GroupsAmount;
        internal UnsafeSpan<GroupId> Groups;

        public void Initialize()
        {
            GroupsAmount = 0;
            Groups = new(4);
        }

        public void Dispose()
        {
            Groups.Dispose();
        }

        public void AddGroup(int groupID)
        {
            Groups[GroupsAmount++]->Id = groupID;
        }
        
        public void RemoveGroup(int groupID)
        {
            for (int i = 0; i <= GroupsAmount; i++)
            {
                if (Groups[i]->Id == groupID)
                {
                    for (int j = i; j < GroupsAmount; ++j)
                    {
                        Groups[j]->Id = Groups[j + 1]->Id;
                    }
        
                    GroupsAmount--;
                    break;
                }
            }
        }

        public struct GroupId : IInitialize, IDisposable
        {
            public int Id;
            
            public void Initialize() { }
            public void Dispose() { }
        }
    }
}