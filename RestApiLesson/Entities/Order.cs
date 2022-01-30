using RestApiLesson.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiLesson.Entities
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Status { get; internal set; }
        public string[] Products { get; set; }
        public decimal Price { get; set; }
        public string PostamatNumber { get; internal set; }
        public string TelephoneNumber { get; set; }
        public string ReceiverFullName { get; set; }
    }
}
