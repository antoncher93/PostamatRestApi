using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiLesson.Entities
{
    [Serializable]
    public class Postamat
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Adress { get; set; }
        public bool IsWorking { get; set; }
    }
}
