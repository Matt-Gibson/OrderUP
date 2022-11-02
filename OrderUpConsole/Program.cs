using LineItemLibrary;
using Newtonsoft.Json;
using Spectre.Console;

Console.WriteLine("Welcome to OrderUp");
Console.WriteLine("");
Console.WriteLine("What would you like to do?");

var itemList = new List<LineItem>();
var userWishesToContinue = true;

if (File.Exists(@"C:\\temp\WorkingList.txt"))
{
    string loadingString = File.ReadAllText(@"C:\\temp\WorkingList.txt");
    itemList = JsonConvert.DeserializeObject<List<LineItem>>(loadingString);
}
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
            itemList!.Add(LineItem.createLineItem(quantity, metalColor, customerName, priority));
            break;
        case 2:
            Console.WriteLine("Current List");
            foreach (var lineItem in itemList!)
            {
                Console.WriteLine(lineItem.quantity);
                Console.WriteLine(lineItem.metalColor);
                Console.WriteLine(lineItem.customerName);
                Console.WriteLine(lineItem.priorityFactor);
                Console.WriteLine(lineItem.isMade);
            }
            break;
        case 3:
            Console.WriteLine("Which Item Shall Be Updated?");
            int lineNumber = int.Parse(Console.ReadLine()!);

            Console.WriteLine("What do you wish to update?");
            Console.WriteLine("[1] Quantity");
            Console.WriteLine("[2] Metal Color");
            Console.WriteLine("[3] Customer Name");
            Console.WriteLine("[4] Priority");
            int selection = int.Parse(Console.ReadLine()!);

            switch (selection)
            {
                case 1:
                    Console.WriteLine("Enter New Quantity");
                    int newQuantity = int.Parse(Console.ReadLine()!);
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newQuantity);
                    break;
                case 2:
                    Console.WriteLine("Enter New Metal Color");
                    string? newMetalColor = Console.ReadLine();
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newMetalColor);
                    break;
                case 3:
                    Console.WriteLine("Enter New Customer Name");
                    string? newCustomerName = Console.ReadLine();
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newCustomerName, true);
                    break;
                case 4:
                    Console.WriteLine("Enter New Priority Factor");
                    int newPriorityFactor = int.Parse(Console.ReadLine()!);
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newPriorityFactor, true);
                    break;
                default:
                    break;
            }
            break;
        case 4:
            Console.WriteLine("Which Item Shall Be Deleted?");
            itemList!.RemoveAt(int.Parse(Console.ReadLine()!));
            break;
        case 5:
            Console.WriteLine("Goodbye!");
            string saveState = JsonConvert.SerializeObject(itemList, Formatting.Indented);
            File.WriteAllText(@"C:\\temp\WorkingList.txt", saveState);
            userWishesToContinue = false;
            break;
        default:
            break;
    }
}