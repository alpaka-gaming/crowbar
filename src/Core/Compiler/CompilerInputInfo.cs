using Core.Enums;

namespace Core
{
    public class CompilerInputInfo
    {
        public string compilerPathFileName {get;set;}
        public string compilerOptions {get;set;}
        public string gamePathFileName {get;set;}
        public string qcPathFileName {get;set;}
        public string customModelFolder {get;set;}
        public InputOptions theCompileMode {get;set;}
        public string modelAbsolutePathFileName {get;set;}
        public string result {get;set;}
    }
}
