using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalScales3
{
    class Program
    {
        static void Main(string[] args)
        {

           //Collection coll = new Collection();
            List<Collection> list_coll = Collection.getCollection();
            //Collection.getCollection(list_coll);
            int q = 1;
            while (q == 1)
            {
                Console.WriteLine("The available scales are : \n");
                Collection.printCollection(list_coll);
                string input_scale = Collection.Ask("Enter the scale / search : ");
                Collection col = new Collection();
                
                if (list_coll.Any(x => x.name == input_scale))
                {
                    col = list_coll.Find(x => x.name == input_scale);
                    bool scale_exists = col.checkScale(list_coll, input_scale);
                }
                else if(input_scale=="exit")
                {
                    break;
                }
                else//if (!scale_exists)
                
                {
                    string ch = Collection.Ask("Scale does not exist. Do you want to add? y/n");
                    if (ch == "y")
                    {
                        list_coll = Collection.addScale(list_coll);
                    }
                    else if(ch=="exit")
                    {
                        break;
                    }
                }
            }
            Collection.serializeHash();
            Console.Read();
        }

        
    }
    
}
