class Program
    {
        static void Main(string[] args)
        {
            //int blockSize = 500000;
            int blockSize = 1000000;
            int transactions = 12;
            int[] size = { 57247, 98732, 134928, 77275, 29240, 15440, 70820, 139603, 63718, 143807, 190457, 40572 };
            double[] fee = { 0.0887, 0.1856, 0.2307, 0.1522, 0.0532, 0.0250, 0.1409, 0.2541, 0.1147, 0.2660, 0.2933, 0.0686 };
            double bonus = 12.5;

            int tempSize = 0;
            double tempProfit = 0.0;

            //sort both the array's in ascending order by size
            for (int i = 0; i <= size.Length - 1; i++)
            {
                for (int j = i + 1; j < size.Length; j++)
                {
                    if (size[i] > size[j])
                    {
                        tempSize = size[i];
                        size[i] = size[j];
                        size[j] = tempSize;

                        tempProfit = fee[i];
                        fee[i] = fee[j];
                        fee[j] = tempProfit;
                    }
                }
            }

            double result = CalculateMaxReward(blockSize, size, fee, transactions);

            //adding the result with bonus will return the possible maximum reward bor creating a block for the above 12 transactions
            Console.WriteLine(bonus + result);
            Console.ReadLine();
        }

        public static double CalculateMaxReward(int blockSize, int[] size, double[] fee, int transactions)
        {
            double[,] arr = new double[transactions + 1, blockSize + 1];

            for (int i = 0; i <= transactions; ++i)
            {
                for (int j = 0; j <= blockSize; ++j)
                {
                    if (i == 0 || j == 0)
                        arr[i, j] = 0;
                    else if (size[i - 1] <= j)
                        arr[i, j] = Math.Max(fee[i - 1] + arr[i - 1, j - size[i - 1]], arr[i - 1, j]);
                    else
                        arr[i, j] = arr[i - 1, j];
                }
            }
            return arr[transactions, blockSize];
        }

    }
