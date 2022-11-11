using System.Runtime.InteropServices;

namespace LineItemLibrary;

public class LineItem
{
    public int Quantity { get; set; }
    public string? MetalColor { get; set; }
    public string? CustomerName { get; set; }
    public int PriorityFactor { get; set; }
    public bool IsMade { get; set; }

    public LineItem(int quantity, string? metalColor, string? customerName, int priorityFactor, bool isMade = false)
    {
        this.Quantity = quantity;
        this.MetalColor = metalColor;
        this.CustomerName = customerName;
        this.PriorityFactor = priorityFactor;
        this.IsMade = isMade;
    }

    public static LineItem CreateLineItem(int quantity, string? metalColor, string? customerName, int priorityFactor)
    {
        return new LineItem(quantity, metalColor, customerName, priorityFactor);
    }

    public static LineItem UpdateLineItem(LineItem itemToUpdate, int newQuantity)
    { 
       itemToUpdate.Quantity = newQuantity;
        return itemToUpdate;
    }

    public static LineItem UpdateLineItem(LineItem itemToUpdate, string? newMetalColor)
    {
        itemToUpdate.MetalColor = newMetalColor;
        return itemToUpdate;
    }

    public static LineItem UpdateLineItem(LineItem itemToUpdate, string? newCustomerName, bool overLoad)
    {
        itemToUpdate.CustomerName = newCustomerName;
        return itemToUpdate;
    }

    public static LineItem UpdateLineItem(LineItem itemToUpdate, int newPriorityFactor, bool overLoad)
    {
        itemToUpdate.PriorityFactor = newPriorityFactor;
        return itemToUpdate;
    }

    public static LineItem UpdateLineItem(LineItem itemToUpdate, bool newIsMade)
    {
        itemToUpdate.IsMade = newIsMade;
        return itemToUpdate;
    }
}