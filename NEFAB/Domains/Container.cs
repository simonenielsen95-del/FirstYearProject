using System;
using System.Collections.Generic;
using System.Text;

namespace NEFAB.Domains
{
    public class Container
    {
       
            public string ContainerNumber { get; set; }
            public int Week { get; set; }
            public int Year { get; set; }

            public Container(string containerNumber)
            {
                ContainerNumber = containerNumber;
            }
        
    }
}
