#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

namespace EvenCart.Infrastructure.Routing
{
    public static class AdminRouteNames
    {
        private const string Prefix = "Admin.";

        public const string SelectorDialog = Prefix + "SelectorDialog";
        public const string GetTemplates = Prefix + "GetTemplates";

        public const string Dashboard = Prefix + "Dashboard";
        public const string RestartApplication = Prefix + "RestartApplication";
        public const string PurgeCache = Prefix + "PurgeCache";

        public const string ProductsList = Prefix + "ProductsList";
        public const string SaveProduct = Prefix + "SaveProduct";
        public const string SaveProductMedia = Prefix + "SaveProductMedia";
        public const string DeleteProduct = Prefix + "DeleteProduct";
        public const string GetProduct = Prefix + "GetProduct";
        public const string ProductVendorsList = Prefix + "ProductVendorsList";
        public const string GetProductVendor = Prefix + "GetProductVendor";
        public const string SaveProductVendor = Prefix + "SaveProductVendor";
        public const string DeleteProductVendor = Prefix + "DeleteProductVendor";

        public const string UploadMedia = Prefix + "UploadMedia";
        public const string UploadMediaUrl = Prefix + "UploadMediaUrl";
        public const string UpdateMediaDisplayOrder = Prefix + "UpdateMediaDisplayOrder";
        public const string DeleteMedia = Prefix + "DeleteMedia";

        public const string UpdateProductCategoryDisplayOrder = Prefix + "UpdateProductCategoryDisplayOrder";
        public const string GetCategorySuggestions = Prefix + "GetCategorySuggestions";
        public const string DeleteProductCategories = Prefix + "DeleteProductCategories";
        public const string CategoriesList = Prefix + "CategoriesList";
        public const string SaveCategory = Prefix + "SaveCategory";
        public const string SaveCategoryTrees = Prefix + "SaveCategoryTrees";
        public const string EditCategory = Prefix + "EditCategory";
        public const string DeleteCategory = Prefix + "DeleteCategory";
        public const string UpdateCategoryDisplayOrder = Prefix + "UpdateCategoryDisplayOrder";

        
        public const string GetAttributeSuggestions = Prefix + "GetAttributeSuggestions";
        public const string GetAttributeValueSuggestions = Prefix + "GetAttributeValueSuggestions";

        public const string SaveProductAttribute = Prefix + "SaveProductAttribute";
        public const string ProductAttributesList = Prefix + "ProductAttributesList";
        public const string EditProductAttribute = Prefix + "EditProductAttribute";
        public const string DeleteProductAttribute = Prefix + "DeleteProductAttribute";
        public const string DeleteProductAttributeValue = Prefix + "DeleteProductAttributeValue";
        public const string UpdateProductAttributeDisplayOrder = Prefix + "UpdateProductAttributeDisplayOrder";
        public const string UpdateProductAttributeValueDisplayOrder = Prefix + "UpdateProductAttributeValueDisplayOrder";

        public const string SaveProductSpecification = Prefix + "SaveProductSpecification";
        public const string SaveProductSpecificationGroup = Prefix + "SaveProductSpecificationGroup";
        public const string ProductSpecificationsList = Prefix + "ProductSpecificationsList";
        public const string ProductSpecificationsListByGroup = Prefix + "ProductSpecificationsListByGroup";
        public const string EditProductSpecification = Prefix + "EditProductSpecification";
        public const string EditProductSpecificationGroup = Prefix + "EditProductSpecificationGroup";
        public const string DeleteProductSpecification = Prefix + "DeleteProductSpecification";
        public const string DeleteProductSpecificationValue = Prefix + "DeleteProductSpecificationValue";
        public const string DeleteProductSpecificationGroup = Prefix + "DeleteProductSpecificationGroup";
        public const string GetProductSpecificationsGroupSuggestions = Prefix + "GetProductSpecificationsGroupSuggestions";
        public const string UpdateProductSpecificationDisplayOrder = Prefix + "UpdateProductSpecificationDisplayOrder";

