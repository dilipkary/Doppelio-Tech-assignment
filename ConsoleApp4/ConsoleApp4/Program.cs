using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Project
    {
        // No.of vertices
        private int V;

        // Adjacency List as ArrayList
        // of ArrayList's
        private List<List<int>> adj;

        // Constructor
        Project(int v)
        {
            V = v;
            adj = new List<List<int>>(v);
            for (int i = 0; i < v; i++)
                adj.Add(new List<int>());
        }

        // Function to add an edge into the graph
        public void AddEdge(int v, int w) { adj[v].Add(w); }

        // A recursive function used by ProjetSort()
        void ProjectSortUtil(int v, bool[] visited,
                                 Stack<int> stack)
        {

            // Mark the current node as visited.
            visited[v] = true;

            // Recur for all the vertices
            // adjacent to this vertex
            foreach (var vertex in adj[v])
            {
                if (!visited[vertex])
                    ProjectSortUtil(vertex, visited, stack);
            }

            // Push current vertex to
            // stack which stores result
            stack.Push(v);
        }
        // This function used by isCyclic() 
        private bool isCyclicUtil(int i, bool[] visited,
                                        bool[] recStack)
        {

            // Mark the current node as visited and 
            // part of recursion stack 
            if (recStack[i])
                return true;

            if (visited[i])
                return false;

            visited[i] = true;

            recStack[i] = true;
            List<int> children = adj[i];

            foreach (int c in children)
                if (isCyclicUtil(c, visited, recStack))
                    return true;

            recStack[i] = false;

            return false;
        }

        // Returns true if the graph contains a 
        // cycle, else false. 
        private bool isCyclic()
        {

            // Mark all the vertices as not visited and 
            // not part of recursion stack 
            bool[] visited = new bool[V];
            bool[] recStack = new bool[V];


            // Call the recursive helper function to 
            // detect cycle in different DFS trees 
            for (int i = 0; i < V; i++)
                if (isCyclicUtil(i, visited, recStack))
                    return true;

            return false;
        }

        // The function to do Topological Sort.
        // It uses recursive ProjectSortUtil()
        List<string> ProjectSort()
        {
            Stack<int> stack = new Stack<int>();

            // Mark all the vertices as not visited
            var visited = new bool[V];

            // Call the recursive helper function
            // to store Topological Sort starting
            // from all vertices one by one
            for (int i = 0; i < V; i++)
            {
                if (visited[i] == false)
                    ProjectSortUtil(i, visited, stack);
            }

            List<string> proj_l = new List<string>();
            // Process contents of stack
            foreach (var vertex in stack)
            {
                proj_l.Add("P" + (vertex + 1).ToString());
            }
            proj_l.Reverse();
            return proj_l;
        }

        static string[] ReadInput(string filepath)
        {

            string[] lines = { };
            if (File.Exists(filepath))
            {
                lines = File.ReadAllLines(filepath);
                return lines;
            }
            else
            {
                throw new FileNotFoundException("Check for input file present at location " + filepath);
            }
        }

        static void Main(string[] args)
        {

            int counter = 0;

            string[] proj_list;
            string[] lines;
            string[] p_pair;
            string[] dep_proj;
            string dependency;

            // Refular experession for checking input Project list 
            Regex regex = new Regex(@"P+[0-9]");

            List<string> proj_result;

            //get base durectory of project
            string _BaseDirectory = Directory.GetCurrentDirectory();

            //get path of the file input by 2 step up 
            string root = Path.GetFullPath(Path.Combine(_BaseDirectory, @"..\..\..\"));

            //file path
            string fileName = new FileInfo(Path.Combine(root, "input.txt")).ToString();

            //read Project list from first line 
            try
            {

                lines = Project.ReadInput(fileName);
                string project = lines[0];
                if (lines.Length == 0)
                {
                    throw new ArgumentException("Input is not specified in the input file.");
                }


                if (!project.Contains(","))
                {
                    throw new ArgumentException("Project list is not comma seperated");
                }

                //convert the list into list of projects
                proj_list = project.Split(',');


                foreach (string s in proj_list)
                {
                    Match match = regex.Match(s);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Project list is not in specified format Ex. P1,P2,P3...");
                    }
                }

                //read project depenency from line second
                dependency = lines[1];

                //remove all unwanted spaces
                dependency = dependency.Replace(" ", "");


                if (!dependency.Contains("),("))
                {
                    throw new ArgumentException("Dependencies are not given in specified format ex. (P1,P2),(P2,P3)....");
                }

                //replace seprator for the project dependenies
                dependency = dependency.Replace("),(", "#");

                //replacing remaining openeing an closing brackets
                dependency = dependency.Replace("(", "");
                dependency = dependency.Replace(")", "");

                //create project dependencies list via split 
                dep_proj = dependency.Split('#');
                Project process;


                //initialise the graph of project with number of projects.
                process = new Project(proj_list.Length);
                for (int i = 0; i < dep_proj.Length; i++)
                {
                    p_pair = dep_proj[i].Replace("P", "").Split(',');

                    // Add each dependencies into a graph
                    process.AddEdge(Convert.ToInt32(p_pair[0]) - 1, Convert.ToInt32(p_pair[1]) - 1);
                }
                if (process.isCyclic())
                {
                    throw new ArgumentException("There is a Cycle exists in given dependencies");
                }
                // Get Tropological Sort Project list
                proj_result = process.ProjectSort();
                Console.Write("Projects list is ");
                Console.WriteLine(lines[0]);
                Console.Write("Project dependencies");
                Console.WriteLine(lines[1]);
                Console.Write("Order of the Project Build is ");
                foreach (string s in proj_result)
                {
                    counter++;
                    Console.Write(s);
                    if (counter < proj_result.Count())
                    {
                        Console.Write(">>");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
