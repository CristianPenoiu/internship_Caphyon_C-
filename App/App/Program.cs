namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Citirea datelor din fisier
            string[] inputLines = File.ReadAllLines("input.txt");

            // Inițializarea stackurilor
            List<Stack<char>> stacks = new List<Stack<char>>();

            // Impartirea cutiilor în rânduri.
            int numberOfRows = 0;
            int i = 0;
            while (!string.IsNullOrEmpty(inputLines[i]))
            {
                numberOfRows++;
                i++;
            }

            int numberOfStacks = int.Parse(inputLines[i - 1].Trim().Split(' ').Last());

            // Calcularea poziției fiecărei cutii în rânduri.
            int[] positions = Enumerable.Range(0, numberOfStacks)
                .Select(s => (s * 4) + 1)
                .ToArray();

            // Pentru fiecare stack, se citesc cutiile de sus în jos.
            for (int stack = 0; stack < numberOfStacks; stack++)
            {
                Stack<char> crates = new Stack<char>();
                for (int row = numberOfRows - 2; row >= 0; row--)
                {
                    string crate = inputLines[row].Substring(positions[stack], 1);

                    if (!string.IsNullOrEmpty(crate.Trim()))
                    {
                        crates.Push(crate.Trim()[0]);
                    }
                }

                stacks.Add(crates);
            }

            // Crearea instructiunilor de mutare
            List<Tuple<int, int, int>> steps = new List<Tuple<int, int, int>>();
            for (; i < inputLines.Length; i++)
            {
                if (!inputLines[i].StartsWith("move"))
                {
                    continue;
                }

                string[] parts = inputLines[i].Split(' ');

                int quantity = int.Parse(parts[1]);

                int fromStack = int.Parse(parts[3]) - 1;
                int toStack = int.Parse(parts[5]) - 1;

                steps.Add(Tuple.Create(quantity, fromStack, toStack));
            }

            // Aplicarea instrucțiunilor de mutare.
            foreach (Tuple<int, int, int> step in steps)
            {
                for (i = 0; i < step.Item1; i++)
                {
                    if (!stacks[step.Item2].Any())
                    {
                        continue;
                    }

                    char crate = stacks[step.Item2].Pop();
                    stacks[step.Item3].Push(crate);
                }
            }

            // Afisarea cutiei de sus pentru fiecare stack
            foreach (Stack<char> stack in stacks)
            {
                if (!stack.Any())
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(stack.Peek());
                }
            }

            Console.WriteLine();
        }
    }
}