        public const string SaveProductRelation = Prefix + "SaveProductRelation";
        public const string ProductRelationsList = Prefix + "ProductRelationsList";
        public const string EditProductRelation = Prefix + "EditProductRelation";
        public const string DeleteProductRelation = Prefix + "DeleteProductRelation";

        public const string EditProductVariant = Prefix + "EditProductVariant";
        public const string ProductVariantsList = Prefix + "ProductVariantsList";
        public const string DeleteProductVariant = Prefix + "DeleteProductVariant";
        public const string SaveProductVariant = Prefix + "SaveProductVariant";

        public const string GetAvailableAttribute = Prefix + "GetAvailableAttribute";
        public const string AvailableAttributesList = Prefix + "AvailableAttributesList";
        public const string SaveAvailableAttribute = Prefix + "SaveAvailableAttribute";
        public const string DeleteAvailableAttribute = Prefix + "DeleteAvailableAttribute";

        public const string InventoryList = Prefix + "InventoryList";
        public const string SaveInventory = Prefix + "SaveInventory";

        public const string OrdersList = Prefix + "OrdersList";
        public const string SaveOrder = Prefix + "SaveOrder";
        public const string DeleteOrder = Prefix + "DeleteOrder";
        public const string CancelAdminOrder = Prefix + "CancelAdminOrder";
        public const string GetOrder = Prefix + "GetOrder";
        public const string GetOrderInfo = Prefix + "GetOrderInfo";
        public const string DownloadInvoice = Prefix + "DownloadAdminInvoice";
        public const string DownloadPackingSlip = Prefix + "DownloadAdminPackingSlip";
        public const string CancelAdminSubscription = Prefix + "CancelAdminSubscription";

        public const string OrderFulfillmentsList = Prefix + "OrderFulfillmentsList";
        public const string SaveOrderFulfillment = Prefix + "SaveOrderFulfillment";
        public const string GetOrderFulfillment = Prefix + "GetOrderFulfillment";
        public const string ReturnRequestsList = Prefix + "ReturnRequestsList";
        public const string SaveReturnRequest = Prefix + "SaveReturnRequest";
        public const string DeleteReturnRequest = Prefix + "DeleteReturnRequest";
        public const string GetReturnRequest = Prefix + "GetReturnRequest";
        public const string PaymentTransactionsList = Prefix + "PaymentTransactionsList";
        public const string RefundEditor = Prefix + "RefundEditor";
        public const string SavePaymentStatus = Prefix + "SavePaymentStatus";
        public const string SaveOrderStatus = Prefix + "SaveOrderStatus";
        public const string ApproveRefund = Prefix + "ApproveRefund";
        public const string ApproveCapture = Prefix + "ApproveCapture";
        public const string ApproveVoid = Prefix + "ApproveVoid";
        public const string OrderDownloadsList = Prefix + "OrderDownloadsList";
        public const string SaveOrderDownload = Prefix + "SaveOrderDownload";

        public const string GetShipment = Prefix + "GetShipment";
        public const string ShipmentsList = Prefix + "ShipmentsList";
        public const string SaveShipment = Prefix + "SaveShipment";
        public const string SaveShipmentHistory = Prefix + "SaveShipmentHistory";
        public const string DeleteShipmentHistory = Prefix + "DeleteShipmentHistory";
        public const string DeleteShipment = Prefix + "DeleteShipment";
        public const string BuyShipmentLabel = Prefix + "BuyShipmentLabel";

