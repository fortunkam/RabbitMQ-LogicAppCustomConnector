

resource "azurerm_container_group" "rabbitmq" {
  name                = local.rabbitmq_aci_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  ip_address_type     = "public"
  dns_name_label      = local.rabbitmq_aci_name
  os_type             = "Linux"

  container {
    name   = "rabbitmq"
    image  = "rabbitmq:latest"
    cpu    = "1.0"
    memory = "1.5"

    ports {
      port     = 5672
      protocol = "TCP"
    }
    ports {
      port     = 15672
      protocol = "TCP"
    }
    ports {
      port     = 25672
      protocol = "TCP"
    }
    ports {
      port     = 4369
      protocol = "TCP"
    }

    environment_variables = {
      "RABBITMQ_ERLANG_COOKIE" = random_string.rabbitmq_cookie.result
      "RABBITMQ_USE_LONGNAME" = "true"
      "RABBITMQ_NODENAME" = "rabbit@${local.rabbitmq_aci_name}.${var.location}.azurecontainer.io"
    }
  }
}