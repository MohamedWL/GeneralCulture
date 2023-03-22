using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralCultureWeb.Models
{
    public class Question
    {
        //prop is a shortcut for a getter and setter
        public int Id { get; set; }
        public string Quest { get; set; }
        public string Answer { get; set; }
        //ctor shortcut for the constructor
        
        public Question()
        {
            
        }
    }
}