        public const string UsersList = Prefix + "UsersList";
        public const string AffiliatesList = Prefix + "AffiliatesList";
        public const string GetUser = Prefix + "GetUser";
        public const string SaveUser = Prefix + "SaveUser";
        public const string DeleteUser = Prefix + "DeleteUser";
        public const string RolesList = Prefix + "RolesList";
        public const string SaveRole = Prefix + "SaveRole";
        public const string DeleteRole = Prefix + "DeleteRole";
        public const string GetRole = Prefix + "GetRole";
        public const string CapabilitiesList = Prefix + "CapabilitiesList";
        public const string RoleCapabilitiesList = Prefix + "CapabilitiesList";
        public const string SaveCapabilities = Prefix + "SaveCapabilities";
        public const string UserCart = Prefix + "UserCart";
        public const string UserPointsList = Prefix + "UserPointsList";
        public const string SaveUserPoint = Prefix + "SaveUserPoint";
        public const string GetUserPoint = Prefix + "GetUserPoint";
        public const string StoreCreditsList = Prefix + "StoreCreditsList";
        public const string SaveStoreCredit = Prefix + "SaveStoreCredit";
        public const string GetStoreCredit = Prefix + "GetStoreCredit";
        public const string UserImitate = Prefix + "UserImitate";
        public const string AnonymizeUser = Prefix + "AnonymizeUser";
        public const string GenerateInviteLink = Prefix + "GenerateInviteLink";
        public const string InvitationRequestsList = Prefix + "InvitationRequestsList";
        public const string DeleteInvitationRequest = Prefix + "DeleteInvitationRequest";

        public const string AddressList = Prefix + "AddressList";
        public const string GetAddress = Prefix + "GetAddress";
        public const string SaveAddress = Prefix + "SaveAddress";
        public const string DeleteAddress = Prefix + "DeleteAddress";
        public const string UserOrdersList = Prefix + "UserOrdersList";
      
        public const string VendorsList = Prefix + "VendorsList";
        public const string GetVendor = Prefix + "GetVendor";
        public const string SaveVendor = Prefix + "SaveVendor";
        public const string DeleteVendor = Prefix + "DeleteVendor";
        public const string VendorUsersList = Prefix + "VendorUsersList";
        public const string SaveVendorUser = Prefix + "SaveVendorUser";
        public const string DeleteVendorUser = Prefix + "DeleteVendorUser";

        public const string DiscountsList = Prefix + "DiscountsList";
        public const string GetDiscount = Prefix + "GetDiscount";
        public const string SaveDiscount = Prefix + "SaveDiscount";
        public const string DeleteDiscount = Prefix + "DeleteDiscount";
        public const string DeleteDiscountRestriction = Prefix + "DeleteDiscountRestriction";

        public const string ManufacturersList = Prefix + "ManufacturersList";
        public const string GetManufacturer = Prefix + "GetManufacturer";
        public const string SaveManufacturer = Prefix + "SaveManufacturer";
        public const string DeleteManufacturer = Prefix + "DeleteManufacturer";
        public const string GetManufacturerSuggestions = Prefix + "GetManufacturerSuggestions";

        public const string PaymentMethodsList = Prefix + "PaymentMethodsList";
        public const string ShippingMethodsList = Prefix + "ShippingMethodsList";
        public const string PluginsList = Prefix + "PluginsList";
        public const string MarketPluginsList = Prefix + "MarketPluginsList";
        public const string UpdatePluginStatus = Prefix + "UpdatePluginStatus";
        public const string UpgradeDbPlugin = Prefix + "UpgradeDbPlugin";
        public const string WidgetsList = Prefix + "WidgetsList";
        public const string SaveWidget = Prefix + "SaveWidget";
        public const string DeleteWidget = Prefix + "DeleteWidget";
        public const string UpdateWidgetsDisplayOrder = Prefix + "UpdateWidgetsDisplayOrder";
        public const string UploadPackage = Prefix + "UploadPackage";
        public const string GetSettings = Prefix + "GetSettings";
        public const string SaveSettings = Prefix + "SaveSettings";
        public const string SaveSharedSecuritySetting = Prefix + "SaveSharedSecuritySetting";

        public const string GetWidgetSettings = Prefix + "GetWidgetSettings";
        public const string SaveWidgetSettings = Prefix + "SaveWidgetSettings";

        public const string ContentPagesList = Prefix + "ContentPagesList";
        public const string GetContentPage = Prefix + "GetContentPage";
        public const string SaveContentPage = Prefix + "SaveContentPage";
        public const string DeleteContentPage = Prefix + "DeleteContentPage";

