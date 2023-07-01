using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class КлиентыЛьготы
    {
        public int IdКлиента { get; set; }
        public int IdЛьготы { get; set; }

        public virtual Клиенты IdКлиентаNavigation { get; set; }
        public virtual Льготы IdЛьготыNavigation { get; set; }
    }
}
