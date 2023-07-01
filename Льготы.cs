using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class Льготы
    {
        public Льготы()
        {
            КлиентыЛьготыs = new HashSet<КлиентыЛьготы>();
        }

        public int IdЛьготы { get; set; }
        public string НазваниеЛьготы { get; set; }

        public virtual ICollection<КлиентыЛьготы> КлиентыЛьготыs { get; set; }
    }
}
