using LineItemLibrary;

Console.WriteLine("Welcome to OrderUp");
Console.WriteLine("");
Console.WriteLine("What would you like to do?");

var itemList = new List<LineItem>();
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
            Console.WriteLine("Enter a Quantity");
            int quantity = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter a Metal Color");
            string? metalColor = Console.ReadLine();
            Console.WriteLine("Enter a Customer Name");
            string? customerName = Console.ReadLine();
            Console.WriteLine("Enter a Priority");
            int priority = int.Parse(Console.ReadLine()!);
            itemList.Add(new LineItem(quantity, metalColor, customerName, priority));
            break;
        case 2:
            Console.WriteLine("Current List");
            foreach (var LineItem in itemList)
            {
                Console.WriteLine(LineItem.quantity);
                Console.WriteLine(LineItem.metalColor);
                Console.WriteLine(LineItem.customerName);
                Console.WriteLine(LineItem.priorityFactor);
                Console.WriteLine(LineItem.isMade);
            }
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