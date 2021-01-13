variable location {
    default="uksouth"
}
variable prefix {
    default="rmq"
}

variable rabbitMQConnectionString {}

locals {
    rg_name = "${var.prefix}"
    app_service_plan_name = "${var.prefix}-plan"
    rabbitmq_aci_name = "${var.prefix}-rabbit"
    function_name = "${var.prefix}-function"
    app_insights_name = "${var.prefix}-insights"
}

resource "random_id" "storage_name" {
  keepers = {
    resource_group = azurerm_resource_group.rg.name
  }
  byte_length = 8
}

resource "random_string" "rabbitmq_cookie" {
  keepers = {
    resource_group = azurerm_resource_group.rg.name
  }
  special = false
  length = 20
  lower = false
  number = false
  upper = true

}

output "rabbitmq_cookie" {
    value = random_string.rabbitmq_cookie.result
}