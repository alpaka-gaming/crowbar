using System.Collections.Generic;

namespace Core
{
    public class ParseMeshSmdInfo
    {
        public ParseMeshSmdInfo()
        {
            messages = new List<string>();
            boneNames = new List<string>();
        }

        public List<string> messages;
        public int lineCount;
        public int boneCount;
        public List<string> boneNames;
    }
}