        public const string EmailTemplatesList = Prefix + "EmailTemplatesList";
        public const string GetEmailTemplate = Prefix + "GetEmailTemplate";
        public const string SaveEmailTemplate = Prefix + "SaveEmailTemplate";
        public const string DeleteEmailTemplate = Prefix + "DeleteEmailTemplate";

        public const string EmailAccountsList = Prefix + "EmailAccountsList";
        public const string GetEmailAccount = Prefix + "GetEmailAccount";
        public const string SaveEmailAccount= Prefix + "SaveEmailAccount";
        public const string DeleteEmailAccount = Prefix + "DeleteEmailAccount";
        public const string TestEmailAccount = Prefix + "TestEmailAccount";

        public const string EmailMessagesList = Prefix + "EmailMessagesList";
        public const string GetEmailMessage = Prefix + "GetEmailMessage";
        public const string SaveEmailMessage = Prefix + "SaveEmailMessage";
        public const string DeleteEmailMessage = Prefix + "DeleteEmailMessage";
        public const string ResendEmailMessage = Prefix + "ResendEmailMessage";

        public const string ScheduledTasksList = Prefix + "ScheduledTasksList";
        public const string GetScheduledTask = Prefix + "GetScheduledTask";
        public const string SaveScheduledTask = Prefix + "SaveScheduledTask";
        public const string RunScheduledTask = Prefix + "RunScheduledTask";

        public const string TaxesList = Prefix + "TaxesList";
        public const string GetTax = Prefix + "GetTax";
        public const string SaveTax = Prefix + "SaveTax";
        public const string DeleteTax = Prefix + "DeleteTax";
        public const string TaxRatesList = Prefix + "TaxRatesList";
        public const string GetTaxRate = Prefix + "GetTaxRate";
        public const string SaveTaxRate = Prefix + "SaveTaxRate";
        public const string DeleteTaxRate = Prefix + "DeleteTaxRate";

        public const string CountriesList = Prefix + "CountriesList";
        public const string GetCountry = Prefix + "GetCountry";
        public const string SaveCountry = Prefix + "SaveCountry";

        public const string StatesList = Prefix + "StatesList";
        public const string GetState = Prefix + "GetState";
        public const string SaveState = Prefix + "SaveState";
        public const string DeleteState = Prefix + "DeleteState";

        public const string MenuList = Prefix + "MenuList";
        public const string GetMenu = Prefix + "GetMenu";
        public const string SaveMenu = Prefix + "SaveMenu";
        public const string DeleteMenu = Prefix + "DeleteMenu";
        public const string MenuItemList = Prefix + "MenuItemList";
        public const string GetMenuItem = Prefix + "GetMenuItem";
        public const string SaveMenuItem = Prefix + "SaveMenuItem";
        public const string DeleteMenuItem = Prefix + "DeleteMenuItem";
        public const string UpdateMenuItemDisplayOrder = Prefix + "UpdateMenuItemDisplayOrder";
        public const string BulkCreateMenuItems = Prefix + "BulkCreateMenuItems";

        public const string CurrenciesList = Prefix + "CurrenciesList";
        public const string GetCurrency = Prefix + "GetCurrency";
        public const string SaveCurrency = Prefix + "SaveCurrency";
        public const string DeleteCurrency = Prefix + "DeleteCurrency";

        public const string ConsentsList = Prefix + "ConsentsList";
        public const string GetConsent = Prefix + "GetConsent";
        public const string SaveConsent = Prefix + "SaveConsent";
        public const string DeleteConsent = Prefix + "DeleteConsent";
        public const string UpdateConsentDisplayOrder = Prefix + "UpdateConsentDisplayOrder";

        public const string ConsentGroupsList = Prefix + "ConsentGroupsList";
        public const string GetConsentGroup = Prefix + "GetConsentGroup";
        public const string SaveConsentGroup = Prefix + "SaveConsentGroup";
        public const string DeleteConsentGroup = Prefix + "DeleteConsentGroup";
        public const string UpdateConsentGroupDisplayOrder = Prefix + "UpdateConsentGroupDisplayOrder";

