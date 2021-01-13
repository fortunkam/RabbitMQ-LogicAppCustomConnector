resource "azurerm_storage_account" "function_storage" {
  name                     = "tfsta${lower(random_id.storage_name.hex)}"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "appplan" {
  name                = local.app_service_plan_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_function_app" "function" {
  name                      = local.function_name
  location                  = azurerm_resource_group.rg.location
  resource_group_name       = azurerm_resource_group.rg.name
  app_service_plan_id       = azurerm_app_service_plan.appplan.id
  version = "~3"
  storage_account_name = azurerm_storage_account.function_storage.name
  storage_account_access_key = azurerm_storage_account.function_storage.primary_access_key

  app_settings = {
    "RabbitMQConnectionAppSetting"=var.rabbitMQConnectionString
    "WEBSITE_DISABLE_OVERLAPPED_RECYCLING" = "1"
    "APPLICATIONINSIGHTS_CONNECTION_STRING"="InstrumentationKey=${azurerm_application_insights.appinsights.instrumentation_key}"
    "APPINSIGHTS_INSTRUMENTATIONKEY"=azurerm_application_insights.appinsights.instrumentation_key
    "APPINSIGHTS_PROFILERFEATURE_VERSION"="1.0.0"
    "APPINSIGHTS_SNAPSHOTFEATURE_VERSION"="1.0.0"
    "ApplicationInsightsAgent_EXTENSION_VERSION"="~2"
    "DiagnosticServices_EXTENSION_VERSION"="~3"
    "InstrumentationEngine_EXTENSION_VERSION"="disabled"
    "SnapshotDebugger_EXTENSION_VERSION"="disabled"
    "XDT_MicrosoftApplicationInsights_BaseExtensions"="disabled"
    "XDT_MicrosoftApplicationInsights_Mode"="recommended"
  }
}