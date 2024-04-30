namespace Lab8;

public class Grid
{
    int[,] map;
    int[,] roomMap;
    int[,] maelstromMap;
    int[,] amoroksMap;
    string input = "";
    int a = 0;
    int b = 0;
    int yaxis;
    int xaxis;
    bool fountainRoom;
    bool fountainOn;
    int arrow = 5;
    bool arrowBool;
    Random random = new Random();
    public char LocationChar(int location)
    {
        if (location == 1) // players location
            return 'X';
        else
            return ' ';
    }

    public void SelectLevel()
    {
        System.Console.WriteLine("What Size Would You Like?");
        System.Console.WriteLine("[Small] [Medium] [Large]");
        try
        {
            input = Console.ReadLine();
        }
        catch (Exception)
        {
            System.Console.WriteLine("What you entered was inalid try again");
            SelectLevel();
        }

        if (input == "Small")
        {
            GenerateMap(4, 4);
            roomMap[0, 2] = 3; //Fountain
            roomMap[3, 2] = 5; //Pit
            roomMap[2, 2] = 4; //Draft
            roomMap[3, 1] = 4;
            roomMap[3, 3] = 4;
            roomMap[2, 1] = 4;
            roomMap[2, 3] = 4;
            generateMaelstrom();
            generateAmarok();
            PrintMap();

        }
        else if (input == "Medium")
        {
            GenerateMap(6, 6);
            roomMap[2, 4] = 3;
            roomMap[4, 2] = 5;
            roomMap[5, 2] = 4;
            roomMap[3, 2] = 4;
            roomMap[4, 1] = 4;
            roomMap[4, 3] = 4;
            roomMap[3, 1] = 4;
            roomMap[5, 1] = 4;
            roomMap[5, 3] = 4;
            PrintMap();
        }
        else if (input == "Large")
        {
            GenerateMap(8, 8);
            roomMap[7, 5] = 3;

            PrintMap();
        }
        else
        {
            System.Console.WriteLine("Invalid Input, Try Again");
            SelectLevel();
        }
    }

    public void GenerateMap(int y, int x)
    {
        map = new int[y, x];
        roomMap = new int[y, x];
        maelstromMap = new int[y, x];
        amoroksMap = new int[y, x];
        yaxis = map.GetLength(0);
        xaxis = map.GetLength(1);
        map[0, 0] = 1;
        roomMap[0, 0] = 2; //Entrance
    }

    public void PrintMap()
    {
        Console.Clear();
        for (int J = 0; J < xaxis; J++)
        {
            System.Console.Write("-----");
        }
        System.Console.WriteLine();
        for (int i = 0; i < yaxis; i++)
        {
            for (int j = 0; j < xaxis; j++)
            {
                Console.Write($"| {LocationChar(map[i, j])} |");
            }
            System.Console.WriteLine();
            for (int J = 0; J < xaxis; J++)
            {
                Console.Write("-----");
            }
            Console.WriteLine();
        }

        Movement();
    }

