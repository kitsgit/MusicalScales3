using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalScales3
{
    abstract class AbsCollection
    {
        public string key;
        public string name;
    }

    class Collection : AbsCollection
    {

    }

    class Scale : AbsCollection
    {
        public string root;

    }

    class Note : Scale
    {
        public string nname;

        public void print()
        {

        }
    }
}
