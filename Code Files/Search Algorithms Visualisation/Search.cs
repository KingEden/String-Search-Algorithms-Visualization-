using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Search_Algorithms_Visualisation
{
    public partial class Search : Form
    {
        //matching words
        private void showMatch(string text, string expr)
        {
            Console.WriteLine("The Expression: " + expr);
            MatchCollection mc = Regex.Matches(text, expr);

            foreach (Match m in mc)
            {
                richTextBox1.Text = m.ToString();
            }
        }
        //Swap
        public String swap(String a,
                            int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }
       //Get File Name
        public string getFileName(string str, char spearator)
        {
            string temp = "";
            for (int i = 0; i < strFindLength(str); i++)
            {
                if (str[i] != spearator)
                {
                    temp += str[i];
                }
                else
                {
                    //Console.WriteLine($"{temp} ");
                    temp = "";
                }
            }
            return temp;
        }
        //Get Length
        static int strFindLength(string str)
        {
            str += "\0";
            int i = 0;
            while (str[i] != '\0')
            {
                i++;
            }
            return i;
        }
        //-----------------------------------//
        //KMP Algorithm for Pattern Searching//
        //-----------------------------------//

        /*
        public void KMPSearch(string pat, string txt)
        {
            int M = pat.Length;
            int N = txt.Length;

            // create lps[] that will hold the longest
            // prefix suffix values for pattern
            int[] lps = new int[M];
            int j = 0; // index for pat[]

            // Preprocess the pattern (calculate lps[]
            // array)
            computeLPSArray(pat, M, lps);

            int i = 0; // index for txt[]
            while ((N - i) >= (M - j))
            {
                if (pat[j] == txt[i])
                {
                    j++;
                    i++;
                }
                if (j == M)
                {
                  // Console.Write("Found pattern "
                           //    + "at index " + (i - j));
                    j = lps[j - 1];
                    String B="";
                    
                    for (int O = 0; O < M; O++)
                    {
                          B += txt[(i - j) + O];
                        
                            
                    }
                    richTextBox1.Text= B;
                }

                // mismatch after j matches
                else if (i < N && pat[j] != txt[i])
                {
                    // Do not match lps[0..lps[j-1]] characters,
                    // they will match anyway
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
        }



        public void computeLPSArray(string pat, int M, int[] lps)
        {
            // length of the previous longest prefix suffix
            int len = 0;
            int i = 1;
            lps[0] = 0; // lps[0] is always 0

            // the loop calculates lps[i] for i = 1 to M-1
            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else // (pat[i] != pat[len])
                {
                    // This is tricky. Consider the example.
                    // AAACAAAA and i = 7. The idea is similar
                    // to search step.
                    if (len != 0)
                    {
                        len = lps[len - 1];

                        // Also, note that we do not increment
                        // i here
                    }
                    else // if (len == 0)
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }
        */
        public bool KMPSearch(string txt, string pat)
        {
            int M = pat.Length;
            int N = txt.Length;
            bool c = false;
            int[] lps = new int[M];
            int j = 0; // index for pat[]

            computeLPSArray(pat, M, lps);

            int i = 0; // index for txt[]
            while ((N - i) >= (M - j))
            {
                if (pat[j] == txt[i])
                {
                    j++;
                    i++;
                }
                if (j == M)
                {
                    //Console.Write("Found pattern " + "at index " + (i - j));
                    int z = i - j;
                    string y = z.ToString() + "\t";
                    textBox2.Text = y;
                    j = lps[j - 1];
                    return true;
                }
                else if (i < N && pat[j] != txt[i])
                {
                    if (j != 0)
                    { j = lps[j - 1]; }
                    else
                    { i = i + 1; }
                }
            }
            return false; ;
        }

        void computeLPSArray(string pat, int M, int[] lps)
        {
            int len = 0;
            int i = 1;
            lps[0] = 0;

            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else // (pat[i] != pat[len])
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else // if (len == 0)
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }



        //Exact Match Function
        public bool ExactMatch(string input, string match)
        {

            return Regex.IsMatch(input, string.Format(@"\b{0}\b", Regex.Escape(match)));

        }

        //-----------------------------------//
        //BF-Algorithm for Pattern Searching //
        //-----------------------------------//
        /*  
         public void search(String txt, String pat)
           {
               int M = pat.Length;
               int N = txt.Length;


               for (int i = 0; i <= N - M; i++)
               {
                   int j;


                   for (j = 0; j < M; j++)
                       if (txt[i + j] != pat[j])
                           break;
                   string B="";
                   // if pat[0...M-1] = txt[i, i+1, ...i+M-1]
                   int count = 0;
                   if (j == M)
                   {
                       count++;
                       for (int O = 0; O < M; O++)
                       {
                           B += txt[(i) + O];


                       }
                       richTextBox1.Text = B;

                   }


               }
           }

   */
        public bool search(String txt, String pat)
        {
            int M = pat.Length;
            int N = txt.Length;

            for (int i = 0; i <= N - M; i++)
            {
                int j;

                for (j = 0; j < M; j++)
                    if (txt[i + j] != pat[j])
                        break;
                string B = "";
                if (j == M)
                {
                    for (int O = 0; O < M; O++)
                    {
                        B += txt[(i) + O];


                    }
                    richTextBox1.Text = B; 
                    string y = i.ToString()+ "\t";
                    textBox2.Text = y;
                    return true;
                }
            }
            return false;
        }
        //Rabin Karp Algorithm
        public readonly static int d = 10;
        public bool RKsearch(String txt, String pat, int q)
        {
            int M = pat.Length;
            int N = txt.Length;
            int i, j;
            int p = 0; // hash value for pattern
            int t = 0; // hash value for txt
            int h = 1;

            for (i = 0; i < M - 1; i++)
                h = (h * d) % q;

            for (i = 0; i < M; i++)
            {
                p = (d * p + pat[i]) % q;
                t = (d * t + txt[i]) % q;
            }

            for (i = 0; i <= N - M; i++)
            {
                if (p == t)
                {
                    for (j = 0; j < M; j++)
                    {
                        if (txt[i + j] != pat[j])
                            break;
                    }

                    if (j == M)
                    {
                        //Console.WriteLine("Pattern found at index " + i);
                        string y=i.ToString();
                        textBox2.Text = y;
                        return true;
                       
                    }
                }

                // Calculate hash value for next window of text:
                // Remove leading digit, add trailing digit
                if (i < N - M)
                {
                    t = (d * (t - txt[i] * h) + txt[i + M]) % q;

                    // We might get negative value of t,
                    // converting it to positive
                    if (t < 0)
                        t = (t + q);
                }
            }
            return false;
        }

        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileInfo File = new FileInfo (@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#1.txt");
            if (File.Exists)
            {
                StreamReader reader = File.OpenText();
                string Read = "";
                String Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read = reader.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Forcre")
                    {
                        search(Read, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read, Find);
                    }

                }
                reader.Close();

            }
             FileInfo File2 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#2.txt");
                if (File2.Exists)
                {
                    StreamReader reader2 = File2.OpenText();
                    string Read2 = "";
                     string Find = textBox1.Text;
                    //while ((Read = reader.ReadLine()) != null)
                    {
                        Read2 = reader2.ReadToEnd();
                        if (comboBox1.Text == "Rabin-Karps")
                        {

                            // textBox1.Text = Read;
                            //search(Read, Find);
                        }
                        else if (comboBox1.Text == "Brute Forcre")
                        {
                            search(Read2, Find);
                        }
                        else if (comboBox1.Text == "Knuth-Morris-Pratt")
                        {
                            KMPSearch(Read2, Find);
                        }

                    }
                    reader2.Close();
                }
            FileInfo File3 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#3.txt");
            if (File3.Exists)
            {
                StreamReader reader3 = File3.OpenText();
                string Read3 = "";
               string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read3 = reader3.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Forcre")
                    {
                        search(Read3, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read3, Find);
                    }

                }
                reader3.Close();
            }
            FileInfo File4 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#4.txt");
            if (File4.Exists)
            {
                StreamReader reader4 = File4.OpenText();
                string Read4 = "";
                string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read4 = reader4.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Forcre")
                    {
                        search(Read4, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read4, Find);
                    }

                }
                reader4.Close();
            }
            FileInfo File5 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#5.txt");
            if (File5.Exists)
            {
                StreamReader reader5 = File5.OpenText();
                string Read5 = "";
                string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read5 = reader5.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Forcre")
                    {
                        search(Read5, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read5, Find);
                    }

                }
                reader5.Close();
            }
            FileInfo File6 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#6.txt");
            if (File6.Exists)
            {
                StreamReader reader6 = File6.OpenText();
                string Read6 = "";
                string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read6 = reader6.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Forcre")
                    {
                        search(Read6, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read6, Find);
                    }

                }
                reader6.Close();
            }
            FileInfo File7 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#7.txt");
            if (File7.Exists)
            {
                StreamReader reader7 = File7.OpenText();
                string Read7 = "";
                string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read7 = reader7.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Forcre")
                    {
                        search(Read7, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read7, Find);
                    }

                }
                reader7.Close();
            }
            FileInfo File8 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#8.txt");
            if (File8.Exists)
            {
                StreamReader reader8 = File8.OpenText();
                string Read8 = "";
                string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read8 = reader8.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Force")
                    {
                        search(Read8, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read8, Find);
                    }

                }
                reader8.Close();
            }
            FileInfo File9 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#9.txt");
            if (File9.Exists)
            {
                StreamReader reader9 = File9.OpenText();
                string Read9 = "";
                string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read9 = reader9.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Force")
                    {
                        search(Read9, Find);
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read9, Find);
                    }

                }
                reader9.Close();
            }
            FileInfo File10 = new FileInfo(@"C:\\Users\\uzair\\Downloads\\DataFiles\Research#10.txt");
            if (File10.Exists)
            {
                StreamReader reader10 = File10.OpenText();
                string Read10 = "";
                string Find = textBox1.Text;
                //while ((Read = reader.ReadLine()) != null)
                {
                    Read10 = reader10.ReadToEnd();
                    if (comboBox1.Text == "Rabin-Karps")
                    {

                        // textBox1.Text = Read;
                        //search(Read, Find);
                    }
                    else if (comboBox1.Text == "Brute Force")
                    {
                        search(Read10, Find);
                        
                        
                    }
                    else if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                        KMPSearch(Read10, Find);
                    }

                }
                reader10.Close();
            }

            string find = textBox1.Text;
            string path = "c:\\Users\\uzair\\Downloads\\DataFiles\\";
            var filelist = new List<string>();
            string[] filePaths = Directory.GetFiles(@path, "*.txt");
            int q = 101;
            int R= 0;
            foreach (string file in filePaths)
            {
                R++;
            }
            for (int i = 0; i < R; i++)
            {
                
                using (var fileStream = new FileStream(@filePaths[i], FileMode.Open, FileAccess.Read))
                {
                    var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                    string text = streamReader.ReadToEnd();
                    if (comboBox1.Text == "Brute Force")
                    {
                        if (checkBox1.Checked == true)
                        {
                            if (ExactMatch(text, find))
                            {
                                filelist.Add(filePaths[i]);
                            }
                        }
                        else if (checkBox2.Checked == true)
                        {
                            if (search(text.ToLower(), find.ToLower()))
                                filelist.Add(filePaths[i]);
                        }
                        else
                        {
                            if (search(text, find))
                                filelist.Add(filePaths[i]);
                        }
                    }
                    if (comboBox1.Text == "Knuth-Morris-Pratt")
                    {
                       
                        if (checkBox1.Checked == true)
                        {
                            if (ExactMatch(text, find))
                            {
                                filelist.Add(filePaths[i]);
                            }
                        }
                        else if (checkBox2.Checked == true)
                        {
                            if (KMPSearch(text.ToLower(), find.ToLower()))
                                filelist.Add(filePaths[i]);
                        }
                        else
                        {
                            if (KMPSearch(text, find))
                                filelist.Add(filePaths[i]);
                        }
                    }
                    if (comboBox1.Text == "Rabin-Karps")
                    {
                        
                        if (checkBox1.Checked == true)
                        {
                            if (ExactMatch(text, find))
                            {
                                filelist.Add(filePaths[i]);
                            }
                        }
                        else if (checkBox2.Checked == true)
                        {
                            if (RKsearch(text.ToLower(), find.ToLower(), q))
                                filelist.Add(filePaths[i]);
                        }
                        else
                        {
                            if (RKsearch(text, find, q))
                                filelist.Add(filePaths[i]);
                        }
                    }
                }
            }
            /*
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < len; i++)
                {
                    //Console.WriteLine(filePaths[i]);
                    using (var fileStream = new FileStream(@filePaths[i], FileMode.Open, FileAccess.Read))
                    {
                        var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                        string text = streamReader.ReadToEnd();
                        if (comboBox1.Text == "Brute Force")
                        {
                            //BFsearch();
                            if (search(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Knuth-Morris-Pratt")
                        {
                            //KMPsearch();
                            if (KMPSearch(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Rabin-Karps")
                        {
                            //RKsearch();
                            if (RKsearch(text, find, q))
                            { filelist.Add(filePaths[i]); }
                        }
                    }
                }
            }
            if (checkBox1.Checked == false)
            {
                for (int i = 0; i < len; i++)
                {
                    //Console.WriteLine(filePaths[i]);
                    using (var fileStream = new FileStream(@filePaths[i], FileMode.Open, FileAccess.Read))
                    {
                        var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                        string text = streamReader.ReadToEnd();
                        if (comboBox1.Text == "Brute Force")
                        {
                            //BFsearch();
                            if (search(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Knuth-Morris-Pratt")
                        {
                            //KMPsearch();
                            if (KMPSearch(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Rabin-Karps")
                        {
                            //RKsearch();
                            if (RKsearch(text, find, q))
                            { filelist.Add(filePaths[i]); }
                        }
                    }
                }
            }
            
            if (checkBox2.Checked == true)
            {
                for (int i = 0; i < len; i++)
                {
                    //Console.WriteLine(filePaths[i]);
                    using (var fileStream = new FileStream(@filePaths[i], FileMode.Open, FileAccess.Read))
                    {
                        var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                        string text = streamReader.ReadToEnd();
                        if (comboBox1.Text == "Brute Force")
                        {
                            //BFsearch();
                            if (search(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Knuth-Morris-Pratt")
                        {
                            //KMPsearch();
                            if (KMPSearch(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Rabin-Karps")
                        {
                            //RKsearch();
                            if (RKsearch(text, find, q))
                            { filelist.Add(filePaths[i]); }
                        }
                    }
                }
            }
            if (checkBox2.Checked == false)
            {
                find = Regex.Replace(find, @"\B[A-Z]", m => " " + m.ToString().ToLower());
                for (int i = 0; i < len; i++)
                {
                    //Console.WriteLine(filePaths[i]);
                    using (var fileStream = new FileStream(@filePaths[i], FileMode.Open, FileAccess.Read))
                    {
                        var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                        string text = streamReader.ReadToEnd();
                        if (comboBox1.Text == "Brute Force")
                        {
                            //BFsearch();
                            if (search(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Knuth-Morris-Pratt")
                        {
                            //KMPsearch();
                            if (KMPSearch(text, find))
                            { filelist.Add(filePaths[i]); }
                        }
                        if (comboBox1.Text == "Rabin-Karps")
                        {
                            //RKsearch();
                            if (RKsearch(text, find, q))
                            { filelist.Add(filePaths[i]); }
                        }
                    }
                }
                find = Regex.Replace(find, @"\B[A-Z]", m => " " + m.ToString().ToUpper());
                for (int i = 0; i < len; i++)
                {
                    //Console.WriteLine(filePaths[i]);
                    using (var fileStream = new FileStream(@filePaths[i], FileMode.Open, FileAccess.Read))
                    {
                        var streamReader = new StreamReader(fileStream, Encoding.UTF8);
                        string text = streamReader.ReadToEnd();
                        if (comboBox1.Text == "Brute Force")
                        {
                            //BFsearch();
                            if (search(text, find))
                            {
                                filelist.Add(filePaths[i]);
                                richTextBox1.Text = find;
                            }
                        }
                        if (comboBox1.Text == "Knuth-Morris-Pratt")
                        {
                            //KMPsearch();
                            if (KMPSearch(text, find))
                            {
                                filelist.Add(filePaths[i]);
                                richTextBox1.Text = find;
                            }
                        }
                        if (comboBox1.Text == "Rabin-Karps")
                        {
                            //RKsearch();
                            if (RKsearch(text, find, q))
                            {
                                filelist.Add(filePaths[i]);
                                richTextBox1.Text = find;
                            }
                        }
                    }
                }
            }
            */

            filelist.ForEach(Console.WriteLine);
            //filelist.ForEach(Replace($"{path}", ""));
            for (int i = 0; i < filelist.Count; i++)
            {
                string res = getFileName(filelist[i], '\\');
                filelist[i] = res;
            }
            Console.Write($"\n\tFiles with the word \"{find}\": \n");
            string output = string.Join(", ", filelist);
            // Console.Write(output);
            // Console.Write(output);
            string temmm = output + "\0";
            richTextBox2.Text = temmm;


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
