using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataStructures
{
    /// <summary>
    /// This class stores and handles the lists which can be used to 
    /// </summary>
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class ListData
    {
        public static readonly int NLists = 10;

        private List<double>[] lists = new List<double>[NLists];

        [Description("An array of lists of the values stored in the lists.")]
        public List<double>[] Lists
        {
            get { return lists; }
            set { lists = value; }
        }

        private bool[] listEnabled = new bool[NLists];

        [Description("An array of boolean values, indicating whether the lists are enabled.")]
        public bool[] ListEnabled
        {
            get { return listEnabled; }
            set { listEnabled = value; }
        }

        private bool listLocked = false;

        [Description("Indicates whether the lists are stopped.")]
        public bool ListLocked
        {
            get { return listLocked; }
            set { listLocked = value; }
        }

        public double[] getListValues(int iterationNumber)
        {
            double[] ans = new double[NLists];

            int[] listIndecies = new int[NLists];

            for (int i = 0; i < NLists; i++)
            {
                if (lists[i]!=null && lists[i].Count != 0)
                    listIndecies[i] = (iterationNumber / iterationsPerListStep(i)) % lists[i].Count;
                else
                    listIndecies[i] = 0;
            }

            for (int i = 0; i < NLists; i++)
            {
                if ((listEnabled[i]) && (lists[i].Count > 0))
                    ans[i] = lists[i][listIndecies[i]];
                else
                    ans[i] = 0;
            }

            return ans;
        }

        /// <summary>
        /// gets the number of iteration "ticks" required to advance the list given by list ID by 1.
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        private int iterationsPerListStep(int listID)
        {
            int ans = 1;
            bool crossEnabled = false;
            for (int i = listID; i < NLists; i++)
            {
                if (listEnabled[i])
                {
                    if (crossEnabled)
                    {
                        ans = ans * lists[i].Count;
                        crossEnabled = false;
                    }
                    if (cross[i])
                        crossEnabled = true;
                }
            }

            if (ans == 0)
                ans = 1;

            return ans;
        }

        public int iterationsCount()
        {
            int ans = 1;
            bool crossEnabled = true;
            for (int i = 0; i < NLists; i++)
            {
                if (listEnabled[i])
                {
                    if (crossEnabled)
                    {
                        ans = ans * lists[i].Count;
                        crossEnabled = false;
                    }
                    if (cross[i])
                        crossEnabled = true;
                }
            }
            return ans;
        }


        /// <summary>
        /// stores the locations of the "X" 's. The final item in the array is unused.
        /// </summary>
        private bool[] cross = new bool[NLists];

        [Description("Stores the location of the \"X\" and \",\" list separators.")]
        public bool[] Cross
        {
            get { return cross; }
            set { cross = value; }
        }

        public ListData()
        {      
            for (int i=0; i<NLists; i++) 
            {
                lists[i] = new List<double>();
            }

        }
    }
}
