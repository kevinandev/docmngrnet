using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bean
{

    public class FunctionBean
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

    }
}