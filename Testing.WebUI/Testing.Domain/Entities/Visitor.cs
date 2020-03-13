using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Domain.Entities
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        public string ExceotionMessage { get; set; }
        public string Ip { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime Date { get; set; }
    }
}
