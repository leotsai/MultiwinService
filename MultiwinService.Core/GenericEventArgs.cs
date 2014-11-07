using System;

namespace MultiwinService.Core
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Data { get; set; }

        public GenericEventArgs(T data)
        {
            this.Data = data;
        } 
    }
}
