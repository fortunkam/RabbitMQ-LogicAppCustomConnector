// Checklist
// ACI with RabbitMQ container installed (https://registry.hub.docker.com/_/rabbitmq/)
// App Service (Function)

provider "azurerm" {
  version = "~> 2.42"
  features {}
}

provider "random" {
  version = "~> 2.3.1"
}
