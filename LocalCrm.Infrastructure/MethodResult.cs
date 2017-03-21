using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.Infrastructure
{
    public class MethodResult<T>
    {
        public bool Success;
        public List<string> Messages { get; }
        public T Result { get; set; }
        public MethodResult(T result)
        {
            Result = result;
            Messages = new List<string>();
            Success = true;
        }

        public void AddErrorMessage(string message)
        {
            Messages.Add(message);
            Success = false;
        }

    }
}


