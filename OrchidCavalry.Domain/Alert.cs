using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidCavalry.Models
{
    public class Alert
    {
        public Alert(string header, string message)
        {
            this.Header = header;
            this.Message = message;
        }

        public string Header { get; set; }
        public string Message { get; set; }
    }
}