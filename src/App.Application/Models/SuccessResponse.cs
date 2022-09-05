using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Models
{
    public class SuccessResponse<T>
    {
        public T Data { get; set; }
    }
}
