using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class cell
    {
        public string state { get; set; }
        
        public cell(string state)
        {
            this.state = state;
        }
    }
}