    void Movement()
    {
        CheckRoom(roomMap[a, b]);
        checkMaelstrom(maelstromMap[a, b]);
        checkAmarok(amoroksMap[a, b]);
        Console.Write("What would you like to do: ");
        try
        {
            input = Console.ReadLine();
        }
        catch (Exception)
        {
            System.Console.WriteLine("What you entered was inalid try again");
            Movement();
        }

        if (input == "north")
        {
            if (a == 0)
            {
                System.Console.WriteLine("There is a wall there, try again");
                Movement();
            }
            else
            {
                map[a, b] = 0;
                a--;
                map[a, b] = 1;
                PrintMap();
            }
        }
        else if (input == "south")
        {
            int c = a + 1; //what if added
            if (c > yaxis - 1)
            {
                System.Console.WriteLine("There is a wall there, try again");
                Movement();
            }
            else
            {
                map[a, b] = 0;
                a++;
                map[a, b] = 1;
                PrintMap();
            }
        }
        else if (input == "east")
        {
            int c = b + 1;
            if (c > xaxis - 1)
            {
                System.Console.WriteLine("There is a wall there, try again");
                Movement();
            }
            else
            {
                map[a, b] = 0;
                b++;
                map[a, b] = 1;
                PrintMap();
            }
        }
        else if (input == "west")
        {
            int c = b + 1;
            if (b == 0)
            {
                System.Console.WriteLine("There is a wall there, try again");
                Movement();
            }
            else
            {
                map[a, b] = 0;
                b--;
                map[a, b] = 1;
                PrintMap();
            }
        }
        else if (input == "shoot north")
        {
            if (arrow > 0)
            {
                arrow--;
                arrowBool = true;
                checkAmarok(a - 1);
                arrowBool = false;
            }
        }
        else if (input == "shoot south")
        {
            if (arrow > 0)
            {
                arrow--;
                arrowBool = true;
                checkAmarok(a + 1);
                arrowBool = false;
            }
        }
        else if (input == "shoot east")
        {
            if (arrow > 0)
            {
                arrow--;
                arrowBool = true;
                checkAmarok(b + 1);
                arrowBool = false;
            }
        }
        else if (input == "shoot west")
        {
            if (arrow > 0)
            {
                arrow--;
                arrowBool = true;
                checkAmarok(b - 1);
                arrowBool = false;
            }
        }
        else if (input == "enable fountain")
        {
            if (fountainRoom)
            {
                fountainOn = true;
            }
            else
            {
                System.Console.WriteLine("There is no fountain here");
            }
            Movement();
        }
        else if (input == "leave")
        {
            if (fountainOn)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("You Turned on the Fountain and Escaped");
                Console.ResetColor();
                Environment.Exit(0);
            }
            else
            {
                System.Console.WriteLine("You cannot leave without the fountain on");
            }
        }
        else if (input == "exit program")
        {
            Environment.Exit(0);
        }
        else
        {
            System.Console.WriteLine("Invalid Input, Try Again");
            Movement();
        }
    }

    void CheckRoom(int roomValue)
    {
        if (roomValue == 3)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            fountainRoom = true;
            if (fountainOn)
            {
                System.Console.WriteLine("You hear rushing water the fountain is turned on");
            }
            else
                System.Console.WriteLine("You hear water dripping");
            Console.ResetColor();
        }
        else if (roomValue == 2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("You see Light coming from the entrance");
            Console.ResetColor();
        }
        else if (roomValue == 4)
        {
            System.Console.WriteLine("You feel a draft, a pit is nearby");
        }
        else if (roomValue == 5)
        {
            System.Console.WriteLine("You Have Died from a pit");
            Environment.Exit(0);
        }
        else
        {
            fountainRoom = false;
        }
    }

    void checkAmarok(int num)
    {
        if (num == 1)
        {
            System.Console.WriteLine("you smell stinky amarok near you");
        }
        if (num == 2)
        {
            if (arrowBool)
            {
                System.Console.WriteLine("you have killed the Amarok");
                amoroksMap[a, b] = 3;
            }
            else
            {
                System.Console.WriteLine("You Have Died to Amarok");
                Environment.Exit(0);
            }
        }
        if (num == 3)
            System.Console.WriteLine("Nothing but a Amarok corpse");
    }

    void checkMaelstrom(int num)
    {
        if (num == 1)
            System.Console.WriteLine("You hear a Maelstrom");
        if (num == 2)
        {
            System.Console.WriteLine("You have met the Maelstrom and will be moved");
            map[a, b] = 0;
            b++;
            a--;
            a--;
            if (a < 0)
                a = 0;
            if (b > xaxis - 1)
            {
                b = xaxis - 1;
            }
            map[a, b] = 1;
            System.Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            PrintMap();
        }
    }

    void generateMaelstrom()
    {
        int rdm1;
        int rdm2;

        rdm1 = random.Next(yaxis - 1);
        rdm2 = random.Next(xaxis - 1);
        try
        {
            maelstromMap[rdm1, rdm2] = 2;
            maelstromMap[rdm1 + 1, rdm2] = 1;
            maelstromMap[rdm1 - 1, rdm2] = 1;
            maelstromMap[rdm1, rdm2 + 1] = 1;
            maelstromMap[rdm1, rdm2 - 1] = 1;
            maelstromMap[rdm1 + 1, rdm2 + 1] = 1;
            maelstromMap[rdm1 + 1, rdm2 - 1] = 1;
            maelstromMap[rdm1 - 1, rdm2 - 1] = 1;
            maelstromMap[rdm1 + 1, rdm2 - 1] = 1;
        }
        catch { } //I know risky, I did it for a reason
    }

    void generateAmarok()
    {
        int rdm1;
        int rdm2;

        rdm1 = random.Next(yaxis - 1);
        rdm2 = random.Next(xaxis - 1);
        if(rdm1 == 0 && rdm2 == 0)
        try
        {
            amoroksMap[rdm1, rdm2] = 2;
            amoroksMap[rdm1 + 1, rdm2] = 1;
            amoroksMap[rdm1 - 1, rdm2] = 1;
            amoroksMap[rdm1, rdm2 + 1] = 1;
            amoroksMap[rdm1, rdm2 - 1] = 1;
            amoroksMap[rdm1 + 1, rdm2 + 1] = 1;
            amoroksMap[rdm1 + 1, rdm2 - 1] = 1;
            amoroksMap[rdm1 - 1, rdm2 - 1] = 1;
            amoroksMap[rdm1 + 1, rdm2 - 1] = 1;
        }
        catch { } //I know risky, I did it for a reason
    }
}