        public const string ConsentLogsList = Prefix + "ConsentLogsList";

        public const string ReviewsList = Prefix + "ReviewsList";
        public const string SaveReview = Prefix + "SaveReview";
        public const string DeleteReview = Prefix + "DeleteReview";
        public const string GetReview = Prefix + "GetReview";

        public const string StockReport = Prefix + "StockReport";
        public const string OrdersReport = Prefix + "OrdersReport";
        public const string UserOrdersReport = Prefix + "UserOrdersReport";
        public const string ProductOrdersReport = Prefix + "ProductOrdersReport";
        public const string ProductWishReport = Prefix + "ProductWishReport";
        public const string TaxReport = Prefix + "TaxReport";
        public const string TotalsReport = Prefix + "TotalsReport";
        public const string RecentOrdersReport = Prefix + "RecentOrdersReport";
        public const string RecentUsersReport = Prefix + "RecentUsersReport";
        public const string BestSellersReport = Prefix + "BestSellersReport";
        public const string OrdersChartReport = Prefix + "OrdersChartReport";
        public const string UsersChartReport = Prefix + "UsersChartReport";


        public const string WarehouseList = Prefix + "WarehouseList";
        public const string GetWarehouse = Prefix + "GetWarehouse";
        public const string SaveWarehouse = Prefix + "SaveWarehouse";
        public const string DeleteWarehouse = Prefix + "DeleteWarehouse";
        public const string UpdateWarehouseDisplayOrder = Prefix + "UpdateWarehouseDisplayOrder";

        public const string GetCustomLabelSuggestions = Prefix + "GetCustomLabelSuggestions";
        public const string GetCustomLabel = Prefix + "GetCustomLabel";
        public const string SaveCustomLabel = Prefix + "SaveCustomLabel";
        public const string CustomLabelsList = Prefix + "CustomLabelsList";
        public const string EditCustomLabel = Prefix + "EditCustomLabel";
        public const string DeleteCustomLabel = Prefix + "DeleteCustomLabel";
        public const string UpdateCustomLabelDisplayOrder = Prefix + "UpdateCustomLabelDisplayOrder";

        public const string LogsList = Prefix + "LogsList";
        public const string GetLog = Prefix + "GetLog";
        public const string DeleteLog = Prefix + "DeleteLog";
        public const string ClearLogs = Prefix + "ClearLogs";

        public const string GetAbout = Prefix + "GetAbout";

        public const string DownloadList = Prefix + "DownloadList";
        public const string UploadDownloadFile = Prefix + "UploadDownloadFile";
        public const string GetDownload = Prefix + "GetDownload";
        public const string SaveDownload = Prefix + "SaveDownload";
        public const string DeleteDownload = Prefix + "DeleteDownload";
        public const string AdminDownloadFile = Prefix + "AdminDownloadFile";
        public const string UpdateDownloadDisplayOrder = Prefix + "UpdateDownloadDisplayOrder";

        public const string InstallSampleData = Prefix + "InstallSampleData";
        public const string GetEntityTagsSuggestions = Prefix + "GetEntityTagsSuggestions";

        public const string StoresList = Prefix + "StoresList";
        public const string GetStore = Prefix + "GetStore";
        public const string DeleteStore = Prefix + "DeleteStore";
        public const string SaveStore = Prefix + "SaveStore";
        public const string CloneStore = Prefix + "CloneStore";

        public const string CatalogsList = Prefix + "CatalogsList";
        public const string GetCatalog = Prefix + "GetCatalog";
        public const string DeleteCatalog = Prefix + "DeleteCatalog";
        public const string SaveCatalog = Prefix + "SaveCatalog";

        public const string DataTransfer = Prefix + "DataTransfer";
        public const string DataTransferExport = Prefix + "DataTransferExport";
        public const string DataTransferImport = Prefix + "DataTransferImport";

    }
}