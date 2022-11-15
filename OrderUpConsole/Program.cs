using LineItemLibrary;
using Newtonsoft.Json;
using Spectre.Console;

Console.Title = "OrderUp";
Console.WriteLine("Welcome to OrderUp");
Console.WriteLine("");

var itemList = new List<LineItem>();
var userWishesToContinue = true;
var pathToJson = Directory.GetCurrentDirectory() + "\\workinglist.txt";

if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
{
    pathToJson = Directory.GetCurrentDirectory() + "/workinglist.txt";
}


if (File.Exists(pathToJson))
{
    string loadingString = File.ReadAllText(pathToJson);
    itemList = JsonConvert.DeserializeObject<List<LineItem>>(loadingString);
}

while (userWishesToContinue)
{
    var optionSelected = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(new[] {
                "Create New Item", "View Current List", "Update an Item",
                "Delete an Item", "Save and Exit",
            }));
    
    switch (optionSelected)
    {
        case "Create New Item":

            Console.Clear();
            int quantity = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter Quantity: ")
                    .ValidationErrorMessage("[red]Error Try Again[/]")
                    .Validate(quantity =>
                     {
                       return quantity switch
                        {
                          <= 0 => ValidationResult.Error("[red]That's just silly.[/]"),
                          _ => ValidationResult.Success(),
                         };
                     }));
            Console.Clear();

            var metalColorSelected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a Color")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more colors)[/]")
                    .AddChoices(new[] {
                        "#2 White", "Barn Red", "Black", "Bright Red", "Brown", "Buckskin Tan", "Burnished Slate",
                        "Charcoal", "Clay", "Copper Penny", "Dark Red", "Gallery Blue", "Galvalume", "Gray",
                        "Green", "Hawaiian Blue", "Light Stone", "Sapphire Blue", "Tan", "Plum", "White",
                    }));
            
            string customerName = AnsiConsole.Ask<string>("Enter Customer Name: ");

            Console.Clear();

            var priority = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("Select a Priority Factor")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(new[] {
                        1, 2, 3,
                    }));

            itemList!.Add(LineItem.CreateLineItem(quantity, metalColorSelected, customerName, priority));
            break;

        case "View Current List":
            Console.Clear();
            Console.WriteLine("Current List");
            foreach (var lineItem in itemList!)
            {
                Console.WriteLine($"  {lineItem.Quantity}  {lineItem.MetalColor}  {lineItem.CustomerName}  {lineItem.PriorityFactor}  {lineItem.IsMade}");
            }
            Console.WriteLine();
            break;

        case "Update an Item":
            Console.Clear();
            var lineItemToUpdate = AnsiConsole.Prompt(
                        new SelectionPrompt<LineItem>()
                            .Title("Select an Item to Update")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
                            .AddChoices<LineItem>(itemList!)
                            .UseConverter<LineItem>(DisplaySelector));

            int indexOfLineItemToUpdate = itemList!.IndexOf(lineItemToUpdate);

         
            var selection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Update:")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more)[/]")
                            .AddChoices(new[] {
                                "Quantity", "Metal Color", "Customer Name",
                                "Priority", }));

                switch (selection)
            {
                case "Quantity":
                    Console.WriteLine("Enter New Quantity");
                    int newQuantity = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter Quantity: ")
                            .ValidationErrorMessage("[red]Error Try Again[/]")
                            .Validate(newQuantity =>
                            {
                               return newQuantity switch
                              {
                                <= 0 => ValidationResult.Error("[red]That's just silly.[/]"),
                                _ => ValidationResult.Success(),
                              };
                            }));
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdateQuantity(lineItemToUpdate, newQuantity);
                    Console.Clear();
                    break;
                    
                case "Metal Color":
                    var newMetalColor = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select a Color")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more colors)[/]")
                            .AddChoices(new[] {
                             "#2 White", "Barn Red", "Black", "Bright Red", "Brown", "Buckskin Tan", "Burnished Slate",
                             "Charcoal", "Clay", "Copper Penny", "Crinkle Brown", "Dark Red", "Gallery Blue", "Galvalume",
                             "Gray", "Green", "Hawaiian Blue", "Light Stone", "Sapphire Blue", "Tan", "Plum", "White",

                            }));
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdateMetalColor(lineItemToUpdate, newMetalColor);
                    Console.Clear();
                    break;

                case "Customer Name":
                    Console.WriteLine("Enter New Customer Name");
                    string? newCustomerName = Console.ReadLine();
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdateCustomerName(lineItemToUpdate, newCustomerName);
                    Console.Clear();
                    break;

                case "Priority":
                    var newPriorityFactor = AnsiConsole.Prompt(
                        new SelectionPrompt<int>()
                            .Title("Select a Priority Factor")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                            .AddChoices(new[] {
                             1, 2, 3,
                            }));
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdatePriorityFactor(lineItemToUpdate, newPriorityFactor);
                    Console.Clear();
                    break;
            }
            break;

        case "Delete an Item":
           
            Console.Clear();
            var selectedLineItem = AnsiConsole.Prompt(
                        new SelectionPrompt<LineItem>()
                            .Title("Select an Item to Delete")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
                            .AddChoices<LineItem>(itemList!)
                            .UseConverter<LineItem>(DisplaySelector));
            var userConfirmsDelete = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title($"Are you sure you want to delete this item?\n\n{DisplaySelector(selectedLineItem)}")
                            .AddChoices(new[] {
                             "Yes", "No",
                            }));
            if (userConfirmsDelete == "Yes")
            {
                itemList!.Remove(selectedLineItem);
                break;
            }
                break;

        case "Save and Exit":
            Console.WriteLine("Goodbye!");
            string saveState = JsonConvert.SerializeObject(itemList, Formatting.Indented);
            File.WriteAllText(pathToJson, saveState);
            userWishesToContinue = false;
            break;
    }
}



static string DisplaySelector (LineItem lineitem)
{
    var result = $"{lineitem.Quantity}    {lineitem.MetalColor}    {lineitem.CustomerName}"; 

    return result;
}