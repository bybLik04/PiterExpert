using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class Клиенты
    {
        public Клиенты()
        {
            Договорs = new HashSet<Договор>();
            КлиентыЛьготыs = new HashSet<КлиентыЛьготы>();
            СистемаНалогооблаженияКлиентs = new HashSet<СистемаНалогооблаженияКлиент>();
        }

        public int IdКлиента { get; set; }
        public string Название { get; set; }
        public long Инн { get; set; }
        public string Адрес { get; set; }

        public virtual ICollection<Договор> Договорs { get; set; }
        public virtual ICollection<КлиентыЛьготы> КлиентыЛьготыs { get; set; }
        public virtual ICollection<СистемаНалогооблаженияКлиент> СистемаНалогооблаженияКлиентs { get; set; }
    }
}
