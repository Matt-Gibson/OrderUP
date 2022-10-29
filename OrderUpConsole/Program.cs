Console.WriteLine("Welcome to OrderUp");
Console.WriteLine("");
Console.WriteLine("What would you like to do?");

bool userWishesToContinue = true;
while (userWishesToContinue)
{
    Console.WriteLine("[1] Create New Item");
    Console.WriteLine("[2] View Current List");
    Console.WriteLine("[3] Update An Item");
    Console.WriteLine("[4] Delete An Item");
    Console.WriteLine("[5] Exit");

    switch (int.Parse(Console.ReadLine()!))
    {
        case 1:
            break;
        case 2:
            Console.WriteLine("Current List");
            break;
        case 3:
            Console.WriteLine("Which Item Shall Be Updated?");
            break;
        case 4:
            Console.WriteLine("Which Item Shall Be Deleted?");
            break;
        case 5:
            Console.WriteLine("Goodbye!");
            userWishesToContinue = false;
            break;
        default:
            break;
    }
}