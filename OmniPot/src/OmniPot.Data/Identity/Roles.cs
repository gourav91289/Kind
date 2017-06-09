using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data.Identity
{
    public static class Roles
    {
        //TODO: The rest of the available application roles for seeding.
        private static readonly string[] roles = {
            "SuperAdmin",
            "TechSupport",
            "TenantAdmin",
            "DeliveryDriver",
            "FrontDesk",
            "HumanResources",
            "Inventory",
            "InventoryAdmin",
            "InventoryManager",
            "Investor",
            "Owner",
            "PlantHarvester",
            "PlantTender",
            "Pharmacist",
            "Sales",
            "SalesAdmin",
            "SalesManager",
            "SecurityOfficer"
        };

        public static string SuperAdmin         { get { return roles[0]; } }
        public static string TechSupport        { get { return roles[1]; } }
        public static string TenantAdmin        { get { return roles[2]; } }
        public static string DeliveryDriver     { get { return roles[3]; } }
        public static string FrontDesk          { get { return roles[4]; } }
        public static string HumanResources     { get { return roles[5]; } }
        public static string Inventory          { get { return roles[6]; } }
        public static string InventoryAdmin     { get { return roles[7]; } }
        public static string InventoryManager   { get { return roles[8]; } }
        public static string Investor           { get { return roles[9]; } }
        public static string Owner              { get { return roles[10]; } }
        public static string PlantHarvester     { get { return roles[11]; } }
        public static string PlantTender        { get { return roles[12]; } }
        public static string Pharmacist         { get { return roles[13]; } }
        public static string Sales              { get { return roles[14]; } }
        public static string SalesAdmin         { get { return roles[15]; } }
        public static string SalesManager       { get { return roles[16]; } }
        public static string SecurityOfficer    { get { return roles[17]; } }


        public static IEnumerable<string> All { get { return roles; } }

        public static string getRoleIndex(int index) { return roles[index]; }
    }
}
