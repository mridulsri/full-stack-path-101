using DOMTrace.Models;
using System.Collections.ObjectModel;

namespace DOMTrace.Core;

public static class StaticDataContext
{
    public static ObservableCollection<ComboBoxOption> RegexCollection
    {
        get
        {
            return
            [
                new ("--Select--", ""),
                new ("Amount", @"(\d+\.\d{1,2})"),
                new ("Number Only", @"[0-9]+"),
                new ("Letter Only", @"[a-zA-Z ]+"),
                new ("Letter and Number", @"^[a-zA-Z0-9 ]+"),
            ];
        }
    }

    public static ObservableCollection<ComboBoxOption> ExpertContainerOptionCollection
    {
        get
        {
            return [
                new("Detail_container","1"),
                new("Item_list_container","2"),
                new("Item_container","3"),
                new("Image_container","4"),
                new("Menu_container","5"),
                new("Product_detail_container","6"),
                new("Paging_container","7"),
                new("Sub_menu_container","8"),
                new("Size_container","9"),
            ];
        }
    }
    public static ObservableCollection<ComboBoxOption> ExpertFieldIOptionCollection
    {
        get
        {
            return [
                 new("Age_group","1"),
                 new("Brand","2"),
                 new("Breadcrumbs","3"),
                 new("Color","4"),
                 new("Currency","5"),
                 new("Description","6"),
                 new("Gender","7"),
                 new("SKU","8"),
                 new("Industry","9"),
                 new("Material","10"),
                 new("Category","11"),
                 new("Name","12"),
                 new("Variations","13"),
                 new("Price","14"),
                 new("MRP","15"),
                 new("Sizes","16"),
                 new("stock_qty","17"),
                 new("Stock_status","18"),
                 new("Style","19"),
                 new("Menu_item","20"),
                 new("SubMenu_item","21"),
                 new("Image","22"),
                 new("Page","23"),
                 new("Menu_container","24"),
                 new("Sub_menu_container","25"),
                 new("Item_list_container","26"),
                 new("Item_container","27"),
                 new("Image_container","28"),
                 new("Detail_container","29"),
                 new("Size_container","30"),
                 new("Product_detail_container","31"),
                 new("Paging_container","32")
            ];
        }
    }
}
