using Educat.Logic.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic.Pages
{
    public class Feed<T>
    {
        public Feed()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }

        public void Init(int total, int skip, List<T> items)
        {
            this.Skip = skip;
            this.Total = total;
            this.Items = items;
        }
    }

}
