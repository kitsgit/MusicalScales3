using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MusicalScales3
{
    abstract class AbsCollection
    {
        public string key { get; set; }
        public string name { get; set; }
        public List<Scale> scaleCollection = new List<Scale>();
    }

    class Collection : AbsCollection
    {

        //public Collection(string name, string key)
        //{
        //    this.name = name;
        //    this.key = key;

        //}
        public static string path = ((Directory.GetCurrentDirectory()).Replace("\\MusicalScales3\\bin\\Debug", "")) + "\\Scales\\vector.txt";
        public static Hashtable scalehash = null;
        public static List<Collection> getCollection()//(List<Collection> list_coll)
        {
            List<Collection> list_coll = new List<Collection>();
            //string path = (Directory.GetCurrentDirectory());
            //path = path.Replace("\\MusicalScales3\\bin\\Debug", "");
            //path = path + "\\Scales\\vector.txt";
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            //Hashtable scalehash = null;
            scalehash = (Hashtable)formatter.Deserialize(fs);
            fs.Close();
            if (scalehash.ContainsKey("test"))
            {
                scalehash.Remove("test");
            }
            fs.Close();
            Console.WriteLine("The available scales are : \n");
            foreach (string key in scalehash.Keys)
            {
                list_coll.Add(new Collection { name = key, key = scalehash[key].ToString() });
            }
            
            return list_coll;
        }
        public static void printCollection(List<Collection> list_coll)
        {
            foreach (Collection coll in list_coll)
            {
                Console.WriteLine(coll.name + " : " + coll.key);
            }
        }
        
        public static List<Collection> addScale(List<Collection> list_coll)
        {
            string name=Collection.Ask("Enter the name : ");
            string key = Collection.Ask("Enter the key : ");
            if (list_coll.Any(x => x.name == name))
            {
                Console.WriteLine("A scale with that name already exists");
                return list_coll;
            }
            else
            {
                list_coll.Add(new Collection { name = name, key = key });
                scalehash.Add(name, key);
                return list_coll;
            }
        }

        public bool checkScale(List<Collection> list_coll, string input_scale)
        {
            if (list_coll.Any(x => x.name == input_scale))
            {
                // Collection col = list_coll.Find(x => x.name == input_scale);
                int q2 = 1;
                string choice;
                while (q2 == 1)
                {
                    string root=Collection.Ask("Enter the root : ");
                    root = root.ToUpper();
                    if (root == "ALL")
                    {
                        Scale sc = new Scale(this.key);
                        sc.printAll();
                    }
                    else
                    {
                        if (this.scaleCollection.Any(x => x.key == this.key && x.root == root))
                        {
                            Scale sc = this.scaleCollection.Find(x => x.key == this.key && x.root == root);
                            sc.getScale();
                            //Console.WriteLine("Scale already exists and is reused");
                        }
                        
                        else
                        {
                            Scale sc = new Scale(this.key, root);
                            this.scaleCollection.Add(sc);
                            sc.getScale();
                            //Console.WriteLine("Scale does not exist and is created");
                        }
                        
                    }
                    choice = Collection.Ask("Change scale? y/n");
                    if (choice == "y")
                    {
                        q2 = 0;
                        break;
                    }
                    //else if (choice == "exit")
                    //{
                    //    break;
                    //}

                }
                return true;
            }
            else
                return false;
        }
        public static void serializeHash()
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, scalehash);
            fs.Close();
            //foreach (string name in scalehash.Keys)
            //{
            //    Console.WriteLine(String.Format("{0}: {1}", name, scalehash[name]));
            //}
        }

        static public string Ask(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
    }

    class Scale : AbsCollection
{
        public string root;
        public static string[] notes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        public List<Note> noteCollection = new List<Note>();
        public Scale()
        {

        }
        public Scale(string key)
        {
            this.key = key;
        }
        public Scale(string key, string root)
        {
            this.key = key;
            this.root = root;
        }
        public void getScale()//(string key, string root)
        {
            if (!notes.Contains(this.root))
            {
                Console.WriteLine("Not valid");
            }
            else if(!this.noteCollection.Any())
            {
                int position = Array.IndexOf(notes, this.root);
                int[] seq = new int[30];
                for (int i = 0; i < this.key.Length; i++)
                {

                    seq[i] = Int32.Parse(this.key[i].ToString());
                    int p = position % notes.Length;
                    this.noteCollection.Add(new Note(p));
                    //Note n = new Note(p);
                    //n.print();
                    position += seq[i];
                }

                
            }
            this.printScale(noteCollection);
        }
        public void printScale(List<Note> notes)
        {
            foreach (Note note in notes)
            {
                note.print();
            }
            Console.WriteLine("\n");
        }

        public void printAll()
        {
            foreach (string r in notes)
            {
                this.scaleCollection.Add(new Scale(this.key, r));
            }
            foreach (Scale sc in scaleCollection)
            {
                sc.getScale();
            }
        }
    }

    class Note : Scale
    {
        public string note_name;
        public int frequency;
        //public int octave;
        //public int sustain;

        //public int position;
        public Note(int pos)
        {
            //this.position = pos;
            this.note_name = Scale.notes[pos];
            int sf = 261;
            for (int i = 0; i < pos; i++)
            {
                sf += 16;
                //Console.WriteLine(i);

            }
            this.frequency = sf;
            //Console.WriteLine(this.frequency);
        }
        public void print()
        {
            Console.Write(this.note_name+" ");
            Console.Beep(this.frequency, 200);
        }
    }
}
