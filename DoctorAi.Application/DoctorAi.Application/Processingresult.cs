using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Application
{
    public class Processingresult<T>
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
    public class Processingresult
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
   
    }
}

